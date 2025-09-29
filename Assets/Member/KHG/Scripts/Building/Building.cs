using System.Collections;
using Core.Events;
using System.Collections.Generic;
using KHG.Scripts.Managers;
using Member.LCM._01.Scripts.UI;
using UnityEngine;

namespace KHG.Scripts.Buildings
{
    public class Building : Structure, IConstruction
    {
        [SerializeField] private List<MeshRenderer> windows;
        [SerializeField] private BuildingSO currentBuilding;
        [SerializeField] private GameEventChannelSO resourceChannel;

        [SerializeField] private int reduceValue;
        
        private BuildingManager _buildingManager;
        
        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
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
            
            _buildingManager.structures.Add(this);
            StartCoroutine(UseElecticity());
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

        private IEnumerator UseElecticity()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                
                resourceChannel.RaiseEvent(ResourceEvents.ElectricityEvent.Initialize(-1,-reduceValue));
            }
        }
    }
}
