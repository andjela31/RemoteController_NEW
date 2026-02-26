using System.Net.Sockets; // Za TCP
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Concurrent;


public class ServerConnector : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    public static ServerConnector Instance;
    public ConcurrentQueue<string> incomingMessages = new ConcurrentQueue<string>();

    public bool IsDisconnected { get; private set; } = true;



    private void Awake() => Instance = this;

    public async Task<bool> Connect(string address, int port)
    {
        try
        {
            client = new TcpClient();
            await client.ConnectAsync(address, port);
            stream = client.GetStream();
            SendHello();
            IsDisconnected = false;

            _ = ListenForMessages(); // cekaj poruku sa servera
            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task ListenForMessages()
    {
        byte[] buffer = new byte[1024];

        try
        {
            while (true)
            {
                int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                Debug.Log(bytes);
                if (bytes == 0)
                {
                    Debug.Log("Server closed connection (FIN)");
                    IsDisconnected = true;
                    break;
                }

                string msg = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

                if (msg == "SRV:QUIT")
                {
                    Debug.Log("Server shutdown received");
                    IsDisconnected = true;

                    stream?.Close();
                    client?.Close();
                    return;
                }

                incomingMessages.Enqueue(msg);
            }
        }
        catch
        {
            Debug.Log("Disconnected from server");
            IsDisconnected = true;
            stream?.Close();
            client?.Close();
        }
    }

    private void SendHello()
    {
        if (client == null || !client.Connected || stream == null)
            return;
        string hello = $"HELLO:{SystemInfo.deviceUniqueIdentifier}:{SystemInfo.deviceName}\n";
        byte[] data = Encoding.UTF8.GetBytes(hello);
        stream.Write(data, 0, data.Length);
    }

    public void SendButtonCommand(string displayName)
    {
        if (client == null || !client.Connected || stream == null)
            return;
        string payload = "BTN:" + displayName + "\n";
        byte[] data = Encoding.UTF8.GetBytes(payload);
        stream.Write(data, 0, data.Length);
    }

    public void SendTextMessage(string text)
    {
        if (client == null || !client.Connected || stream == null)
            return;

        text = text.Trim();
        if (string.IsNullOrEmpty(text)) return;

        string payload = "MSG:" + text + "\n";
        byte[] data = Encoding.UTF8.GetBytes(payload);
        stream.Write(data, 0, data.Length);
    }

    private void OnApplicationQuit()
    {
        stream?.Close();
        client?.Close();
    }
}
