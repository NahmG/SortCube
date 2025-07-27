public class BoosterShuffle : BaseBooster
{
    Stack target;

    public override BOOSTER ID => BOOSTER.SHUFFLE;

    public override void Activate()
    {
        if (target != null)
        {
            target.Shuffle();
            target = null;
        }
    }
}