using UnityEngine;

public class EditManager : MonoBehaviour
{
    public static EditManager Instance;

    public bool editMode = false;
    public RemoteButtonUI selectedButton = null;
    public EditPanelUI editPanel;

    private void Awake() => Instance = this;

    public void ExitEditMode()
    {
        Deselect();
        editPanel.ClearUI();
        editMode = false;
    }

    public void EnterEditMode()
    {
        editMode = true;
    }

    public void Select(RemoteButtonUI btn)
    {
        if (!editMode) return;

        Deselect();
        selectedButton = btn;
        btn.SetSelected(true);

        editPanel.SetUIFromButton(btn);
    }

    public void Deselect()
    {
        if (selectedButton != null)
            selectedButton.SetSelected(false);

        selectedButton = null;
    }
}
