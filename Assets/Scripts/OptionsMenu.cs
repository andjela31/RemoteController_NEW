using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public ConnectionMenu connectionMenu;
    public ColorPalettePanel colorPalettePanel;
    public Button menuButton;
    public Image menuImage;
    public Sprite menuActiveSprite;
    public Sprite menuInactiveSprite;

    public void Open()
    {
        UIPanelManager.Instance.CloseAll();
        SetVisual(true);
        optionsPanel.SetActive(true);
    }

    private void SetVisual(bool active)
    {
        menuImage.sprite = active ? menuActiveSprite : menuInactiveSprite;
        ThemeManager.Instance.ApplyActiveThemeToNavButton(menuImage);

        Color c = active ? Color.lightGray : Color.white;
        c.a = active ? 0.5f : 0f;
        menuButton.image.color = c;
    }

    public void Close()
    {
        SetVisual(false);
        optionsPanel.SetActive(false);
        // gasi sve pod-panele
        connectionMenu.Close();
        colorPalettePanel.Close();
    }

    public void Toggle()
    {
        if (optionsPanel.activeSelf)
            Close();
        else
            Open();
    }
}
