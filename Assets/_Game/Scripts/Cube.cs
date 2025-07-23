using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] GameObject mesh; //render part of the cube
    [SerializeField] CubeData data;
    CUBE type;
    public CUBE Type => type;
    Slot slot;
    public Slot Slot => slot;

    public void Init(CUBE type)
    {
        this.type = type;

        GameObject g = Instantiate(mesh, transform);
        g.GetComponent<MeshRenderer>().material = data.GetColor(type);
    }

    public void SetSlot(Slot slot)
    {
        this.slot = slot;
        transform.SetParent(slot.transform);
    }

    public void AnimMoveToPosition()
    {
        transform.DOLocalMove(Vector3.zero, .2f);
    }
}

public enum CUBE
{
    BLUE = 1,
    RED = 2,
    YELLOW = 3,
    GREEN = 4,
}
