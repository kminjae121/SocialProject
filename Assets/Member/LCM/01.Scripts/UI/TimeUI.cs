using TMPro;
using UnityEngine;

namespace Member.LCM._01.Scripts.UI
{
    public class TimeUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DayCycle dayCycle;

        [Header("Time UI")]
        [SerializeField] private TextMeshProUGUI timeText;
        private int _lastDay = -1;
        private int _lastHour = -1;

        private void Awake()
        {
            if (dayCycle == null)
            {
                Debug.LogError("DayCycle이 할당되지 않았습니다!", this);
            }
        }

        private void Update()
        {
            if (dayCycle.Day != _lastDay || dayCycle.Hour != _lastHour)
            {
                _lastDay = dayCycle.Day;
                _lastHour = dayCycle.Hour;
                timeText.SetText($"{_lastDay} day\n{_lastHour} hour");
            }
        }
    }
}
