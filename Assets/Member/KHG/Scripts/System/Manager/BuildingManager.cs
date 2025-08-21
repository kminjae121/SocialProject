using Core.Events;
using KHG.Scripts.Buildings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KHG.Scripts.Managers
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO buildingChannel;

        public int MaxPopulation { get; private set; }
        public List<Building> buildings;

        private void Awake()
        {
            buildingChannel.AddListener<TurnOffTheLight>(ManageLight);
        }

        private void ManageLight(TurnOffTheLight light)
        {
            StartCoroutine(ChangeLight(light.isTurnOff == false));
        }

        private IEnumerator ChangeLight(bool value)
        {
            foreach (var building in buildings)
            {
                building.SetActive(value);
                yield return new WaitForSeconds(0.025f);
            }
        }
    }
}