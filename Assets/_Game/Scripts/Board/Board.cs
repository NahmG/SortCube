using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Sirenix.OdinInspector;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<Stack> stacks = new();
    Stack stackPref;
    LockStack lockStackPref;

    Stack source;
    Stack target;
    List<Cube> cubeToMove = new();

#if UNITY_EDITOR
    [Button("Add Stack")]
    public void AddStack()
    {
        if (stackPref == null)
        {
            stackPref = Resources.Load<Stack>("Board/Stack");

        }
        Stack stack = Instantiate(stackPref, transform);
        stacks.Add(stack);
    }

    [Button("Add Lock Stack")]
    public void AddLockStack()
    {
        if (lockStackPref == null)
        {
            lockStackPref = Resources.Load<LockStack>("Board/LockStack");
        }
        LockStack stack = Instantiate(lockStackPref, transform);
        stacks.Add(stack);
    }

    [Button("ClearBoard")]
    public void ClearBoard()
    {
        stacks.Clear();
    }

    [Button("FillBoard")]
    public void FillBoard()
    {
        stacks = transform.GetComponentsInChildren<Stack>().ToList();
    }

    [Button("Save")]
    public void SaveAsPrefab()
    {
        string folderPath = CONSTANTS.LEVEL_FOLDER;
        string baseName = gameObject.name;
        string prefabName = baseName;
        string prefabPath = $"{folderPath}/{prefabName}.prefab";

        // Create folder if needed
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogWarning($"Path {folderPath} Invalid!");
            return;
        }

        // Find available name
        int counter = 0;
        while (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
        {
            prefabName = $"{baseName}_{counter}";
            prefabPath = $"{folderPath}/{prefabName}.prefab";
            counter++;
        }

        // Save the prefab
        PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, prefabPath, InteractionMode.UserAction);
        Debug.Log($"Prefab saved as: {prefabName}");
    }
#endif

    public void OnInit()
    {
        foreach (Stack s in stacks)
        {
            s.Init(this);
        }
    }

    public void OnStackSelected(Stack stack)
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
        if (stacks.Where(s => !s.IsEmpty).All(s => s.IsComplete))
        {
            Debug.Log("Level Complete.");
        }
    }
}