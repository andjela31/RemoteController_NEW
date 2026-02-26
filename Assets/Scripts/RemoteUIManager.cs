using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoteUIManager : MonoBehaviour
{
    public RectTransform rect;
    public Transform remoteContent;          // Content daljinskog
    public GameObject remoteButtonPrefab;    // Prefab dugmeta daljinskog
    public static RemoteUIManager Instance;
    public GameObject placeholderPrefab;

    public Dictionary<RemoteActionType, RemoteButtonUI> buttonsByAction;
    public Dictionary<RemoteActionType, GameObject> placeholders;

    public TMP_InputField messageInput;

    public float parentWidth = 1000f;
    public float parentHeight = 1500f;

    public float widthPercent = 0.47f;
    public float heightPercent = 0.235f;

    private void Awake()
    {
        Instance = this;
        buttonsByAction = new Dictionary<RemoteActionType, RemoteButtonUI>();
        placeholders = new Dictionary<RemoteActionType, GameObject>();
    }

    public void HandleClick(RemoteButtonUI button, RemoteButtonData data)
    {
        if (DeleteManager.Instance != null && DeleteManager.Instance.deleteMode)
        {
            Instance.RemoveButton(data.action);
            DeleteManager.Instance.EnableButton(data);
            LoadButtonsDataManager.Instance.SaveToFile();
        }
        else if (EditManager.Instance != null && EditManager.Instance.editMode)
        {
            EditManager.Instance.Select(button);
        }
        else
        {
            ServerConnector.Instance.SendButtonCommand(data.displayName);
            messageInput.SetTextWithoutNotify(data.displayName);
        }
    }

    private void Start()
    {
        parentWidth = rect.rect.width;
        parentHeight = rect.rect.height;

        Debug.Log("Width: " + parentWidth);
        Debug.Log("Height: " + parentHeight);

        Debug.Log("Width ph: " + (parentWidth * widthPercent));
        Debug.Log("Height ph: " + (parentHeight * heightPercent));
        // napravi placeholder za SVAKI enum
        foreach (RemoteActionType type in Enum.GetValues(typeof(RemoteActionType)))
        {
            GameObject ph = Instantiate(placeholderPrefab, remoteContent);

            RectTransform phRect = ph.GetComponent<RectTransform>();

            phRect.sizeDelta = new Vector2(
                parentWidth * widthPercent,
                parentHeight * heightPercent
            );


            placeholders.Add(type, ph);
            ThemeManager.Instance.ApplyActiveThemeToPlaceholder(ph);
        }

        // natera layout da se odmah izračuna
        LayoutRebuilder.ForceRebuildLayoutImmediate(remoteContent as RectTransform);

        foreach (RemoteButtonData d in LoadButtonsDataManager.Instance.buttonsData)
        {
            if (d.isAdded)
                AddButton(d);
        }
    }

    public void AddButton(RemoteButtonData data)
    {
        if (buttonsByAction.ContainsKey(data.action))
            return;

        int index = (int)data.action;

        // Ukloni placeholder
        Destroy(placeholders[data.action]);

        // napravi dugme
        GameObject btn = Instantiate(remoteButtonPrefab, remoteContent);

        btn.GetComponentInChildren<TextMeshProUGUI>().text = data.displayName;

        // OVO JE BITNO ZA TACNU POZICIJU DUGMETA!!!
        btn.transform.SetSiblingIndex(placeholders[data.action].transform.GetSiblingIndex());

        RemoteButtonUI btnUI = btn.GetComponent<RemoteButtonUI>();
        btnUI.Setup(data);

        buttonsByAction[data.action] = btnUI;

        // poveži Button.onClick sa btnUI.OnClick
        // klik ne bi radio bez ovoga -> TESTIRAJ DA LI JE TO TAKO!
        Button b = btn.GetComponent<Button>();
        b.onClick.AddListener(() => btnUI.OnClick());
        ThemeManager.Instance.ApplyActiveThemeToButton(b);

        data.isAdded = true;
        LoadButtonsDataManager.Instance.SaveToFile();

        LayoutRebuilder.ForceRebuildLayoutImmediate(remoteContent as RectTransform);
    }

    public void ApplyButtonsTheme()
    {
        foreach (RemoteButtonUI btnUI in buttonsByAction.Values)
        {
            Button b = btnUI.GetComponent<Button>();
            if (b != null)
            {
                ThemeManager.Instance.ApplyActiveThemeToButton(b);
            }
        }
    }

    public void ApplyPlaceholdersTheme()
    {
        foreach (GameObject ph in placeholders.Values)
        {
            if (ph != null)
            {
                ThemeManager.Instance.ApplyActiveThemeToPlaceholder(ph);
            }
        }
    }

    public void RemoveButton(RemoteActionType type)
    {
        if (!buttonsByAction.ContainsKey(type))
            return;

        int index = (int)type;

        Destroy(buttonsByAction[type].gameObject);
        buttonsByAction.Remove(type);

        // vrati placeholder NA ISTU POZICIJU
        GameObject ph = Instantiate(placeholderPrefab, remoteContent);

        RectTransform phRect = ph.GetComponent<RectTransform>();

        phRect.sizeDelta = new Vector2(
            parentWidth * widthPercent,
            parentHeight * heightPercent
        );

        ph.transform.SetSiblingIndex(index);
        ThemeManager.Instance.ApplyActiveThemeToPlaceholder(ph);

        placeholders[type] = ph;

        LayoutRebuilder.ForceRebuildLayoutImmediate(remoteContent as RectTransform);
    }
}
