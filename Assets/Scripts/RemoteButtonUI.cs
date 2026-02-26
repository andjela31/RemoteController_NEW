using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class RemoteButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI label;
    private RemoteButtonData data;
    public UnityEngine.UI.Image background;

    private bool selected = false;

    

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color deleteColor = Color.gainsboro;
    public Color deleteHoverColor = Color.whiteSmoke;

    public Color selectedColor = Color.darkGray;


    private bool isHovered = false;

    RectTransform rt;
    [SerializeField] private float baseFontSize = 80f; // početni font size

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void Setup(RemoteButtonData d)
    {
        data = d;
        label.text = d.displayName;
        ApplySize(d.size);
    }

    void Update()
    {
        RefreshVisual();
    }

    public void RefreshVisual()
    {
        if (DeleteManager.Instance != null && DeleteManager.Instance.deleteMode)
        {
            if (isHovered)
                background.color = deleteHoverColor;
            else
                background.color = deleteColor;
        }
        else if (EditManager.Instance != null && EditManager.Instance.editMode && selected)
        {
             background.color = selectedColor;
        }
        else
        {
            background.color = normalColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public void OnClick()
    {
        RemoteUIManager.Instance.HandleClick(this, data);
    }

    public void SetSelected(bool value)
    {
        if(EditManager.Instance.editMode)
        {
            selected = value;
        }
    }

    public void SetText(string newText)
    {
        data.displayName = newText;
        label.text = newText;
        LoadButtonsDataManager.Instance.SaveToFile();
    }

    public void SetSize(float size)
    {
        data.size = size;
        ApplySize(size);
        LoadButtonsDataManager.Instance.SaveToFile();
    }

    private void ApplySize(float size)
    {
        float w = RemoteUIManager.Instance.widthPercent * RemoteUIManager.Instance.parentWidth * size;
        float h = RemoteUIManager.Instance.heightPercent * RemoteUIManager.Instance.parentHeight * size;
        Debug.Log("Width btn: " + w);
        Debug.Log("Height btn: " + h);
        rt.sizeDelta = new Vector2(w, h);

        // scale font prema stvarnoj širini dugmeta
        label.fontSize = baseFontSize * size;
    }

    public float GetSize()
    {
        return data.size;
    }


}
