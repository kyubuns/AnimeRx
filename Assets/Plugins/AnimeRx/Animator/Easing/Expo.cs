using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InExpo(float duration)
        {
            return new EasingDurationAnimator(duration, new InExpoEasing());
        }

        public static IAnimator OutExpo(float duration)
        {
            return new EasingDurationAnimator(duration, new OutExpoEasing());
        }

        public static IAnimator InOutExpo(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutExpoEasing());
        }

        public static IAnimator InExpo(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InExpoEasing());
        }

        public static IAnimator OutExpo(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutExpoEasing());
        }

        public static IAnimator InOutExpo(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutExpoEasing());
        }

        private class InExpoEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Approximately(0.0f, v) ? v : Mathf.Pow(2f, 10f * (v - 1f));
            }
        }

        private class OutExpoEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Approximately(1.0f, v) ? v : 1f - Mathf.Pow(2f, -10f * v);
            }
        }

        private class InOutExpoEasing : IEasing
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
