using System;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    [SerializeField] Button button;

    void Awake()
    {
        button.onClick.AddListener(Clicked);
    }

    public void Clicked()
    {
        Debug.Log("Undo");
        BoosterManager.Ins.ActivateBooster(BOOSTER.UNDO);
    }
}
