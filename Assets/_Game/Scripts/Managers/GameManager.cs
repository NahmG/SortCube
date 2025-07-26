using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState _state;

    public void ChangeState(GameState newState)
    {
        _state = newState;

        switch (newState)
        {

        }
    }
}

public enum GameState
{
    MainMenu,
    Gameplay,
    Win,
    Lose,
}