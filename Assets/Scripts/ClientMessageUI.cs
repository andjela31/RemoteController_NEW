using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientMessageUI : MonoBehaviour
{
    public TMP_Text messagesText;
    public Image statusIndicator1;
    public Image statusIndicator2;

    void Update()
    {
        if (ServerConnector.Instance == null)
            return;

        var connector = ServerConnector.Instance;

        if (connector.IsDisconnected)
            SetStatusRed();

        while (connector.incomingMessages.TryDequeue(out string msg))
        {
            HandleMessage(msg);
        }
    }

    private void SetStatusRed()
    {
        statusIndicator1.color = Color.red;
        statusIndicator2.color = Color.red;
    }

    void HandleMessage(string msg)
    {
        if (msg.StartsWith("SRV:"))
        {
            Debug.Log(msg);
            MessagePopUps.Instance.AddMessage(msg);
            //messagesText.text += "\n[SERVER] " + msg.Substring(4);
        }
        else
        {
            Debug.Log(msg);
            //messagesText.text += "\n" + msg;
        }
    }
}
