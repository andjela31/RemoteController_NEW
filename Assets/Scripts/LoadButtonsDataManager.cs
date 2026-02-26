using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadButtonsDataManager : MonoBehaviour
{
    public List<RemoteButtonData> buttonsData;
    public string fileName = "buttons.json";

    public static LoadButtonsDataManager Instance;

    private string FilePath => Path.Combine(Application.persistentDataPath, fileName);

    private void Awake()
    {
        Instance = this;

        if (!File.Exists(FilePath))
        {
            string defaultPath = Path.Combine(
                Application.streamingAssetsPath,
                fileName
            );

#if UNITY_ANDROID
            UnityWebRequest www =
                UnityWebRequest.Get(defaultPath);

            www.SendWebRequest();

            while (!www.isDone) { }

            if (www.result == UnityWebRequest.Result.Success)
            {
                File.WriteAllText(FilePath, www.downloadHandler.text);
            }
#else
        if (File.Exists(defaultPath))
            File.Copy(defaultPath, FilePath);
#endif
        }

        LoadFromFile();
    }

    public void LoadFromFile()
    {

        if (!File.Exists(FilePath))
        {
            Debug.LogError("buttons.json not found");
            buttonsData = new List<RemoteButtonData>();
            return;
        }

        string json = File.ReadAllText(FilePath);
        RemoteButtonDataList wrapper =
            JsonUtility.FromJson<RemoteButtonDataList>(json);

        buttonsData = wrapper.buttons;
    }


    public void SaveToFile()
    {
        RemoteButtonDataList wrapper = new RemoteButtonDataList
        {
            buttons = buttonsData
        };

        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(FilePath, json);
    }
}