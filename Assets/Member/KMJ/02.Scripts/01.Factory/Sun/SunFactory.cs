using UnityEngine;

public class SunFactory : Factory, IEventable
{
    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
    }
    public void BadEvent()
    {
        Debug.Log("������ ȿ���� ������");
    }

    public void GoodEvent()
    {
        return;
    }
}
