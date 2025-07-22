using System;
using UnityEngine;

public class Slot : MonoBehaviour
{
    Cube cube;
    public Cube Cube => cube;
    public bool IsEmpty => cube == null;

    public void SetPosition(Vector3 position, bool useLocalPosition)
    {
        if (useLocalPosition)
        {
            transform.localPosition = position;
        }
        else
        {
            transform.position = position;
        }
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

[Serializable]
public class SlotData
{
    public CubeData cube;
}
