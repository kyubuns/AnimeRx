using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInCirc(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInCircEasing());
        }

        public static IAnimator EaseOutCirc(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutCircEasing());
        }

        public static IAnimator EaseInOutCirc(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutCircEasing());
        }

        public static IAnimator EaseInCirc(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInCircEasing());
        }

        public static IAnimator EaseOutCirc(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutCircEasing());
        }

        public static IAnimator EaseInOutCirc(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutCircEasing());
        }

        private class EaseInCircEasing : IEasing
        {
            public float Function(float v)
            {
                return 1f - Mathf.Sqrt(1f - (v * v));
            }
        }

        private class EaseOutCircEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sqrt((2f - v) * v);
            }
        }

        private class EaseInOutCircEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    return 0.5f * (1 - Mathf.Sqrt(1f - 4f * (v * v)));
                }
                else
                {
                    return 0.5f * (Mathf.Sqrt(-((2f * v) - 3f) * ((2f * v) - 1f)) + 1f);
                }
            }
        }
    }
}
