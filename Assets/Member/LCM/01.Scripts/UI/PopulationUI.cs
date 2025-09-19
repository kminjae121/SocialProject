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
            resourceChannel.AddListener<PopulationEvent>(HandleChangePopulation);
        }

        private void OnDestroy()
        {
            resourceChannel.RemoveListener<PopulationEvent>(HandleChangePopulation);
        }

        private void HandleChangePopulation(PopulationEvent evt)
        {
            populationText.SetText($"인구수 : {evt.CurrentPopulation}명 / {evt.MaxPopulation}명");
        }
    }
}
