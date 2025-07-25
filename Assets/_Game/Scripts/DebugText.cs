using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    public TMP_Text textComponent;
    string currentText;
    public string CurrentText => currentText;

    public void Init(Vector3 pos, float scale)
    {
        transform.position = pos;
        transform.localScale = scale * Vector3.one;
    }

    public void Set(string text)
    {
        currentText = text;
        textComponent.text = currentText;
    }
}