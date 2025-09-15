using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private float hoursPerSecond = 60f;
    [SerializeField] private Transform sun;

    public int Day { get; private set; }
    public int Hour { get; private set; }

    private Light _sunLight;
    private float _totalTime;

    private void OnEnable()
    {
        _sunLight = sun.GetComponent<Light>();
        Initialize(0, 6);
    }

    public void Initialize(int d, int h)
    {
        Day = d;
        Hour = h;
        _totalTime = h + d * 24f;
    }

    private void Update()
    {
        _totalTime += hoursPerSecond * Time.deltaTime;
        Day = Mathf.FloorToInt(_totalTime / 24f);
        Hour = Mathf.FloorToInt(_totalTime % 24f);
        SetSky();
        SetLight();
    }

    private void SetSky()
    {
        float hourInDay = _totalTime % 24f;
        float angle = (hourInDay / 24f) * 360f - 90f;
        Quaternion targetRot = Quaternion.Euler(angle, 0f, 0f);
        sun.rotation = targetRot;

        //print($"{Day}:{Hour}");
    }

    private void SetLight()
    {
        float dot = Vector3.Dot(sun.forward, Vector3.down);
        float intensity = Mathf.Clamp01(dot);
        _sunLight.intensity = intensity;
    }
}
