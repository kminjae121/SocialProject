using Core.Events;
using UnityEngine;

public class ResourceEvents
{
    public static readonly ElectricityEvent ElectricityEvent = new();
    public static readonly PopulationEvent PopulationEvent = new();
    public static readonly SatisfactionEvent SatisfactionEvent = new();
    public static readonly GetResourceEvent GetResourceEvent = new();
}

public class ElectricityEvent : GameEvent
{
    public int Electricity; //-1이면 AddedElectricity를 사용
    public int AddedElectricity;
}

public class PopulationEvent : GameEvent
{
    public int Population;
    public int AddedPopulation;
}

public class SatisfactionEvent : GameEvent
{
    public int Satisfaction;
    public int AddedSatisfaction;
}

public class GetResourceEvent : GameEvent
{
    public int Electricity;
    public int Satisfaction;
    public int Population;
}
