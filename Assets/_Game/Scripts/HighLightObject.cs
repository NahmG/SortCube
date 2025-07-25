using UnityEngine;

public class HighLightObject : MonoBehaviour
{
    public int outlineLayer;
    public int originalLayer;

    [ContextMenu("Highlight")]
    public void Highlight()
    {
        gameObject.layer = outlineLayer;
    }

    [ContextMenu("ClearHighlight")]
    public void ClearHighlight()
    {
        gameObject.layer = originalLayer;
    }
}