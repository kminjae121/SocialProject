using UnityEngine;

public class Rain : WeatherController
{
    public override void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
