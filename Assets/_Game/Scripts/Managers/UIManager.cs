using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] PanelMainMenu _mainMenuPanel;

    [SerializeField] PanelGameplay _gameplayPanel;
    [SerializeField] PanelWin _winPanel;
    [SerializeField] PanelLose _losePanel;
    [SerializeField] PanelTargetMode _targetPanel;

    UI currentUI = UI.MainMenu;

    public Dictionary<UI, UIPanel> UIPanels;

    void Awake()
    {
        UIPanels = new Dictionary<UI, UIPanel>
        {
            { UI.MainMenu, _mainMenuPanel },
            { UI.Gameplay, _gameplayPanel },
            { UI.TargetMode, _targetPanel },
            { UI.Win, _winPanel },
            { UI.Lose, _losePanel }
        };
    }

    public void ChangeUI(UI type)
    {
        if (currentUI != type)
        {
            CloseUI(currentUI);
            currentUI = type;
        }
        OpenUI(currentUI);
    }

    public void OpenUI(UI type)
    {
        if (!IsOpen(type))
            GetPanel(type).gameObject.SetActive(true);
    }

    public void CloseUI(UI type)
    {
        GetPanel(type).Close();
    }

    public bool IsOpen(UI type)
    {
        return GetPanel(type).gameObject.activeInHierarchy;
    }

    public UIPanel GetPanel(UI type)
    {
        return UIPanels[type];
    }

    public void CloseAllUI()
    {
        foreach (var key in UIPanels.Keys)
        {
            CloseUI(key);
        }
    }
}

public enum UI
{
    MainMenu,
    Gameplay,
    TargetMode,
    Win,
    Lose
}