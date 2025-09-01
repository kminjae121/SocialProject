using Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Member.LCM._01.Scripts.UI
{
    public class WattUI : MonoBehaviour
    {
        [Header("Event")]
        [SerializeField] private GameEventChannelSO resourceChannel;

        [Header("UI")] 
        [SerializeField] private Image wattConversion;
        [SerializeField] private TextMeshProUGUI wattText;

        private void Awake()
        {
            resourceChannel.AddListener<ElectricityEvent>(HandleChangeElectricity);
        }

        private void OnDestroy()
        {
            resourceChannel.RemoveListener<ElectricityEvent>(HandleChangeElectricity);
        }

        private void HandleChangeElectricity(ElectricityEvent evt)
        {
            wattText.SetText($"{UnitConversion(evt.Electricity)}");
        }

        private string UnitConversion(int wattAmount)
        {
            return "a";
        }
    }
}