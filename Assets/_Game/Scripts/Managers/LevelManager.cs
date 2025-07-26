using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    List<Board> levels;
    int currentLevel;
    public int CurrentLevel => currentLevel;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        PreLoadLevel();
        currentLevel = 1;
    }

    void PreLoadLevel()
    {
        levels = Resources.LoadAll<Board>("Levels").ToList();
    }

    public Board GetLevel()
    {
        return CurrentLevel > 1 ? levels[CurrentLevel - 1] : levels[0];
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel > levels.Count)
            currentLevel = 1;
    }

}