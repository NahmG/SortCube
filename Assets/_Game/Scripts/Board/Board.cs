using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<Stack> stacks = new();
    [SerializeField] Stack stackPref;
    [SerializeField] LockStack lockStackPref;

    Transform _root;
    Stack source;
    Stack target;
    List<Cube> cubeToMove = new();

    #region EDITOR
#if UNITY_EDITOR
    public void AddStack(List<CUBE> cubeSet)
    {
        Stack stack = Instantiate(stackPref, transform);
        stack.SetCubeSet(cubeSet);

        stacks.Add(stack);
    }

    public void AddLockStack()
    {
        LockStack stack = Instantiate(lockStackPref, transform);
        stacks.Add(stack);
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
    #endregion
    #region LAYOUT
    [Header("Layout")]
    public int columns = 4;           // Number of columns
    public float spacingX = 1.5f;     // Horizontal spacing
    public float spacingY = .3f;     // Vertical spacing
    
    [Button("Layout")]
    void Layout()
    {
        int totalCount = stacks.Count;
        int rows = Mathf.CeilToInt((float)totalCount / columns);

        // Offset to center grid around origin
        Vector3 offset = new Vector3(
            (columns - 1) * spacingX * 0.5f,
            -(rows - 1) * spacingY * 0.5f,
            0);

        for (int i = 0; i < totalCount; i++)
        {
            int row = i / columns;
            int col = i % columns;

            Vector3 origin = transform.position;
            Vector3 localPos = new Vector3(col * spacingX, -row * spacingY, 0);
            Vector3 worldPos = origin + localPos - offset;

            stacks[i].transform.position = worldPos;
        }
    }

    #endregion
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
            GameplayManager.Ins.CheckWin();
        }
    }
}