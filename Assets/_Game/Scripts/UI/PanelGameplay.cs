using System.Collections.Generic;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameplay : UIPanel
{
    [SerializeField] TMP_Text levelText;

    [OdinSerialize]
    Dictionary<BOOSTER, Button> buttons = new();

    void OnEnable()
    {
        CheckUnlockBooster();
    }

    void Update()
    {
        levelText.text = LevelManager.Ins.CurrentLevel.ToString();
    }

    public void UnlockBooster(BOOSTER Id)
    {
        buttons[Id].gameObject.SetActive(true);
    }

    public void LockBooster(BOOSTER Id)
    {
        buttons[Id].gameObject.SetActive(false);
    }

    public void CheckUnlockBooster() {
        foreach (BoosterData data in LevelManager.Ins.Boosters)
        {
            if (LevelManager.Ins.CurrentLevel >= data.unlockLevel)
            {
                UnlockBooster(data.Id);
            }
            else
            {
                LockBooster(data.Id);
            }
        }
    }
}