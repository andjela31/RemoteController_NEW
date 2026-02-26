using System.Collections.Generic;

[System.Serializable]
public class RemoteButtonData
{
    public string displayName;          // Tekst koji će biti prikazan na dugmetu
    public RemoteActionType action;     // Tip akcije dugmeta (enum)
    public bool isAdded = false;        // da li je dugme dodato na daljinski -> potrebno mi je za info da li je interactable u meniju
    public float size = 1f; // default size
}

[System.Serializable]
public class RemoteButtonDataList
{
    public List<RemoteButtonData> buttons;
}

public enum RemoteActionType
{
    Btn1,
    Btn2,
    Btn3,
    Btn4,
    Btn5,
    Btn6,
    Btn7,
    Btn8
}
