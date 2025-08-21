using Core.Events;
using UnityEngine;

public class ResourceEvents
{
    public static readonly ElectricityEvent ElectricityEvent = new();
    public static readonly PopulationEvent PopulationEvent = new();
    public static readonly SatisfactionEvent SatisfactionEvent = new();
}

public class ElectricityEvent : GameEvent
{
    public int Electricity;
}

public class PopulationEvent : GameEvent
{
    public int Population;
}

public class SatisfactionEvent : GameEvent
{
    public int Satisfaction;
}
