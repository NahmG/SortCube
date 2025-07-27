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

    #region GAMEPLAY

    

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