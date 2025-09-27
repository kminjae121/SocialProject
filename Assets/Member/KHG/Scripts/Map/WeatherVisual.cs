using Core.Events;
using System;
using UnityEngine;

public class WeatherVisual : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO mapChannel;

    private void Awake()
    {
        mapChannel.AddListener<WeatherChangeEvent>(HandleWeatherChange);
    }

    private void OnDestroy()
    {
        mapChannel.RemoveListener<WeatherChangeEvent>(HandleWeatherChange);
    }

    private void HandleWeatherChange(WeatherChangeEvent evt)
    {
        switch (evt.Weather)
        {
            case Weather.Rain:
                break;
            case Weather.Cloudy:
                break;
            case Weather.Clean:
                break;
            default:
                break;
        }
    }
}
