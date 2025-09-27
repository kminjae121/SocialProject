using System.Collections.Generic;
using Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Member.LCM._01.Scripts.UI
{
    public class WeatherUI : MonoBehaviour
    {
        [Header("Event")] [SerializeField] private GameEventChannelSO mapChannel;

        [Header("Weather UI")] 
        [SerializeField] private Image weatherIcon;
        [SerializeField] private TextMeshProUGUI weatherText;
        
        [Header("Weather Values")]
        [Tooltip("날씨별 아이콘 스프라이트와 텍스트를 순서대로 할당\n0: 맑음, 1: 흐림, 2: 비")]
        [SerializeField] private List<Sprite> weatherIcons;
        [SerializeField] private List<string> weatherNames;

        private void Awake()
        {
            mapChannel.AddListener<WeatherChangeEvent>(HandleWeatherChange);
        }

        private void OnDestroy()
        {
            mapChannel.RemoveListener<WeatherChangeEvent>(HandleWeatherChange);
        }

        private void HandleWeatherChange(WeatherChangeEvent evt)
        {
            switch (evt.Weather)
            {
                case Weather.Clean:
                    UpdateWeatherDisplay(0);
                    break;
                case Weather.Cloudy:
                    UpdateWeatherDisplay(1);
                    break;
                case Weather.Rain:
                    UpdateWeatherDisplay(2);
                    break;
                default:
                    Debug.LogWarning($"날씨값에 NULL값이 들어옴: {evt.Weather}");
                    break;
            }
        }

        private void UpdateWeatherDisplay(int iconIndex)
        {
            if (iconIndex < 0 || iconIndex >= weatherIcons.Count)
            {
                Debug.LogError($"유효하지 않은 아이콘 인덱스: {iconIndex}");
                return;
            }
    
            if (iconIndex >= weatherNames.Count)
            {
                Debug.LogError($"weatherNames 인덱스 범위 초과: {iconIndex}");
                return;
            }
    
            if (weatherIcons[iconIndex] == null)
            {
                Debug.LogError($"인덱스 {iconIndex}의 아이콘이 null");
                return;
            }
    
            if (weatherIcon == null || weatherText == null)
            {
                Debug.LogError("weatherIcon 또는 weatherText 컴포넌트가 null입니다");
                return;
            }

            weatherIcon.sprite = weatherIcons[iconIndex];
            weatherText.text = weatherNames[iconIndex];
        }
    }
}