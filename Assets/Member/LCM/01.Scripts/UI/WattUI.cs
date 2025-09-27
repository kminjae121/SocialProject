using System.Collections.Generic;
using Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Member.LCM._01.Scripts.UI.Text;

namespace Member.LCM._01.Scripts.UI
{
    public class WattUI : MonoBehaviour
    {
        [Header("Event")] 
        [SerializeField] private GameEventChannelSO resourceChannel;

        [Header("Watt Info UI")] [SerializeField]
        private TextMeshProUGUI wattText;

        [SerializeField] private List<string> wattUnits;
        private int _index;

        [Header("Watt Conversion UI")] [SerializeField]
        private Image wattConversionPanel;

        [SerializeField] private float showWattConversionTime = 1f;
        [SerializeField] private List<Transform> wattPopupTextPos;
        [SerializeField] private GameObject popupText;
        private bool _isOpen;
        private int _previousWattAmount;


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
            if(Mathf.Approximately(_previousWattAmount, 0f)) return;
            
            wattText.SetText($"{UnitConversion(evt.Electricity)}");
        }

        private string UnitConversion(int wattAmount)
        {
            if (_previousWattAmount > wattAmount)
            {
                Instantiate(popupText, transform).GetComponent<PopupText>().Initialize(
                    wattPopupTextPos[2], wattPopupTextPos[3], $"-{WattConversion(_previousWattAmount - wattAmount)}"
                    , Color.red);
            }
            else
            {
                Instantiate(popupText, transform).GetComponent<PopupText>().Initialize(
                    wattPopupTextPos[0], wattPopupTextPos[1], $"+{WattConversion(wattAmount - _previousWattAmount)}"
                    , Color.green);
            }

            _previousWattAmount = wattAmount;
            return $"{WattConversion(wattAmount)}";
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

        private string WattConversion(int wattAmount)
        {
            _index = 0;
            float wattValue = wattAmount;
            while (wattValue / 1000f >= 1)
            {
                wattValue /= 1000f;
                _index++;
            }

            return $"{wattValue:F}{wattUnits[_index]}";
        }
    }
}