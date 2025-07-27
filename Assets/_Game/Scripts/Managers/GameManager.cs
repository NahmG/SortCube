using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState currentState;

    void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                UIManager.Ins.ChangeUI(UI.MainMenu);
                break;
            case GameState.Gameplay:
                UIManager.Ins.ChangeUI(UI.Gameplay);
                GameplayManager.Ins.Load();
                break;
            case GameState.Win:
                UIManager.Ins.ChangeUI(UI.Win);
                break;
            case GameState.Lose:
                UIManager.Ins.ChangeUI(UI.Lose);
                break;
            case GameState.TargetMode:
                
                break;
        }
    }
}

public enum GameState
{
    MainMenu,
    Gameplay,
    TargetMode,
    Win,
    Lose,
}