using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public List<List<CUBE>> cubeSets;
    public BoardData Data;
    public List<Stack> stacks;
    public Board boardPref;

    [Button("Create Board")]
    void CreateBoard()
    {
        cubeSets = new List<List<CUBE>>();
        var cubeList = new List<CUBE>();
        foreach (var kvp in Data.cubesCounts)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                cubeList.Add(kvp.Key);
            }
        }
        var rng = new System.Random();
        int stackCount = Data.stackCount;

        // Ensure cubeSets count matches cubeList count if cubeList.Count == stackCount
        if (cubeList.Count == stackCount)
        {
            // Initialize cubeSets
            for (int i = 0; i < stackCount; i++)
            {
                cubeSets.Add(new List<CUBE>());
            }
            // Assign each cube to a random stack, ensuring no stack exceeds 4 cubes
            foreach (var cube in cubeList)
            {
                int attempts = 0;
                while (attempts < 10000)
                {
                    int idx = rng.Next(stackCount);
                    if (cubeSets[idx].Count < 4)
                    {
                        cubeSets[idx].Add(cube);
                        break;
                    }
                    attempts++;
                }
            }
        }
    }
}

[CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObject/Data/BoardData")]
public class BoardData : SerializedScriptableObject
{
    public int stackCount;
    public bool addLockStack;
    public Dictionary<CUBE, int> cubesCounts = new();
}
