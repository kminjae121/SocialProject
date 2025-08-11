using UnityEngine;

public class WindFactory : Factory, IEventable
{
    protected override void MakingEnergy()
    {
        base.MakingEnergy();
    }
    public void BadEvent()
    { 
    }

    public void GoodEvent()
    {
    }
}
