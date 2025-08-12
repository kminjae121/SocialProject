using UnityEngine;

public class WindFactory : Factory, IEventable
{
    
    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
        base.MakingEnergy();
    }

    public void BadEvent()
    {
        Debug.Log("�μ���");
    }

    public void GoodEvent()
    {
        return;
    }
}
