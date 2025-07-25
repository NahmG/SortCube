using UnityEngine;

public class HighLightObject : MonoBehaviour
{
    int originalLayer;

    void Awake()
    {
        originalLayer = gameObject.layer;
    }

    [ContextMenu("Highlight")]
    public void Highlight()
    {
        gameObject.layer = LayerMask.GetMask("Outline");
    }

    [ContextMenu("ClearHighlight")]
    public void ClearHighlight()
    {
        gameObject.layer = originalLayer;
    }
}