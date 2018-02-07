using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInSine(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInSineEasing());
        }

        public static IAnimator EaseOutSine(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutSineEasing());
        }

        public static IAnimator EaseInOutSine(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutSineEasing());
        }

        public static IAnimator EaseInSine(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInSineEasing());
        }

        public static IAnimator EaseOutSine(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutSineEasing());
        }

        public static IAnimator EaseInOutSine(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutSineEasing());
        }

        private class EaseInSineEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sin((v - 1f) * HalfPi) + 1f;
            }
        }

        private class EaseOutSineEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sin(v * HalfPi);
            }
        }

        private class EaseInOutSineEasing : IEasing
        {
            public float Function(float v)
            {
                return 0.5f * (1f - Mathf.Cos(v * Pi));
            }
        }
    }
}
