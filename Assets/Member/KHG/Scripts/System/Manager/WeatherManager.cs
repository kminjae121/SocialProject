using Core.Events;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum Weather
{
    Clean,
    Cloudy,
    Rain,
    NULL
}

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO mapChannel;

    private void OnEnable()
    {
        StartWeather();
    }

    private void StartWeather()
    {
        StartCoroutine(WeatherCycle());
    }
    private IEnumerator WeatherCycle()
    {
        yield return new WaitForSeconds(Random.Range(30,180));
        ChangeWeather(RandomWeather());
        yield return new WaitForSeconds(Random.Range(30,120));
        ChangeWeather(Weather.Clean);
    }
    private Weather RandomWeather()
    {
        int index = Random.Range(0, (int)Weather.NULL - 1);
        return (Weather)index;
    }
    private void ChangeWeather(Weather weather)
    {
        var evt = MapEvents.WeatherChangeEvent;
        evt.Weather = weather;

        mapChannel.RaiseEvent(evt);
    }
}
