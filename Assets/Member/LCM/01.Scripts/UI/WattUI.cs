using System.Collections.Generic;
using Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Member.LCM._01.Scripts.UI.Text;
using Utility.Dependencies;
using Utility.ObjectPool.Runtime;

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
        [SerializeField] private float showWattConversionTime = 1f;
        [SerializeField] private List<Transform> wattPopupTextPos;
        private bool _isOpen = false;
        private int _previousWattAmount = 0;
        
        [Header("Pool")]
        [Inject] private PoolManagerMono _poolManager;

        [SerializeField] private PoolingItemSO popupText;

        private void Awake()
        {
            resourceChannel.AddListener<GetResourceEvent>(HandleChangeElectricity);
            wattConversionPanel.rectTransform.localScale = Vector3.zero;
        }

        private void OnDestroy()
        {
            resourceChannel.RemoveListener<GetResourceEvent>(HandleChangeElectricity);
        }

        private void HandleChangeElectricity(GetResourceEvent evt)
        {
            wattText.SetText($"{UnitConversion(evt.Electricity)}");
        }

        private string UnitConversion(int wattAmount)
        {
            if (_previousWattAmount > wattAmount)
            {
                _poolManager.Pop<PopupText>(popupText).Initialize(
                    wattPopupTextPos[2], wattPopupTextPos[3], $"-{_previousWattAmount - wattAmount}"
                    , Color.red); 
            }
            else
            {
                _poolManager.Pop<PopupText>(popupText).Initialize(
                    wattPopupTextPos[0], wattPopupTextPos[1], $"{wattAmount - _previousWattAmount}"
                    , Color.green);
            }
            
            _previousWattAmount = wattAmount;
            
            _index = 0;
            float wattValue = wattAmount;
            while (wattValue / 1000f >= 1)
            {
                wattValue /= 1000f;
                _index++;
            }
            
            Debug.Log(wattValue);
            
            return $"{wattValue:F}{wattUnits[_index]}";
        }

        public void ShowConversionPanel()
        {
            _isOpen = !_isOpen;
            if (_isOpen)
            {
                wattConversionPanel.rectTransform.DOScale(Vector3.one, showWattConversionTime).SetEase(Ease.OutBack);
            }
            else
            {
                wattConversionPanel.rectTransform.DOScale(Vector3.zero, showWattConversionTime).SetEase(Ease.InBack);
            }
        }
    }
}