using System;
using UnityEngine;

public class Slot
{
    Cube cube;
    public Cube Cube => cube;
    public bool IsEmpty => cube == null;

    public Vector3 _position { get; private set; }
    public Transform _root { get; private set; }

    public Slot(Vector3 position, Transform root)
    {
        _position = position;
        _root = root;
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
}
