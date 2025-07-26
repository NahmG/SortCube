using TMPro;
using UnityEngine;

public class PanelGameplay : UIPanel
{
    [SerializeField] TMP_Text levelText;


    void Update()
    {
        levelText.text = LevelManager.Ins.CurrentLevel.ToString();   
    }
}