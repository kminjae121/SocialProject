using UnityEngine;
using Utility.Unity.Common;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private float hoursPerSecond = 30;
    [SerializeField] private Transform sun;
    public int _day { get; private set; }
    public int _hour { get; private set; }

    private DelayInvoker<int> _timeSet;
    private int _totalTime;

    private void OnEnable()
    {
        Initialize(0, 13);
    }

    public void Initialize(int d, int h)
    {
        _day = d;
        _hour = h;
        _totalTime = h + d * 24;
        _timeSet = new DelayInvoker<int>(RaiseTime, 1, hoursPerSecond);
    }

    private void RaiseTime(int value)
    {
        _totalTime += value;
        _day = _totalTime / 24;
        _hour = _totalTime % 24;
    }

    private void Update()
    {
        _timeSet.Tick();
        SetSky(hoursPerSecond);
    }

    private void SetSky(float duration)
    {
        float currentAngle = sun.rotation.x;
        float targetAngle = (_hour * 15f) - 90;

        //sun.rotation = Quaternion.Euler(targetAngle, sun.rotation.y, sun.rotation.z);
        sun.rotation = Quaternion.Euler(Interpolater.Lerp(sun.rotation.x, targetAngle,1), sun.rotation.y, sun.rotation.z);
    }
}
