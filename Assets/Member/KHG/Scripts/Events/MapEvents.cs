using Core.Events;
using UnityEngine;

public class MapEvents
{
    public static readonly GridMaterialEvent GridMaterialEvent = new();
    public static readonly WeatherChangeEvent WeatherChangeEvent = new();
    public static readonly GameOverEvent GameOverEvent = new();
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

public class GameOverEvent : GameEvent
{
    public GameOverType overType;

    public GameOverEvent Init(GameOverType type)
    {
        overType = type;
        return this;
    }
}

public class WeatherChangeEvent : GameEvent
{
    public Weather Weather;
}
