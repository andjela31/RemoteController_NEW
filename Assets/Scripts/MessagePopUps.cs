using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePopUps : MonoBehaviour
{
    public TMP_Text msgPrefab;
    public Transform panelPrefab;
    public Transform remote;
    public static MessagePopUps Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void AddMessage(string msg)
    {
        string text = msg.Replace("SRV:", "");
        string header = "Message from server:";
        Transform panelInstance = Instantiate(panelPrefab, remote);
        TMP_Text headerInstance = Instantiate(msgPrefab, panelInstance);
        headerInstance.text = header;
        TMP_Text textInstance = Instantiate(msgPrefab, panelInstance);
        textInstance.text = text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(panelInstance.GetComponent<RectTransform>());

        ThemeManager.Instance.ApplyActiveThemeToMessagePanel(panelInstance);

        Button closeBtn = panelInstance.GetComponentInChildren<Button>();
        closeBtn.onClick.AddListener(() =>
        {
            Destroy(panelInstance.gameObject);
        });
    }

}
