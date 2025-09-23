using Core.Events;
using UnityEngine;

public class MapEvents
{
    public static readonly GridMaterialEvent GridMaterialEvent = new();
}
public class GridMaterialEvent : GameEvent
{
    public bool Enabled;
}
