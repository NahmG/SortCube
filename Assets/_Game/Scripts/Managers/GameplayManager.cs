using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    Board currentLevelPref;
    Board CurrentLevel;

    public void Load()
    {
        currentLevelPref = LevelManager.Ins.GetLevel();

        if (CurrentLevel != null)
        {
            Destroy(CurrentLevel.gameObject);
        }
        CurrentLevel = Instantiate(currentLevelPref, transform);
        CurrentLevel.OnInit();
    }

    #region TargetMode

    Stack target;
    public Action OnTargetSelected;
    public bool IsTargetMode;
    BoosterShuffle boosterShuffle;

    public void EnterTargetMode()
    {
        CurrentLevel.ResetInput();
        IsTargetMode = true;

        UIManager.Ins.ChangeUI(UI.TargetMode);
        boosterShuffle = BoosterManager.Ins.GetBooster<BoosterShuffle>(BOOSTER.SHUFFLE);
    }

    public void OnTargetSelect(Stack stack)
    {
        boosterShuffle.SetTarget(stack);
        boosterShuffle.Activate();

        ExitTargetMode();
    }

    public void ExitTargetMode()
    {
        IsTargetMode = false;
        UIManager.Ins.ChangeUI(UI.Gameplay);
    }

    #endregion

    #region WIN
    public void CheckWin()
    {
        GameManager.Ins.ChangeState(GameState.Win);
    }
    #endregion

    #region BUTTON
    public Action OnSlotUnlock;
    #endregion
}