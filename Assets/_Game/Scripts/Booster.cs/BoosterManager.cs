
using System;
using System.Collections.Generic;

public class BoosterManager
{
    Dictionary<BOOSTER, IBooster> boosters;
    public Dictionary<BOOSTER, IBooster> Boosters => boosters;

    public void AddBooster(BOOSTER id, IBooster booster)
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

    public IBooster GetBooster(BOOSTER id)
    {
        if (!boosters.ContainsKey(id))
        {
            AddBooster(id, null);
        }
        return boosters[id];
    }
}

public enum BOOSTER
{
    NONE = 0,
    UNLOCK_SLOT = 1,
    UNDO = 2,
    SHUFFLE_STACK = 3
}