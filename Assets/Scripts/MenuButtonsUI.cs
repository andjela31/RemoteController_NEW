using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsUI : MonoBehaviour
{
    public Transform content;
    public GameObject menuButtonPrefab;
    public Button addButton;
    public Image addImage;
    public Sprite addActiveSprite;
    public Sprite addInactiveSprite;
    public GameObject menu;

    [HideInInspector]
    public Dictionary<RemoteButtonData, Button> menuButtonsMap = new();

    public static MenuButtonsUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    void CreateMenuButtons()
    {
        foreach (Transform c in content)
            Destroy(c.gameObject);

        menuButtonsMap.Clear();

        foreach (RemoteButtonData data in LoadButtonsDataManager.Instance.buttonsData)
        {
            RemoteButtonData localData = data; // localData mi treba da bi data ostao upamcen za kasniji klik na dugme
            // kako bi se ono dodalo i kako bi imalo podatke koje treba da ima

            GameObject btnObj = Instantiate(menuButtonPrefab, content);
            Button btn = btnObj.GetComponent<Button>();

            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = localData.displayName;
            btn.interactable = !localData.isAdded;

            btn.onClick.AddListener(() =>
            {
                RemoteUIManager.Instance.AddButton(localData);
                btn.interactable = false;
            });

            menuButtonsMap[localData] = btn;
        }
    }

    public void EnableButton(RemoteButtonData data)
    {
        data.isAdded = false;
        if (!menuButtonsMap.ContainsKey(data)) // OVO JE PROBLEM JER MAPA SE NAPUNI KAD OTVORIM MENI
            return; // U OVOM SLUCAJU DESI SE RETURN !!!!
        menuButtonsMap[data].interactable = true;
    }

    public void Open()
    {
        UIPanelManager.Instance.CloseAll();
        menu.SetActive(true);

        addImage.sprite = addActiveSprite;
        //btn.image.color = Color.lightGray;
        Color c = addButton.image.color;
        c = Color.lightGray;
        c.a = 0.5f; // 50% providno
        addButton.image.color = c;
        ThemeManager.Instance.ApplyActiveThemeToNavButton(addImage);

        // isAdded se menja dok je meni zatvoren i potrebno je osvezavanje pri otvaranju
        // ovo je ok za mali broj dugmica nije skupa operacija
        CreateMenuButtons(); // ovo se poziva svaki put kada aktiviramo meni !!!

    }

    public void Close()
    {
        menu.SetActive(false);

        addImage.sprite = addInactiveSprite;
        Color c = Color.white;
        c.a = 0f; // potpuno vidljivo
        addButton.image.color = c;
        ThemeManager.Instance.ApplyActiveThemeToNavButton(addImage);
    }

    public void ToggleMenu()
    {
        if (menu.activeSelf)
            Close();
        else
            Open();
    }
}
