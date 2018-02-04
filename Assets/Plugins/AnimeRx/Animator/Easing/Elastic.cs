using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInElastic(TimeSpan duration)
        {
            return new EasingAnimator(duration, new EaseInElasticEasing());
        }

        public static IAnimator EaseOutElastic(TimeSpan duration)
        {
            return new EasingAnimator(duration, new EaseOutElasticEasing());
        }

        public static IAnimator EaseInOutElastic(TimeSpan duration)
        {
            return new EasingAnimator(duration, new EaseInOutElasticEasing());
        }

        private class EaseInElasticEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sin(13 * HalfPi * v) * Mathf.Pow(2f, 10f * (v - 1f));
            }
        }

        private class EaseOutElasticEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sin(-13 * HalfPi * (v + 1)) * Mathf.Pow(2f, -10f * v) + 1f;
            }
        }

        private class EaseInOutElasticEasing : IEasing
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