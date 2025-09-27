using Core.Events;
using UnityEngine;

public class MapEvents
{
    public static readonly GridMaterialEvent GridMaterialEvent = new();
    public static readonly WeatherChangeEvent WeatherChangeEvent = new();
}
public class GridMaterialEvent : GameEvent
{
    public bool Enabled;

    public GridMaterialEvent Initialize(bool enable)
    {
        Enabled = enable;
        return this;
    }
}
public class WeatherChangeEvent : GameEvent
{
    public Weather Weather;
}
