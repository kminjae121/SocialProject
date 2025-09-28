using Core.Events;
using TMPro;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO resourceChannel;
        [SerializeField] private TextMeshProUGUI moneyText;

        private void Awake()
        {
            resourceChannel.AddListener<MoneyEvent>(HandleMoneyChange);
        }

        private void OnDestroy()
        {
            resourceChannel.RemoveListener<MoneyEvent>(HandleMoneyChange);
        }

        private void HandleMoneyChange(MoneyEvent evt)
        {
            moneyText.SetText($"{evt.Money.ToString()}원");
        }
    }
}