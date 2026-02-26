using UnityEngine;
using UnityEngine.UI;

public class NavButtonUI : MonoBehaviour
{
    [Header("References")]
    public Image icon;          
    //public TMP_Text label;

    public void ApplyTheme(ThemeData theme)
    {
        if (icon != null)
            icon.color = theme.NavButtons;
    }
}