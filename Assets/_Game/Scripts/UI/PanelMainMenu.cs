using UnityEngine;
using UnityEngine.UI;

public class PanelMainMenu : UIPanel
{
    [SerializeField] Button PlayButton;

    void Awake()
    {
        PlayButton.onClick.AddListener(ClickPlay);
    }

    public void ClickPlay()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }
}