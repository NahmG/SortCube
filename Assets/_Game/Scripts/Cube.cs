using System;
using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{
    Slot slot;
    public Slot Slot => slot;

    public void SetSlot(Slot slot)
    {
        this.slot = slot;
    }

    public void AnimMoveToPosition()
    {
        transform.DOMove(Slot.transform.position, .2f);
    }
}

[Serializable]
public class CubeData
{
    public CUBE type;
}

public enum CUBE
{
    NONE = 0,
    BLUE = 1,
    RED = 2,
    YELLOW = 3,
    GREEN = 4,

}
