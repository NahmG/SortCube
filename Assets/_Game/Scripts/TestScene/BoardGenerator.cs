using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class BoardGenerator : SerializedMonoBehaviour
{
    [OdinSerialize] 
    public Dictionary<CUBE, int> cubesCounts = new();
    public int stackCount;
    public bool addLockStack;
    public Board boardPref;
    Board board;

    [OdinSerialize] 
    public List<List<CUBE>> cubeSets;

    [Button("Create Board")]
    void CreateBoard()
    {
        GenCubeSets();
        //add new board
        board = Instantiate(boardPref, transform);
        board.gameObject.name = "Board";
        for (int i = 0; i < stackCount; i++)
        {
            board.AddStack(cubeSets[i]);
        }
        if (addLockStack)
        {
            board.AddLockStack();
        }
    }

    void GenCubeSets()
    {
        cubeSets = new List<List<CUBE>>();
        var cubeList = new List<CUBE>();
        foreach (var kvp in cubesCounts)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                cubeList.Add(kvp.Key);
            }
        }
        var rng = new System.Random();

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

