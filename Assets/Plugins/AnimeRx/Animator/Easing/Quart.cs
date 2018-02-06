using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInQuart(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInQuartEasing());
        }

        public static IAnimator EaseOutQuart(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutQuartEasing());
        }

        public static IAnimator EaseInOutQuart(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutQuartEasing());
        }

        public static IAnimator EaseInQuart(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInQuartEasing());
        }

        public static IAnimator EaseOutQuart(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutQuartEasing());
        }

        public static IAnimator EaseInOutQuart(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutQuartEasing());
        }

        private class EaseInQuartEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v * v * v;
            }
        }

        private class EaseOutQuartEasing : IEasing
        {
            public float Function(float v)
            {
                var f = (v - 1f);
                return f * f * f * f + 1f;
            }
        }

        private class EaseInOutQuartEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    return 8f * v * v * v * v;
                }
                else
                {
                    var f = ((2f * v) - 2f);
                    return 0.5f * f * f * f * f + 1f;
                }
            }
        }
    }
}
