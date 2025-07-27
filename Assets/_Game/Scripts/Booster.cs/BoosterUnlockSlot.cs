using UnityEngine;

public class BoosterUnlockSlot : BaseBooster
{
    public override BOOSTER ID => BOOSTER.UNLOCK_SLOT;
    LockStack target;

    public void SetTarget(LockStack target)
    {
        this.target = target;
    }

    public override void Activate()
    {
        target.UnlockSlot();
    }


}