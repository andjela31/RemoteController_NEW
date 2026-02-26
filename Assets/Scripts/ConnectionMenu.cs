using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionMenu : MonoBehaviour
{
    public GameObject connectionPanel;
    public TMP_InputField addressInput;
    public TMP_InputField portInput;
    public Button connectButton;
    public Image statusCircle1;
    public Image statusCircle2;

    private void Start()
    {
        connectButton.onClick.AddListener(OnConnectClicked);
        SetStatusRed();
    }


    private async void OnConnectClicked()
    {
        string address = addressInput.text;
        if (!int.TryParse(portInput.text, out int port)) return;

        bool success = await ServerConnector.Instance.Connect(address, port);

        if (success) SetStatusGreen();
        else SetStatusRed();
    }

    private void SetStatusGreen() 
    {
        statusCircle1.color = Color.green;
        statusCircle2.color = Color.green;
    }
    private void SetStatusRed()
    {
        statusCircle1.color = Color.red;
        statusCircle2.color = Color.red;
    }

    public void Open()
    {
        connectionPanel.SetActive(true);
    }

    public void Close()
    {
        connectionPanel.SetActive(false);
    }
}
