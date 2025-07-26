using System;
using UnityEngine;
using UnityEngine.UI;

public class UnlockTokenButton : MonoBehaviour
{
    [SerializeField] Button button;

    void Awake()
    {
        button.onClick.AddListener(Clicked);
    }

    public void Clicked()
    {
        LevelManager.Ins.OnSlotUnlock?.Invoke();
    }
}
