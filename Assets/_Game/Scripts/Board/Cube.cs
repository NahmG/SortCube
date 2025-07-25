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
    GameObject view;

    public void Init(CUBE type, float scale)
    {
        this.type = type;

        view = Instantiate(mesh, transform);
        view.GetComponent<MeshRenderer>().material = data.GetColor(type);
        view.transform.localScale = Vector3.one * scale;
    }

    public void SetSlot(Slot slot)
    {
        this.slot = slot;
        transform.SetParent(slot.transform);
    }

    public void Selected()
    {
        view.GetComponent<HighLightObject>().Highlight();
    }

    public void DeSelect()
    {
        view.GetComponent<HighLightObject>().ClearHighlight();
    }

    public void AnimMoveToPosition()
    {
        transform.DOLocalMove(Vector3.zero, .3f);
    }
}

public enum CUBE
{
    BLUE = 1,
    RED = 2,
    YELLOW = 3,
    GREEN = 4,
}
