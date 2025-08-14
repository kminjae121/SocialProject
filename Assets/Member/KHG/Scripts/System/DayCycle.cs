using UnityEngine;
using Utility.Unity.Common;

public class DayCycle : MonoBehaviour //시간체크할떄 시작시간 뺴야한다
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

        _timeSet = new DelayInvoker<int>(RaiseTime, 2, 60); //시간 단위 바꾸고싶으면 중간에있는거 수정 ㄱ
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
