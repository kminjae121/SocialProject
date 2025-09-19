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
    public int Electricity; //-1�̸� AddedElectricity�� ���
    public int AddedElectricity;

    public ElectricityEvent Initialize(int SetValue = -1,int AddValue = 0)
    {
        Electricity = SetValue;
        AddedElectricity = AddValue;

        return this;
    }
}

public class PopulationEvent : GameEvent
{
    public int CurrentPopulation;
    public int MaxPopulation;
    public int AddedPopulation;

    public PopulationEvent Initialize(int SetValue = -1, int MaxValue = -1, int AddValue = 0)
    {
        CurrentPopulation = SetValue;
        MaxPopulation = MaxValue;
        AddedPopulation = AddValue;

        return this;
    }
}

public class SatisfactionEvent : GameEvent
{
    public int Satisfaction;
    public int AddedSatisfaction;

    public SatisfactionEvent Initialize(int SetValue = -1, int AddValue = 0)
    {
        Satisfaction = SetValue;
        AddedSatisfaction = AddValue;

        return this;
    }
}

public class GetResourceEvent : GameEvent
{
    public int Electricity;
    public int Satisfaction;
    public int Population;
}
