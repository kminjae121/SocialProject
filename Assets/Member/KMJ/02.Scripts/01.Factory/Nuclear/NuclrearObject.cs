using UnityEngine;

public class NuclrearObjec : Factory, IEventable
{
    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
    }
    public void BadEvent()
    {
        Debug.Log("���� �����");
    }

    public void GoodEvent()
    {

    }
}
