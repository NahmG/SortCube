using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public float scale;
    public float spacing;
    public int size;
    [SerializeField] Cube cubePref;
    [SerializeField] Slot slotPref;
    [SerializeField] StackData data;
    Board board;
    Slot[] slots;

    CUBE targetType; //Use to 

    public bool IsFull => slots.All(x => !x.IsEmpty);
    public bool IsEmpty => !slots.Any(x => !x.IsEmpty);
    public bool IsComplete =>
        IsFull && slots.All(x => x.Cube.Type == slots[0].Cube.Type);

    public void Init(Board board)
    {
        this.board = board;
        slots = new Slot[size];

        for (int i = 0; i < size; i++)
        {
            //new slot
            Slot newSlot = Instantiate(slotPref, transform);
            Vector3 pos = new(0, i * spacing, 0);
            newSlot.SetPosition(pos, true);

            //assign new slot -> array
            slots[i] = newSlot;
        }

        for (int i = 0; i < data.cubes.Length; i++)
        {
            Slot slot = slots[i];
            Cube cube = Instantiate(cubePref, slot.transform);
            cube.SetSlot(slot);
            cube.Init(data.cubes[i], scale);

            slot.Assign(cube);
        }
    }

    public void Push(List<Cube> cubes)
    {
        int cubeIndex = 0;
        for (int i = 0; i < slots.Length && cubeIndex < cubes.Count; i++)
        {
            if (slots[i].IsEmpty)
            {
                Cube cube = cubes[cubeIndex++];
                cube.Slot.Free();
                slots[i].Assign(cube);
                cube.AnimMoveToPosition();
            }
        }

        SetTargetType();
    }

    public List<Cube> Pop()
    {
        if (IsEmpty) return new();

        // Collect all consecutive cubes from the top with type similar to targetType
        List<Cube> cubes = new List<Cube>();
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (!slots[i].IsEmpty && slots[i].Cube.Type == targetType)
            {
                cubes.Add(slots[i].Cube);
            }
            else if (!slots[i].IsEmpty)
            {
                break;
            }
        }
        cubes.Reverse();
        return cubes;
    }

    //Check if Stack is pushable 
    public bool CanPush(List<Cube> cubes)
    {
        if (IsFull) return false;

        if (!IsEmpty)
        {
            if (cubes[0].Type != targetType) return false;
        }

        return true;
    }

    void SetTargetType()
    {
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (!slots[i].IsEmpty)
            {
                Slot lastNonEmptySlot = slots[i];
                targetType = lastNonEmptySlot.Cube.Type;
                break;
            }
        }
    }

    void OnMouseDown()
    {
        board.SetStack(this);
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < size; i++)
        {
            Vector3 position = transform.position + new Vector3(0, i * spacing);
            Gizmos.DrawWireCube(position, Vector3.one * scale);
        }

        if (data.cubes.Length == 0) return;
        for (int i = 0; i < data.cubes.Length; i++)
        {
            Vector3 pos = transform.position + new Vector3(0, i * spacing, 0);
            UnityEditor.Handles.Label(pos, data.cubes[i].ToString());
        }
    }

#endif
}

[Serializable]
public class StackData
{
    public CUBE[] cubes;
}


