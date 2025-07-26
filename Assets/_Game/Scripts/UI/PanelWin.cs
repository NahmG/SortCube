using UnityEngine;
using UnityEngine.UI;

public class PanelWin : UIPanel
{
    [SerializeField] Button Replay;
    [SerializeField] Button Next;

    void Awake()
    {
        Replay.onClick.AddListener(ClickReplay);
        Next.onClick.AddListener(ClickNext);
    }

    public void ClickReplay()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    public void ClickNext()
    {
        LevelManager.Ins.NextLevel();
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }
}