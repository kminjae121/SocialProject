using UnityEngine;

public class Cloudy : WeatherController
{
    [SerializeField] private Light globalLight;

    public override void SetActive(bool value)
    {
        if (value)
        {
            globalLight.intensity = 0f;
            return;
        }
        globalLight.intensity = 1f;
        gameObject.SetActive(false);
    }
}
