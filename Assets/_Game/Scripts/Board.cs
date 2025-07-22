using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Stack[] stacks;

    Stack stack1;
    Stack stack2;


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
        if (stack1 == null)
        {
            stack1 = stack;
            Debug.Log("stack1: " + stack);
        }
        else if (stack2 == null)
        {
            stack2 = stack;
            Debug.Log("stack2: " + stack);
            
            if (stack1 != stack2)
                Sort();
        }
    }

    void Sort()
    {
        if (!stack1.IsEmpty && !stack2.IsFull)
        {
            Cube cube = stack1.Pop();
            stack2.Push(cube);
        }
        ResetInput();
    }

    void ResetInput()
    {
        stack1 = null;
        stack2 = null;
    }
}