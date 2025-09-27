using System;
using UnityEngine;

public class SunFactory : Factory
{
    private void Awake()
    {
        _minusEvent += HandleMinus;
        _fixEvent += HandleFix;
        _brokeEvent += HandleBroke;
    }


    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
    }
    
    protected override void WeatherCondition(WeatherChangeEvent evt)
    {
        switch (evt.Weather)
        {
            case Weather.Clean:
            {
                return;
            }
                break;
            case Weather.Cloudy:
            {
                
            }
                break;
            case Weather.Rain:
            {
                
            }
                break;
        }
    }
    
    private void HandleBroke()
    {
        gameObject.SetActive(false);
    }

    private void HandleFix()
    {
        PlusIncreaseEnergy();
    }

    private void HandleMinus()
    {
        MinusIncreaseEnergy();
    }
}
