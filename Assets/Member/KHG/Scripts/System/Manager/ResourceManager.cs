using Core.Events;
using UnityEngine;
using Utility.Unity.Common;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    [SerializeField] private GameEventChannelSO resourceChannel;
    [SerializeField] private GameEventChannelSO uiChannel;
    public int Population { get; private set; } = 5;
    public int Electricity { get; private set; } = 500;
    public int Money { get; private set; } = 100000;

    private EnergyManager _energyManager;
    private DelayInvoker<int> resourceRefresher;
    private bool _isBlackOut;
    
    [Space]
    [SerializeField] private int moneyPerPerson = 300;

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
        print(Electricity + arg.AddedElectricity < 0);
        if (Electricity + arg.AddedElectricity < 0)
        {
            _isBlackOut = true;
            uiChannel.RaiseEvent(UIEvents.SceneChangePanelEvent.Init(true,"BlackoutOver"));
            return;
        } 
        Electricity += arg.AddedElectricity;
        SendResource();
        
        print(Electricity);
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
        //Electricity = value;
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

    public void ClaimMoney()
    {
        Money += Population * moneyPerPerson;
        resourceChannel.RaiseEvent(ResourceEvents.MoneyEvent.Init(Money));
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
