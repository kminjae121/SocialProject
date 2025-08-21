using Core.Events;
using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
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
        Population = arg.Population;
    }

    private void HandleSatisfaction(SatisfactionEvent arg)
    {
        Satisfaction = arg.Satisfaction;
    }

    private void HandleElectricity(ElectricityEvent arg)
    {
        Electricity = arg.Electricity;
    }
}
