using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInBack(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInBackEasing());
        }

        public static IAnimator EaseOutBack(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutBackEasing());
        }

        public static IAnimator EaseInOutBack(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutBackEasing());
        }

        public static IAnimator EaseInBack(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInBackEasing());
        }

        public static IAnimator EaseOutBack(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutBackEasing());
        }

        public static IAnimator EaseInOutBack(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutBackEasing());
        }

        private class EaseInBackEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v * v - v * Mathf.Sin(v * Pi);
            }
        }

        private class EaseOutBackEasing : IEasing
        {
            public float Function(float v)
            {
                var f = (1f - v);
                return 1f - (f * f * f - f * Mathf.Sin(f * Pi));
            }
        }

        private class EaseInOutBackEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    var f = 2f * v;
                    return 0.5f * (f * f * f - f * Mathf.Sin(f * Pi));
                }
                else
                {
                    var f = (1 - (2 * v - 1));
                    return 0.5f * (1f - (f * f * f - f * Mathf.Sin(f * Pi))) + 0.5f;
                }
            }
        }
    }
}
