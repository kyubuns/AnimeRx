using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InElastic(float duration)
        {
            return new EasingDurationAnimator(duration, new InElasticEasing());
        }

        public static IAnimator OutElastic(float duration)
        {
            return new EasingDurationAnimator(duration, new OutElasticEasing());
        }

        public static IAnimator InOutElastic(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutElasticEasing());
        }

        public static IAnimator InElastic(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InElasticEasing());
        }

        public static IAnimator OutElastic(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutElasticEasing());
        }

        public static IAnimator InOutElastic(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutElasticEasing());
        }

        private class InElasticEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sin(13 * HalfPi * v) * Mathf.Pow(2f, 10f * (v - 1f));
            }
        }

        private class OutElasticEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sin(-13 * HalfPi * (v + 1)) * Mathf.Pow(2f, -10f * v) + 1f;
            }
        }

        private class InOutElasticEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    return 0.5f * Mathf.Sin(13f * HalfPi * (2f * v)) * Mathf.Pow(2f, 10f * ((2f * v) - 1f));
                }
                else
                {
                    return 0.5f * (Mathf.Sin(-13f * HalfPi * ((2f * v - 1f) + 1f)) * Mathf.Pow(2f, -10f * (2f * v - 1f)) + 2f);
                }
            }
        }
    }
}
