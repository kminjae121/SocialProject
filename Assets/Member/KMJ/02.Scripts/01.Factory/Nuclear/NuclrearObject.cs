using UnityEngine;

public class NuclrearObjec : Factory, IEventable
{
    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
    }
    public void BadEvent()
    {
        Debug.Log("방사능 유출됨");
    }

    public void GoodEvent()
    {

    }
}
