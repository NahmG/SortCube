using System;
using UnityEngine;
using UnityEngine.UI;

public class ShuffleStackButton : MonoBehaviour
{
    [SerializeField] Button button;

    void Awake()
    {
        button.onClick.AddListener(Clicked);
    }

    public void Clicked()
    {
        Debug.Log("Shuffle");
        GameplayManager.Ins.EnterTargetMode();
    }
}
