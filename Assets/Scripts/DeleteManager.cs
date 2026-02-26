using UnityEngine;
using UnityEngine.UI;

public class DeleteManager : MonoBehaviour
{
    public static DeleteManager Instance;
    public bool deleteMode = false;
    public Button btn;

    public Image img;
    public Sprite normalSprite;
    public Sprite deleteModeSprite;

    private void Awake() => Instance = this;

    public void EnterDeleteMode()
    {
        deleteMode = true;

        img.sprite = deleteModeSprite;
        //btn.image.color = Color.lightGray;
        Color c = btn.image.color;
        c = Color.lightGray;
        c.a = 0.5f; // 50% providno
        btn.image.color = c;
        ThemeManager.Instance.ApplyActiveThemeToNavButton(img);
    }

    public void ExitDeleteMode()
    {
        deleteMode = false;

        img.sprite = normalSprite;
        Color c = Color.white;
        c.a = 0f; // potpuno vidljivo
        btn.image.color = c;
        ThemeManager.Instance.ApplyActiveThemeToNavButton(img);
    }

    public void ToggleDeleteMode()
    {
        if (deleteMode)
        {
            ExitDeleteMode();
        }
        else
        {
            UIPanelManager.Instance.CloseAll();
            EnterDeleteMode();
        }
    }

    public void EnableButton(RemoteButtonData data)
    {
        MenuButtonsUI.Instance.EnableButton(data);
    }

}
