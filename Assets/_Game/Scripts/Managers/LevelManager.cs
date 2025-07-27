using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Action OnLevelChange;
    public List<Board> levels;
    int currentLevel;
    public int CurrentLevel => currentLevel;

    public List<BoosterData> Boosters;
    void Awake()
    {
        BoosterManager.Ins.AddBooster(BOOSTER.UNLOCK_SLOT, new BoosterUnlockSlot());
        BoosterManager.Ins.AddBooster(BOOSTER.UNDO, new BoosterUndo());
        BoosterManager.Ins.AddBooster(BOOSTER.SHUFFLE, new BoosterShuffle());
        // PreLoadLevel();
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
        levels = Resources.LoadAll<Board>("Levels")
            .OrderBy(board => board.name)
            .ToList();
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