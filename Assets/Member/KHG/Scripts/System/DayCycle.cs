using Core.Events;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private float hoursPerSecond = 0.1f;
    [SerializeField] private Transform sun;
    [SerializeField] private ResourceManager resourceMananger;

    [SerializeField] private float claimIntervalHours = 12f; // 직렬화: 기본 12시간

    public int Day { get; private set; }
    public int Hour { get; private set; }

    private Light _sunLight;
    private float _totalTime;
    private float _nextClaimTime; // 다음 MoneyClaim 실행 시각(시간 단위)

    private void OnEnable()
    {
        _sunLight = sun.GetComponent<Light>();
        Initialize(0, 9);
    }

    public void Initialize(int d, int h)
    {
        Day = d;
        Hour = h;
        _totalTime = h + d * 24f;
        _nextClaimTime = _totalTime + claimIntervalHours;
    }

    private void MoneyClaim()
    {
        resourceMananger.ClaimMoney();
    }

    private void Update()
    {
        _totalTime += hoursPerSecond * Time.deltaTime;
        Day = Mathf.FloorToInt(_totalTime / 24f);
        Hour = Mathf.FloorToInt(_totalTime % 24f);
        SetSky();

        if (_totalTime >= _nextClaimTime)
        {
            MoneyClaim();
            _nextClaimTime += claimIntervalHours;
        }
    }

    private void SetSky()
    {
        float hourInDay = _totalTime % 24f;
        float angle = (hourInDay / 24f) * 360f - 90f;
        Quaternion targetRot = Quaternion.Euler(angle, 0f, 0f);
        sun.rotation = targetRot;
    }
}
