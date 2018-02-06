using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInCubic(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInCubicEasing());
        }

        public static IAnimator EaseOutCubic(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutCubicEasing());
        }

        public static IAnimator EaseInOutCubic(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutCubicEasing());
        }

        public static IAnimator EaseInCubic(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInCubicEasing());
        }

        public static IAnimator EaseOutCubic(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutCubicEasing());
        }

        public static IAnimator EaseInOutCubic(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutCubicEasing());
        }

        private class EaseInCubicEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v * v;
            }
        }

        private class EaseOutCubicEasing : IEasing
        {
            public float Function(float v)
            {
                var f = (v - 1f);
                return f * f * f + 1f;
            }
        }

        private class EaseInOutCubicEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    return 4f * v * v * v;
                }
                else
                {
                    var f = ((2f * v) - 2f);
                    return 0.5f * f * f * f + 1f;
                }
            }
        }
    }
}
