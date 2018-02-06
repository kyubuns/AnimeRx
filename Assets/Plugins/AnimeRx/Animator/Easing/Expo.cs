using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInExpo(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInExpoEasing());
        }

        public static IAnimator EaseOutExpo(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutExpoEasing());
        }

        public static IAnimator EaseInOutExpo(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutExpoEasing());
        }

        public static IAnimator EaseInExpo(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInExpoEasing());
        }

        public static IAnimator EaseOutExpo(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutExpoEasing());
        }

        public static IAnimator EaseInOutExpo(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutExpoEasing());
        }

        private class EaseInExpoEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Approximately(0.0f, v) ? v : Mathf.Pow(2f, 10f * (v - 1f));
            }
        }

        private class EaseOutExpoEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Approximately(1.0f, v) ? v : 1f - Mathf.Pow(2f, -10f * v);
            }
        }

        private class EaseInOutExpoEasing : IEasing
        {
            public float Function(float v)
            {
                if (Mathf.Approximately(0.0f, v) || Mathf.Approximately(1.0f, v)) return v;

                if (v < 0.5f)
                {
                    return 0.5f * Mathf.Pow(2f, (20f * v) - 10f);
                }
                else
                {
                    return -0.5f * Mathf.Pow(2f, (-20f * v) + 10f) + 1f;
                }
            }
        }
    }
}
