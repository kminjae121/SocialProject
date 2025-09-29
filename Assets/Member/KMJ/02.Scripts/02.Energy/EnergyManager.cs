using Core.Events;
using NUnit.Framework;
using System;
using UnityEngine;

public class EnergyManager : MonoSingleton<EnergyManager>
{
    public float currentCityEnergy;

    private GameEventChannelSO _lightChannel;

    [SerializeField] private GameEventChannelSO _electricityEventChannel;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ResetEnergy();
    }

    private void ResetEnergy()
    {
        currentCityEnergy = 0;
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

        currentCityEnergy += (int)energy;
        
        print(_electricityEventChannel);
        _electricityEventChannel.RaiseEvent(ResourceEvents.ElectricityEvent.Initialize(-1,(int)energy));
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
