using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InCirc(float duration)
        {
            return new EasingDurationAnimator(duration, new InCircEasing());
        }

        public static IAnimator OutCirc(float duration)
        {
            return new EasingDurationAnimator(duration, new OutCircEasing());
        }

        public static IAnimator InOutCirc(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutCircEasing());
        }

        public static IAnimator InCirc(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InCircEasing());
        }

        public static IAnimator OutCirc(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutCircEasing());
        }

        public static IAnimator InOutCirc(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutCircEasing());
        }

        private class InCircEasing : IEasing
        {
            public float Function(float v)
            {
                return 1f - Mathf.Sqrt(1f - (v * v));
            }
        }

        private class OutCircEasing : IEasing
        {
            public float Function(float v)
            {
                return Mathf.Sqrt((2f - v) * v);
            }
        }

        private class InOutCircEasing : IEasing
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
