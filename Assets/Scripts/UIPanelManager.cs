using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    public OptionsMenu optionsPanel;
    public EditPanelUI editPanel;

    public static UIPanelManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void CloseAll()
    {
        optionsPanel.Close();
        MenuButtonsUI.Instance.Close();
        DeleteManager.Instance.ExitDeleteMode();
        editPanel.Close();
    }
}
