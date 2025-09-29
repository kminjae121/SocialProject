using Core.Events;
using System;
using UnityEngine;

public class WeatherVisual : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO mapChannel;
    [Space] 
    [SerializeField] private WeatherController rainObj;
    [SerializeField] private WeatherController clodyObj;

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
                Rain();
                break;
            case Weather.Cloudy:
                Cloudy();
                break;
            case Weather.Clean:
                Clear();
                break;
            default:
                break;
        }
    }

    private void Rain()
    {
        rainObj.gameObject.SetActive(true);
        clodyObj.gameObject.SetActive(true);
        rainObj.SetActive(true);
        clodyObj.SetActive(true);
    }

    private void Cloudy()
    {
        rainObj.SetActive(false);
        clodyObj.gameObject.SetActive(true);
        clodyObj.SetActive(true);
    }

    private void Clear()
    {
        rainObj.SetActive(false);
        clodyObj.SetActive(false);
    }
}
