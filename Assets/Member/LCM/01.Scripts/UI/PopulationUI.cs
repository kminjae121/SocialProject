using System.Collections.Generic;
using Core.Events;
using Member.LCM._01.Scripts.UI.Text;
using TMPro;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI
{
    public class PopulationUI : MonoBehaviour
    {
        [Header("Event")] [SerializeField] private GameEventChannelSO resourceChannel;

        [Header("Population UI")] [SerializeField]
        private TextMeshProUGUI populationText;

        [SerializeField] private List<Transform> populationPopupTextPos;
        [SerializeField] private GameObject popupText;

        private int _previousPopulation;

        private void Awake()
        {
            resourceChannel.AddListener<GetResourceEvent>(HandleChangePopulation);
        }

        private void OnDestroy()
        {
            resourceChannel.RemoveListener<GetResourceEvent>(HandleChangePopulation);
        }

        private void HandleChangePopulation(GetResourceEvent evt)
        {
            if(evt.Population == _previousPopulation) return;
            
            if (_previousPopulation > evt.Population)
            {
                Instantiate(popupText, transform).GetComponent<PopupText>().Initialize(
                    populationPopupTextPos[2], populationPopupTextPos[3], $"-{_previousPopulation - evt.Population}명"
                    , Color.red);
            }
            else
            {
                Instantiate(popupText, transform).GetComponent<PopupText>().Initialize(
                    populationPopupTextPos[0], populationPopupTextPos[1], $"+{evt.Population - _previousPopulation}명"
                    , Color.green);
            }

            populationText.SetText($"인구수 : {evt.Population}명 / {evt.Population}명");
            _previousPopulation = evt.Population;
        }
    }
}