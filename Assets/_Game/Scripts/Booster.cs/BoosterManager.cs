
using System;
using System.Collections.Generic;

public class BoosterManager : Singleton<BoosterManager>
{
    Dictionary<BOOSTER, BaseBooster> boosters = new();
    public Dictionary<BOOSTER, BaseBooster> Boosters => boosters;

    public void AddBooster(BOOSTER id, BaseBooster booster)
    {
        if (!boosters.ContainsKey(id))
        {
            boosters.Add(id, booster);
        }
    }

    public void RemoveBooster(BOOSTER id)
    {
        if (boosters.ContainsKey(id))
        {
            boosters.Remove(id);
        }
    }
    
    public void ActivateBooster(BOOSTER id)
    {
        boosters[id]?.Activate();
    }

    public T GetBooster<T>(BOOSTER id) where T:BaseBooster
    {
        if (!boosters.ContainsKey(id))
        {
            AddBooster(id, null);
        }
        return boosters[id] as T;
    }
}

public enum BOOSTER
{
    NONE = 0,
    UNLOCK_SLOT = 1,
    UNDO = 2,
    SHUFFLE = 3
}