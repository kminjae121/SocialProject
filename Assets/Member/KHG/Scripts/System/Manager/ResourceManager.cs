using Core.Events;
using UnityEngine;
using Utility.Unity.Common;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    [SerializeField] private GameEventChannelSO resourceChannel;
    public int Population { get; private set; }
    [field: SerializeField] public int Satisfaction { get; private set; }
    public int Electricity { get; private set; }

    private EnergyManager _energyManager;
    private DelayInvoker<int> resourceRefresher;

    private void Awake()
    {
        resourceChannel.AddListener<PopulationEvent>(HandlePopulation);
        resourceChannel.AddListener<SatisfactionEvent>(HandleSatisfaction);
        resourceChannel.AddListener<ElectricityEvent>(HandleElectricity);
    }

    private void Start()
    {
        _energyManager = EnergyManager.Instance;

        resourceRefresher = new(RefreshResource, (int)_energyManager.currentCityEnergy, 3);
    }

    private void HandlePopulation(PopulationEvent arg)
    {
        if (arg.CurrentPopulation != -1) Population = arg.CurrentPopulation;
        else Population += arg.AddedPopulation;
        SendResource();
    }

    private void HandleSatisfaction(SatisfactionEvent arg)
    {
        if (arg.Satisfaction != -1) Satisfaction = arg.Satisfaction;
        else Satisfaction += arg.AddedSatisfaction;
        SendResource();
    }
    private void HandleElectricity(ElectricityEvent arg)
    {
        print($"{arg.Electricity} == -1 : {arg.Electricity == -1}");
        if (arg.Electricity == -1) Electricity += arg.AddedElectricity;
        else Electricity = arg.Electricity;
        SendResource();
    }

    private void SendResource()
    {
        var evt = ResourceEvents.GetResourceEvent;
        evt.Electricity = Electricity;
        evt.Satisfaction = Satisfaction;
        evt.Population = Population;

        resourceChannel.RaiseEvent(evt);
    }

    private void RefreshResource(int value)
    {
        print($"before energy : {Electricity}, after energy : {value}");
        Electricity = value;
        SendResource();
    }
    private void Update()
    {
        resourceRefresher.Tick();
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
