using Core.Events;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    [SerializeField] private GameEventChannelSO resourceChannel;
    public int Population { get; private set; }
    public int Satisfaction { get; private set; }
    public int Electricity { get; private set; } //Wh

    private void Awake()
    {
        resourceChannel.AddListener<PopulationEvent>(HandlePopulation);
        resourceChannel.AddListener<SatisfactionEvent>(HandleSatisfaction);
        resourceChannel.AddListener<ElectricityEvent>(HandleElectricity);
    }

    private void HandlePopulation(PopulationEvent arg)
    {
        if (arg.Population != -1) Population = arg.Population;
        else Population += arg.AddedPopulation;
    }

    private void HandleSatisfaction(SatisfactionEvent arg)
    {
        if (arg.Satisfaction != -1) Satisfaction = arg.Satisfaction;
        else Satisfaction += arg.AddedSatisfaction;
    }
    private void HandleElectricity(ElectricityEvent arg)
    {
        print($"{arg.Electricity} == -1 : {arg.Electricity == -1}");
        if (arg.Electricity == -1) Satisfaction += arg.AddedElectricity;
        else Electricity = arg.Electricity;
    }

    private void SendResource()
    {
        var evt = ResourceEvents.GetResourceEvent;
        evt.Electricity = Electricity;
        evt.Satisfaction = Satisfaction;
        evt.Population = Population;

        resourceChannel.RaiseEvent(evt);
    }
    public void ReduceSatisfaction(int amount)
    {
        if (Satisfaction - amount < 0)
            return;

        Satisfaction -= amount;
    }

    public bool CanConstructionObject(int amount)
    {
        return Satisfaction - amount >= 0;
    }
}

public enum ResourceType
{
    Electricity,
    Population,
    Satisfaction
}
