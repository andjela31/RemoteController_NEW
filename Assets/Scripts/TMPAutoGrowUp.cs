using TMPro;
using UnityEngine;

public class TMPGrowUp : MonoBehaviour
{
    public TMP_InputField input;
    public float minHeight = 115f;
    public float padding = 5f;

    RectTransform rt;

    void Awake()
    {
        rt = input.GetComponent<RectTransform>();
        input.onValueChanged.AddListener(OnTextChanged);
    }

    void OnTextChanged(string _)
    {
        float h = input.textComponent.preferredHeight + padding;
        rt.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            Mathf.Max(minHeight, h)
        );
    }
}
