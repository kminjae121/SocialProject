using UnityEngine;

public class NuclrearObject : Factory
{
    private void Awake()
    {
        _minusEvent += HandleMinus;
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

    private void HandleMinus()
    {

    }
}
