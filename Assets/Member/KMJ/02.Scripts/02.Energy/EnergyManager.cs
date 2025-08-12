using NUnit.Framework;
using UnityEngine;

public class EnergyManager : MonoSingleton<EnergyManager>, IEventable
{
    public float currentCityEnergy;
    
   
    protected override void Awake()
    {
        base.Awake();
    }

    public void GetEnergy(float energy) => currentCityEnergy += energy;

    public void BadEvent()
    {
        
    }

    public void GoodEvent()
    {
        
    }
}
