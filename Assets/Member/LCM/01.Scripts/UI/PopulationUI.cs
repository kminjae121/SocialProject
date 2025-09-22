using Core.Events;
using TMPro;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI
{
    public class PopulationUI : MonoBehaviour
    {
        [Header("Event")]
        [SerializeField] private GameEventChannelSO resourceChannel;

        [Header("Population UI")]
        [SerializeField] private TextMeshProUGUI populationText;

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
            populationText.SetText($"인구수 : {evt.Population}명 / {evt.Population}명");
        }
    }
}
