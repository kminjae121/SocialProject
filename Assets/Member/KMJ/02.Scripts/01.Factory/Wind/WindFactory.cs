using UnityEngine;

public class WindFactory : Factory
{

    private void Awake()
    {
        _minusEvent += HandleMinus;
        _fixEvent += HandleFix;
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
                return;
            }
                break;
            case Weather.Rain:
            {
                _increaseEnergy -= 20;
            }
                break;
        }
    }
    
    protected override void MakingEnergy()
    {
        EnergyManager.Instance.GetEnergy(_increaseEnergy);
    }

    private void HandleMinus()
    {
        MinusIncreaseEnergy();
    }
    private void HandleFix()
    {
        PlusIncreaseEnergy();
    }


}
