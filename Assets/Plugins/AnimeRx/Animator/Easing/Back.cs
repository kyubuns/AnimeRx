using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InBack(float duration)
        {
            return new EasingDurationAnimator(duration, new InBackEasing());
        }

        public static IAnimator OutBack(float duration)
        {
            return new EasingDurationAnimator(duration, new OutBackEasing());
        }

        public static IAnimator InOutBack(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutBackEasing());
        }

        public static IAnimator InBack(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InBackEasing());
        }

        public static IAnimator OutBack(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutBackEasing());
        }

        public static IAnimator InOutBack(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutBackEasing());
        }

        private class InBackEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v * v - v * Mathf.Sin(v * Pi);
            }
        }

        private class OutBackEasing : IEasing
        {
            public float Function(float v)
            {
                var f = (1f - v);
                return 1f - (f * f * f - f * Mathf.Sin(f * Pi));
            }
        }

        private class InOutBackEasing : IEasing
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
