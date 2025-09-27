using Core.Events;
using UnityEngine;
using Utility.Unity.Common;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    [SerializeField] private GameEventChannelSO resourceChannel;
    public int Population { get; private set; }
    public int Electricity { get; private set; }
    public int Money { get; private set; }

    private EnergyManager _energyManager;
    private DelayInvoker<int> resourceRefresher;

    protected override void Awake()
    {
        resourceChannel.AddListener<PopulationEvent>(HandlePopulation);
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
    private void HandleElectricity(ElectricityEvent arg)
    {
        if (arg.Electricity == -1) Electricity += arg.AddedElectricity;
        else Electricity = arg.Electricity;
        SendResource();
    }

    private void SendResource()
    {
        var evt = ResourceEvents.GetResourceEvent;
        evt.Electricity = Electricity;
        evt.Population = Population;

        resourceChannel.RaiseEvent(evt);
    }
    private void SendMoney()
    {
        var evt = ResourceEvents.GetMoneyEvent;
        evt.Money = Money;

        resourceChannel.RaiseEvent(evt);
    }

    private void RefreshResource(int value)
    {
        Electricity = value;
        SendResource();
    }
    private void Update()
    {
        resourceRefresher.Tick();
    }
    public void ReduceSatisfaction(int amount)
    {
        if (Money - amount < 0)
            return;

        Money -= amount;
        SendMoney();
    }

    public bool CanConstructionObject(int amount)
    {
        return Money - amount >= 0;
    }
}

public enum ResourceType
{
    Electricity,
    Population,
    Satisfaction
}
