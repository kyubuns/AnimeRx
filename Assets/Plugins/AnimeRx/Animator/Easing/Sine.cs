using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InSine(float duration)
        {
            return new EasingDurationAnimator(duration, new InSineEasing());
        }

        public static IAnimator OutSine(float duration)
        {
            return new EasingDurationAnimator(duration, new OutSineEasing());
        }

        public static IAnimator InOutSine(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutSineEasing());
        }

        public static IAnimator InSine(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InSineEasing());
        }

        public static IAnimator OutSine(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutSineEasing());
        }

        public static IAnimator InOutSine(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutSineEasing());
        }

        private class InSineEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sin((v - 1f) * HalfPi) + 1f;
            }
        }

        private class OutSineEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sin(v * HalfPi);
            }
        }

        private class InOutSineEasing : IEasing
        {
            public float Function(float v)
            {
                return 0.5f * (1f - Mathf.Cos(v * Pi));
            }
        }
    }
}
