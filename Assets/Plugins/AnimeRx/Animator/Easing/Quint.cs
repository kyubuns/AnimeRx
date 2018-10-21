using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InQuint(float duration)
        {
            return new EasingDurationAnimator(duration, new InQuintEasing());
        }

        public static IAnimator OutQuint(float duration)
        {
            return new EasingDurationAnimator(duration, new OutQuintEasing());
        }

        public static IAnimator InOutQuint(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutQuintEasing());
        }

        public static IAnimator InQuint(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InQuintEasing());
        }

        public static IAnimator OutQuint(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutQuintEasing());
        }

        public static IAnimator InOutQuint(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutQuintEasing());
        }

        private class InQuintEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v * v * v * v;
            }
        }

        private class OutQuintEasing : IEasing
        {
            public float Function(float v)
            {
                var f = (v - 1f);
                return f * f * f * f * f + 1f;
            }
        }

        private class InOutQuintEasing : IEasing
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
