using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region LOAD
    List<Board> boards;
    int level;
    Board currentLevel;
    void OnLoadLevel(int level) { }
    #endregion

    #region PLAY
    public Action OnSlotUnlock;
    void SpawnBoard() { }
    //Board board

    #endregion

    #region WIN_CONDITION
    
    #endregion

    #region Button

    #endregion
}