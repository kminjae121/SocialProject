using UnityEngine;
using Utility.Unity.Common;

public class DayCycle : MonoBehaviour //�ð�üũ�ҋ� ���۽ð� �����Ѵ�
{
    [SerializeField] private Transform sun;
    public int _day { get; private set; }
    public int _hour { get; private set; }

    private DelayInvoker<int> _timeSet;
    private int _totalTime;

    private void OnEnable()
    {
        Initialize(0,13);
    }

    public void Initialize(int d,int h)
    {
        _day = d;
        _hour = h;
        _totalTime = h + d * 24;

        _timeSet = new DelayInvoker<int>(RaiseTime, 2, 60); //�ð� ���� �ٲٰ������ �߰����ִ°� ���� ��
    }
    private void RaiseTime(int value) => _totalTime += value;

    private void Update()
    {
        _timeSet.Tick();
        SetSky();
    }

    private void SetSky()
    {
        //sun.rotation
    }
}
