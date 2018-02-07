using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInQuint(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInQuintEasing());
        }

        public static IAnimator EaseOutQuint(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutQuintEasing());
        }

        public static IAnimator EaseInOutQuint(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutQuintEasing());
        }

        public static IAnimator EaseInQuint(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInQuintEasing());
        }

        public static IAnimator EaseOutQuint(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutQuintEasing());
        }

        public static IAnimator EaseInOutQuint(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutQuintEasing());
        }

        private class EaseInQuintEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v * v * v * v;
            }
        }

        private class EaseOutQuintEasing : IEasing
        {
            public float Function(float v)
            {
                var f = (v - 1f);
                return f * f * f * f * f + 1f;
            }
        }

        private class EaseInOutQuintEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    return 16f * v * v * v * v * v;
                }
                else
                {
                    var f = ((2f * v) - 2f);
                    return 0.5f * f * f * f * f * f + 1f;
                }
            }
        }
    }
}
