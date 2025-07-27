using Sirenix.OdinInspector;
using UnityEngine;

public class HighLightObject : MonoBehaviour
{
    public GameObject highlighter;

    public void Highlight()
    {
        highlighter.SetActive(true);
    }

    public void ClearHighlight()
    {
        highlighter.SetActive(false);
    }
}