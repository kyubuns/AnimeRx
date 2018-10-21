using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InQuart(float duration)
        {
            return new EasingDurationAnimator(duration, new InQuartEasing());
        }

        public static IAnimator OutQuart(float duration)
        {
            return new EasingDurationAnimator(duration, new OutQuartEasing());
        }

        public static IAnimator InOutQuart(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutQuartEasing());
        }

        public static IAnimator InQuart(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InQuartEasing());
        }

        public static IAnimator OutQuart(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutQuartEasing());
        }

        public static IAnimator InOutQuart(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutQuartEasing());
        }

        private class InQuartEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v * v * v;
            }
        }

        private class OutQuartEasing : IEasing
        {
            public float Function(float v)
            {
                var f = (v - 1f);
                return f * f * f * (1f - v) + 1f;
            }
        }

        private class InOutQuartEasing : IEasing
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
