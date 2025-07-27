using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Action OnLevelChange;
    List<Board> levels;
    int currentLevel;
    public int CurrentLevel => currentLevel;

    public List<BoosterData> Boosters;
    void Awake()
    {
        BoosterManager.Ins.AddBooster(BOOSTER.UNLOCK_SLOT, new BoosterUnlockSlot());
        BoosterManager.Ins.AddBooster(BOOSTER.UNDO, new BoosterUndo());
        PreLoadLevel();
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        currentLevel = 1;
        OnLevelChange?.Invoke();

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

        OnLevelChange?.Invoke();
    }
}