using System.Collections.Generic;
using Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Member.LCM._01.Scripts.UI
{
    public class WattUI : MonoBehaviour
    {
        [Header("Event")]
        [SerializeField] private GameEventChannelSO resourceChannel;

        [Header("Watt Info UI")] 
        [SerializeField] private TextMeshProUGUI wattText;
        [SerializeField] private List<string> wattUnits;
        private int _index = 0;

        [Header("Watt Conversion UI")] 
        [SerializeField] private Image wattConversionPanel;
        [SerializeField] private float showWattConversionSpeed = 1f;
        private bool _isOpen = false;

        private void Awake()
        {
            resourceChannel.AddListener<ElectricityEvent>(HandleChangeElectricity);
            wattConversionPanel.rectTransform.localScale = Vector3.zero;
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
            _index = 0;
            float wattValue = wattAmount;
            while (wattValue / 1000f >= 1)
            {
                wattValue /= 1000f;
                _index++;
            }
            
            Debug.Log(wattValue);
            
            return $"현재 : {wattValue:F}{wattUnits[_index]}";
        }

        public void ShowConversionPanel()
        {
            if (_isOpen)
            {
                wattConversionPanel.rectTransform.DOScale(Vector3.one, showWattConversionSpeed).SetEase(Ease.OutBack);
            }
            else
            {
                wattConversionPanel.rectTransform.DOScale(Vector3.zero, showWattConversionSpeed).SetEase(Ease.InBack);
            }
            _isOpen = !_isOpen;
        }
    }
}