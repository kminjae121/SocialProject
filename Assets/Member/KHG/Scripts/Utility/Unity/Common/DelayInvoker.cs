using System;
using UnityEngine;

namespace Utility.Unity.Common
{
    public class DelayInvoker<T>
    {
        public bool IsCompleted => _targetCount > 0 && _currentCount >= _targetCount;

        private readonly Action<T> _callbackAction;
        private readonly float _delayTime;
        private readonly int _targetCount;
        private readonly T _genericValue;

        private float _lastInvokeTime;
        private int _currentCount;
        private bool _paused;


        /// <summary>
        /// DelayInvoker ������
        /// </summary>
        /// <param name="action">����� action</param>
        /// <param name="value">action�� �� Type</param>
        /// <param name="delayTime">���� �ð�</param>
        /// <param name="repeatCount">�ݺ� Ƚ�� (�⺻��:���ѹݺ�)</param>
        /// <exception cref="ArgumentNullException">action�� null�� ��� �߻�</exception>
        public DelayInvoker(Action<T> action, T value, float delayTime, int repeatCount = -1)
        {
            _callbackAction = action ?? throw new ArgumentNullException(nameof(action));
            _delayTime = delayTime;
            _targetCount = repeatCount;
            _genericValue = value;

            _lastInvokeTime = Time.time;
            _currentCount = 0;
            _paused = false;
        }

        /// <summary>
        /// DelayInvoker�� ƽ. Update���� ȣ��
        /// </summary>
        public void Tick()
        {
            if (_paused || IsCompleted)
                return;

            if (Time.time - _lastInvokeTime >= _delayTime)
            {
                _lastInvokeTime = Time.time;
                _callbackAction?.Invoke(_genericValue);
                _currentCount++;
            }
        }

        public void Pause() => _paused = true;
        public void Resume() => _paused = false;
    }
}
