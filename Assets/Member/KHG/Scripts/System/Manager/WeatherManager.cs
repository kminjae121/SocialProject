using Core.Events;
using UnityEngine;

public enum Weather
{
    Clean,
    Cloudy,
    Rain
}

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO mapChannel;

    private void ChangeWeather(Weather weather)
    {
        var evt = MapEvents.WeatherChangeEvent;
        evt.Weather = weather;

        mapChannel.RaiseEvent(evt);
    }
}
