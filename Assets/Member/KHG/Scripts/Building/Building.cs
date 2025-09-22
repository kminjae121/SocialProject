using Core.Events;
using System.Collections.Generic;
using UnityEngine;

namespace KHG.Scripts.Buildings
{
    public class Building : Structure, IConstruction
    {
        [SerializeField] private List<MeshRenderer> windows;
        [SerializeField] private BuildingSO currentBuilding;
        [SerializeField] private GameEventChannelSO resourceChannel;


        public override void SetActive(bool value)
        {
            foreach (var win in windows)
                win.enabled = value;
        }

        public void StartConstruction()
        {
        }

        public void StopContruction()
        {
        }

        public void AddBuildingPoplulation()
        {
            var evt = ResourceEvents.PopulationEvent;
            evt.CurrentPopulation = -1;
            evt.AddedPopulation = currentBuilding.Population;

            resourceChannel.RaiseEvent(evt);
        }
    }
}
