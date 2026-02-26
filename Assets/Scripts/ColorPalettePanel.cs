using UnityEngine;

public class ColorPalettePanel : MonoBehaviour
{
    public GameObject colorPalettePanel;
    public void Open()
    {
        colorPalettePanel.SetActive(true);
    }

    public void Close()
    {
        colorPalettePanel.SetActive(false);
    }

    public void ColorOriginal()
    {
        ThemeManager.Instance.SelectTheme("original");
    }

    public void ColorGreen()
    {
        ThemeManager.Instance.SelectTheme("green");
    }

    public void ColorBlue()
    {
        ThemeManager.Instance.SelectTheme("blue");
    }

    public void ColorDark()
    {
        ThemeManager.Instance.SelectTheme("dark");
    }
}
