using System;
using UnityEngine;
using UnityEngine.UI;

public class UnlockTokenButton : MonoBehaviour
{
    [SerializeField] Button button;

    void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        button.onClick.AddListener(Clicked);
    }

    public void Clicked()
    {
        Debug.Log("Unlock Slot");
        GameplayManager.Ins.OnSlotUnlock?.Invoke();
    }
}
