using Core.Events;
using System.Collections.Generic;
using Member.LCM._01.Scripts.UI;
using UnityEngine;

namespace KHG.Scripts.Buildings
{
    public class Building : Structure, IConstruction
    {
        [SerializeField] private List<MeshRenderer> windows;
        [SerializeField] private BuildingSO currentBuilding;
        [SerializeField] private GameEventChannelSO resourceChannel;
        
        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }
        public override void SetActive(bool value)
        {
            foreach (var win in windows)
                win.enabled = value;
        }

        public void SetEnable(bool value)
        {
            foreach (Transform child in transform)
                if(child.TryGetComponent(out MeshRenderer renderer) == true)
                    renderer.enabled = value;
            if(_renderer != null)
                _renderer.enabled = value;
            
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
