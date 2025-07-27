using System;

public abstract class BaseBooster
{
    public abstract BOOSTER ID { get; }
    public abstract void Activate();
}

[Serializable]
public class BoosterData 
{
    public BOOSTER Id;
    public int unlockLevel;
}