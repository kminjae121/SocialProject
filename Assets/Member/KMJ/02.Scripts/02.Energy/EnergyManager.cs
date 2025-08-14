using Core.Events;
using NUnit.Framework;
using System;
using UnityEngine;

public class EnergyManager : MonoSingleton<EnergyManager>, IEventable
{
    public float currentCityEnergy;

    private GameEventChannelSO _lightChannel;

    protected override void Awake()
    {
        base.Awake();
    }

    private void LightOff(TurnOffTheLight evt)
    {
        _lightChannel.RaiseEvent(LightEvent.lightEvent.Initialize(true));
    }

    private void LightTurnOn(TurnOffTheLight evt)
    {
        _lightChannel.RaiseEvent(LightEvent.lightEvent.Initialize(false));
    }

    public void GetEnergy(float energy) => currentCityEnergy += energy;

    public void MinusEnergyValue(float energy, float value) => energy -= value; 
    public void BadEvent()
    {
        
    }

    public void GoodEvent()
    {
        
    }

}
