using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class Cube : MonoBehaviour
{
    [SerializeField] GameObject mesh; //render part of the cube
    [SerializeField] CubeData data;
    CUBE type;
    public CUBE Type => type;
    Slot slot;
    public Slot Slot => slot;

    public void Init(CUBE type, float scale)
    {
        this.type = type;

        mesh.GetComponent<SpriteRenderer>().color = data.GetColor(type);
        mesh.transform.localScale = Vector3.one * scale;
    }

    public void SetSlot(Slot slot)
    {
        this.slot = slot;
        transform.SetParent(slot.transform);
    }

    public void Selected()
    {
        mesh.GetComponent<HighLightObject>().Highlight();
    }

    public void DeSelect()
    {
        mesh.GetComponent<HighLightObject>().ClearHighlight();
    }

    public void AnimMoveToPosition()
    {
        transform.DOMove(slot.transform.position, .3f);
    }
}

public enum CUBE
{
    BLUE = 1,
    RED = 2,
    YELLOW = 3,
    GREEN = 4,
}
