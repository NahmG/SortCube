using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] Cube cubePref;
    [SerializeField] Slot slotPref;
    [SerializeField] StackData data;
    Board board;
    Slot[] slots;


    public bool IsFull => slots.All(x => x.Cube.Type != CUBE.NONE);
    public bool IsEmpty => slots.Any(x => x.Cube.Type != CUBE.NONE);

    public void Init(Board board)
    {
        this.board = board;
        slots = new Slot[data.size];

        for (int i = 0; i < data.size; i++)
        {
            //new slot
            Slot newSlot = Instantiate(slotPref, transform);
            Vector3 pos = new(0, i * data.spacing, 0);
            newSlot.SetPosition(pos, true);

            //new cube
            Cube newCube = Instantiate(cubePref, newSlot.transform);
            newCube.Init(data.cubes[i]);

            //Assign new cube -> new slot
            newSlot.Assign(newCube);

            //assign new slot -> array
            slots[i] = newSlot;
        }
    }

    public void Push(Cube cube)
    {
        Slot firstEmptySlot = slots.First(x => x.Cube.Type == CUBE.NONE);
        firstEmptySlot.Assign(cube);
        cube.AnimMoveToPosition();
    }

    public Cube Pop()
    {
        Slot lastNonEmptySlot = slots.Last(x => x.Cube.Type != CUBE.NONE);
        Cube cube = lastNonEmptySlot.Cube;
        lastNonEmptySlot.Free();

        return cube;
    }

    void OnMouseDown()
    {
        board.SetStack(this);
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < data.size; i++)
        {
            Vector3 position = transform.position + new Vector3(0, i * data.spacing);
            Gizmos.DrawWireCube(position, Vector3.one);
        }
    }
#endif
}

[Serializable]
public class StackData
{
    public int size;
    public float spacing;
    public CUBE[] cubes;
}


