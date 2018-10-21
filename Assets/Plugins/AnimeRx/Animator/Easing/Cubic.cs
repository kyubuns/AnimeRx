using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InCubic(float duration)
        {
            return new EasingDurationAnimator(duration, new InCubicEasing());
        }

        public static IAnimator OutCubic(float duration)
        {
            return new EasingDurationAnimator(duration, new OutCubicEasing());
        }

        public static IAnimator InOutCubic(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutCubicEasing());
        }

        public static IAnimator InCubic(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InCubicEasing());
        }

        public static IAnimator OutCubic(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutCubicEasing());
        }

        public static IAnimator InOutCubic(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutCubicEasing());
        }

        private class InCubicEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v * v;
            }
        }

        private class OutCubicEasing : IEasing
        {
            public float Function(float v)
            {
                var f = (v - 1f);
                return f * f * f + 1f;
            }
        }

        private class InOutCubicEasing : IEasing
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
