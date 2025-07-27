using System;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] GameObject lockView;
    Cube cube;
    public Cube Cube => cube;
    public bool IsEmpty => cube == null;
    bool isLock;
    public bool IsLock => isLock;

    public void Init(Vector3 position, float scale, bool isLock)
    {
        this.isLock = isLock;
        transform.position = position;
        transform.localScale = scale * Vector3.one;

        lockView.SetActive(isLock);
    }

    public void Assign(Cube cube)
    {
        this.cube = cube;
        cube.SetSlot(this);
    }

    public void Free()
    {
        cube = null;
    }

    public void Unlock()
    {
        isLock = false;
        lockView.SetActive(false);
    }
}
