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
    [SerializeField] StackData data;
    Board board;
    Slot[] slots;

    CUBE targetType;

    public bool IsFull => slots.All(x => !x.IsEmpty);
    public bool IsEmpty => !slots.Any(x => !x.IsEmpty);
    public bool IsComplete => IsFull && slots.All(x => x.Cube.Type == slots[0].Cube.Type);

    public void Init(Board board)
    {
        this.board = board;
        slots = new Slot[size];

        //gen new slot
        for (int i = 0; i < size; i++)
        {
            //new slot
            Vector3 pos = transform.position + new Vector3(0, i * spacing, 0);
            Slot newSlot = new(pos, transform);

            //assign new slot -> array
            slots[i] = newSlot;
        }

        //gen cube
        for (int i = 0; i < data.cubes.Length; i++)
        {
            Slot slot = slots[i];
            Cube cube = Instantiate(cubePref, slot._root);
            cube.Init(data.cubes[i], scale);
            cube.transform.position = slot._position;

            slot.Assign(cube);
        }
    }

    public void Push(List<Cube> cubes)
    {
        int emptySlot = GetEmptySlotCount();
        int count = Mathf.Min(emptySlot, cubes.Count);

        int cubeIndex = count - 1;
        for (int i = 0; i < slots.Length && cubeIndex >= 0; i++)
        {
            if (slots[i].IsEmpty)
            {
                Cube cube = cubes[cubeIndex--];
                cube.Slot.Free();
                slots[i].Assign(cube);
                cube.AnimMoveToPosition();
            }
        }
    }

    public List<Cube> Pop()
    {
        if (IsEmpty) return new();

        // Collect all adjacent cubes from the top with type == targetType
        SetTargetType();
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
        return cubes;
    }

    //Check if Stack is pushable 
    public bool CanPush(List<Cube> cubes)
    {
        if (IsFull) return false;

        if (!IsEmpty)
        {
            SetTargetType();
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

    int GetEmptySlotCount()
    {
        int count = 0;
        if (IsEmpty) return size;
        for (int i = size - 1; i >= 0; i--)
        {
            if (slots[i].IsEmpty)
            {
                count++;
            }
            else
                break;
        }
        return count;
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


