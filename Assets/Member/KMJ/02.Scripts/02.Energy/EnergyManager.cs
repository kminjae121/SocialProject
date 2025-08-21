using Core.Events;
using NUnit.Framework;
using System;
using UnityEngine;

public class EnergyManager : MonoSingleton<EnergyManager>
{
    public float currentCityEnergy;

    private GameEventChannelSO _lightChannel;

    protected override void Awake()
    {
        base.Awake();
    }

    private void LightOff()
    {
        _lightChannel.RaiseEvent(LightEvent.lightEvent.Initialize(true));
    }

    private void LightTurnOn()
    {
        _lightChannel.RaiseEvent(LightEvent.lightEvent.Initialize(false));
    }

    public void GetEnergy(float energy)
    {
        if(currentCityEnergy < 0)
        {
            LightTurnOn();
        }

        currentCityEnergy += energy;
    }

    public void MinusEnergyValue(float value)
    {
        currentCityEnergy -= value; 

        if(currentCityEnergy <= 0)
        {
            currentCityEnergy = 0;
            LightOff();
        }
    }
}
