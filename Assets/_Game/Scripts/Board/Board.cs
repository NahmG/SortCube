using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Stack[] stacks;

    Stack source;
    Stack target;

    List<Cube> cubeToMove = new();

    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        foreach (Stack s in stacks)
        {
            s.Init(this);
        }
    }

    public void SetStack(Stack stack)
    {
        if (source == null)
        {
            if (stack.IsEmpty || stack.IsComplete) return;

            source = stack;
            cubeToMove = source.Pop();
            cubeToMove.ForEach(x => x.Selected());
        }
        else if (target == null)
        {
            target = stack;

            if (source != target && !target.IsFull && cubeToMove.Count > 0)
                Sort();

            ResetInput();
        }
    }

    void Sort()
    {
        if (target.CanPush(cubeToMove))
        {
            target.Push(cubeToMove);
            CheckCompletion();
        }
    }

    void ResetInput()
    {
        source = null;
        target = null;

        cubeToMove.ForEach(x => x.DeSelect());
        cubeToMove.Clear();
    }

    void CheckCompletion()
    {
        int completeCount = stacks.Count(s => s.IsComplete);
        int colorCount = Enum.GetValues(typeof(CUBE)).Length;

        if (completeCount == colorCount)
        {
            Debug.Log("All stacks for each color are complete! Level Complete.");
        }
    }
}