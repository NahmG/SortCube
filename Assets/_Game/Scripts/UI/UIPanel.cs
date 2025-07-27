using Sirenix.OdinInspector;
using UnityEngine;

public class UIPanel : SerializedMonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}