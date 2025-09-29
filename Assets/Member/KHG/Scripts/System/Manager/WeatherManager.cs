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
        yield return new WaitForSeconds(Random.Range(1,1));
        ChangeWeather(RandomWeather());
        yield return new WaitForSeconds(Random.Range(4,5));
        ChangeWeather(Weather.Clean);
        StartCoroutine(WeatherCycle());
    }
    private Weather RandomWeather()
    {
        int index = Random.Range(0, (int)Weather.NULL);
        return (Weather)index;
    }
    private void ChangeWeather(Weather weather)
    {
        print("ÇöÀç³¯¾¾:" + weather);
        var evt = MapEvents.WeatherChangeEvent;
        evt.Weather = weather;

        mapChannel.RaiseEvent(evt);
    }
}
