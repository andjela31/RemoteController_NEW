using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPanelUI : MonoBehaviour
{
    public TMP_InputField textInput;
    public Slider sizeSlider;

    public Image editImage;
    public Sprite editActiveSprite;
    public Sprite editInactiveSprite;
    public Button editButton;

    private void Start()
    {
        textInput.onValueChanged.AddListener(OnTextChanged);
        sizeSlider.onValueChanged.AddListener(OnSizeChanged);
    }

    public void OnTextChanged(string value)
    {
        if (EditManager.Instance.selectedButton == null) return;
        EditManager.Instance.selectedButton.SetText(value);
    }

    public void OnSizeChanged(float value)
    {
        if (EditManager.Instance.selectedButton == null) return;
        EditManager.Instance.selectedButton.SetSize(value);
    }

    public void SetUIFromButton(RemoteButtonUI btn)
    {
        sizeSlider.SetValueWithoutNotify(btn.GetSize());
        textInput.SetTextWithoutNotify(btn.label.text);
    }

    public void ClearUI()
    {
        textInput.SetTextWithoutNotify("");
        sizeSlider.SetValueWithoutNotify(sizeSlider.maxValue);
    }

    public void ToggleEditPanel()
    {
        if(gameObject.activeSelf)
        {
            EditManager.Instance.ExitEditMode();
            gameObject.SetActive(false);
        }
        else
        {
            UIPanelManager.Instance.CloseAll();
            EditManager.Instance.EnterEditMode();
            gameObject.SetActive(true);
        }
    }

    public void Close()
    {
        EditManager.Instance.ExitEditMode();
        //LoadButtonsDataManager.Instance.SaveToFile();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        editImage.sprite = editActiveSprite;
        //btn.image.color = Color.lightGray;
        Color c = editButton.image.color;
        c = Color.lightGray;
        c.a = 0.5f; // 50% providno
        editButton.image.color = c;
        ThemeManager.Instance.ApplyActiveThemeToNavButton(editImage);
    }

    private void OnDisable()
    {
        editImage.sprite = editInactiveSprite;
        Color c = Color.white;
        c.a = 0f; // potpuno vidljivo
        editButton.image.color = c;
        ThemeManager.Instance.ApplyActiveThemeToNavButton(editImage);
    }
}
