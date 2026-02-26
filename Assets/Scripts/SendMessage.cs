using TMPro;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    public TMP_InputField messageInput;

    public void OnSendMessageClicked()
    {
        string text = messageInput.text;
        if (string.IsNullOrWhiteSpace(text))
            return;

        Debug.Log(text);
        ServerConnector.Instance.SendTextMessage(text);
        messageInput.text = "";
    }
}
