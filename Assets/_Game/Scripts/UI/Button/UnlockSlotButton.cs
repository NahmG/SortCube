using System;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSlotButton : MonoBehaviour
{
    [SerializeField] Button button;

    void Awake()
    {
        button.onClick.AddListener(Clicked);
    }

    public void Clicked()
    {
        Debug.Log("Unlock Slot");
        BoosterManager.Ins.ActivateBooster(BOOSTER.UNLOCK_SLOT);
    }
}
