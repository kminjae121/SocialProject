using UnityEngine;

namespace Utility.Unity.Common
{
    public static class Interpolater
    {
        private const float _epsilon = 0.001f;

        /// <summary>
        /// float 을 Lerp
        /// </summary>
        /// <param name="a">현재값</param>
        /// <param name="b">목표값</param>
        /// <param name="duration">목표 시간(초)</param>
        /// <returns></returns>
        public static float Lerp(float a, float b, float duration)
        {
            if (Mathf.Abs(b - a) < _epsilon) return b;
            float rate = Time.deltaTime / duration;
            return Mathf.Lerp(a, b, rate);
        }

        /// <summary>
        /// float 을 Lerp (초단위)
        /// </summary>
        /// <param name="a">현재값</param>
        /// <param name="b">목표값</param>
        /// <param name="duration">목표 시간(초)</param>
        /// <returns></returns>
        public static float LerpOverTime(float a, float b, float duration)
        {
            if (Mathf.Abs(b - a) < _epsilon) return b;
            float rate = Time.deltaTime / duration;
            return Mathf.Lerp(a, b, rate);
        }

        /// <summary>
        /// Vector3 를 Lerp
        /// </summary>
        /// <param name="v1">현재값</param>
        /// <param name="v2">목표값</param>
        /// <param name="duration">목표 시간(초)</param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 v1, Vector3 v2, float duration)
        {
            if ((v2 - v1).sqrMagnitude < _epsilon * _epsilon) return v2;
            float rate = Time.deltaTime / duration;
            return Vector3.Lerp(v1, v2, rate);
        }

        /// <summary>
        /// Quaternion을 Slerp
        /// </summary>
        /// <param name="q1">현재값</param>
        /// <param name="q2">목표값</param>
        /// <param name="duration">목표 시간(초)</param>
        /// <returns></returns>
        public static Quaternion Lerp(Quaternion q1, Quaternion q2, float duration)
        {
            if (Quaternion.Angle(q1, q2) < _epsilon) return q2;
            float rate = Time.deltaTime / duration;
            return Quaternion.Slerp(q1, q2, rate);
        }

        /// <summary>
        /// Color을 Lerp
        /// </summary>
        /// <param name="c1">현재값</param>
        /// <param name="c2">목표값</param>
        /// <param name="duration">목표 시간(초)</param>
        /// <returns></returns>
        public static Color Lerp(Color c1, Color c2, float duration)
        {
            float r = Lerp(c1.r, c2.r, duration);
            float g = Lerp(c1.g, c2.g, duration);
            float b = Lerp(c1.b, c2.b, duration);
            float a = Lerp(c1.a, c2.a, duration);

            return new Color(r, g, b, a);
        }
    }
}
