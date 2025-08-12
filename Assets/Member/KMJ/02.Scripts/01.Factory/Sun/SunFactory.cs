using UnityEngine;

public class SunFactory : Factory, IEventable
{
    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
    }
    public void BadEvent()
    {
        Debug.Log("±²ÀåÈ÷ È¿À²ÀÌ ³·¾ÆÁü");
    }

    public void GoodEvent()
    {
        return;
    }
}
