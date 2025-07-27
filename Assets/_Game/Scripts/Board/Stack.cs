using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;

[Serializable]
public class Stack : MonoBehaviour
{
    [SerializeField] protected float itemScale;
    [SerializeField] protected float cellScale;
    [SerializeField] protected float spacing;
    [SerializeField] protected int size;
    [SerializeField] protected Cube cubePref;
    [SerializeField] protected Slot slotPref;
    [SerializeField] protected List<CUBE> cubeSet = new();
    protected Slot[] slots;

    Board board;
    CUBE targetType;

    public bool IsFull => slots.All(x => !x.IsEmpty);
    public bool IsEmpty => !slots.Any(x => !x.IsEmpty);
    public bool IsComplete => IsFull && slots.All(x => x.Cube.Type == slots[0].Cube.Type);
    public UndoManager UndoManager
    {
        get
        {
            BoosterUndo boosterUndo = BoosterManager.Ins.GetBooster<BoosterUndo>(BOOSTER.UNDO);
            return boosterUndo.undoManager;
        }
    }

    public void Init(Board board)
    {
        this.board = board;
        CreateStack();
    }

    public void SetCubeSet(List<CUBE> cubeSet)
    {
        this.cubeSet = cubeSet;
    }

    public virtual void CreateStack()
    {
        slots = new Slot[size];
        //gen new slot
        for (int i = 0; i < size; i++)
        {
            //new slot
            Vector3 pos = transform.position + new Vector3(0, i * spacing, 0);
            Slot newSlot = Instantiate(slotPref, transform);
            newSlot.Init(pos, cellScale, false);

            //assign new slot -> array
            slots[i] = newSlot;
        }

        //gen cube
        for (int i = 0; i < cubeSet.Count; i++)
        {
            Slot slot = slots[i];
            Cube cube = Instantiate(cubePref, slot.transform);
            cube.Init(cubeSet[i], itemScale);

            slot.Assign(cube);
        }
    }

    public void Push(List<Cube> cubes)
    {
        int emptySlots = GetEmptySlotCount();
        int count = Mathf.Min(emptySlots, cubes.Count);

        int cubeIndex = count - 1;
        for (int i = 0; i < slots.Length && cubeIndex >= 0; i++)
        {
            if (slots[i].IsEmpty && !slots[i].IsLock)
            {
                Cube cube = cubes[cubeIndex--];

                UndoManager.SetRecord(cube.Slot, slots[i], cube);

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

    public void Shuffle()
    {
        if (IsEmpty) return;

        // Gather all cubes in the stack
        List<Cube> cubes = new List<Cube>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty)
            {
                cubes.Add(slots[i].Cube);
                slots[i].Free();
            }
        }

        // Shuffle the cubes
        for (int i = cubes.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (cubes[i], cubes[j]) = (cubes[j], cubes[i]);
        }

        // Reassign cubes to slots from bottom up
        for (int i = 0; i < cubes.Count; i++)
        {
            slots[i].Assign(cubes[i]);
            cubes[i].AnimMoveToPosition();
        }
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
        for (int i = size - 1; i >= 0; i--)
        {
            if (slots[i].IsEmpty && !slots[i].IsLock)
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
        if (GameplayManager.Ins.IsTargetMode)
        {
            GameplayManager.Ins.OnTargetSelect(this);
        }
        board.OnStackSelected(this);
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < size; i++)
        {
            Vector3 position = transform.position + new Vector3(0, i * spacing);
            Gizmos.DrawWireCube(position, Vector3.one * itemScale);
        }

        // if (cubeSet.Count == 0) return;
        // for (int i = 0; i < cubeSet.Count; i++)
        // {
        //     Vector3 pos = transform.position + new Vector3(0, i * spacing, 0);
        //     UnityEditor.Handles.Label(pos, cubeSet[i].ToString());
        // }
    }
#endif
}

