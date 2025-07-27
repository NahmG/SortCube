using System;

public interface IBooster
{
    public abstract BOOSTER Type { get; }
    void Activate();
}