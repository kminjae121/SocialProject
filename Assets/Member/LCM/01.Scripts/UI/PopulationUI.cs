using System.Collections.Generic;
using Core.Events;
using Member.LCM._01.Scripts.UI.Text;
using TMPro;
using UnityEngine;
using Utility.Dependencies;
using Utility.ObjectPool.Runtime;

namespace Member.LCM._01.Scripts.UI
{
    public class PopulationUI : MonoBehaviour
    {
        [Header("Event")]
        [SerializeField] private GameEventChannelSO resourceChannel;

        [Header("Population UI")]
        [SerializeField] private TextMeshProUGUI populationText;

        [SerializeField] private List<Transform> populationPopupTextPos;

        [Header("Pool")]
        [Inject] private PoolManagerMono _poolManager;

        [SerializeField] private PoolingItemSO popupText;
        
        
        private int _previousPopulation = 0;

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
            if (_previousPopulation > evt.Population)
            {
                _poolManager.Pop<PopupText>(popupText).Initialize(
                    populationPopupTextPos[2], populationPopupTextPos[3], $"-{_previousPopulation - evt.Population}"
                    , Color.red);
            }
            else
            {
                _poolManager.Pop<PopupText>(popupText).Initialize(
                    populationPopupTextPos[0], populationPopupTextPos[1], $"{evt.Population - _previousPopulation}"
                    , Color.green);
            }
            
            populationText.SetText($"인구수 : {evt.Population}명 / {evt.Population}명");
            _previousPopulation = evt.Population;
        }
    }
}
