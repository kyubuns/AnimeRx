using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInQuad(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInQuadEasing());
        }

        public static IAnimator EaseOutQuad(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutQuadEasing());
        }

        public static IAnimator EaseInOutQuad(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutQuadEasing());
        }

        public static IAnimator EaseInQuad(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInQuadEasing());
        }

        public static IAnimator EaseOutQuad(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutQuadEasing());
        }

        public static IAnimator EaseInOutQuad(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutQuadEasing());
        }

        private class EaseInQuadEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v;
            }
        }

        private class EaseOutQuadEasing : IEasing
        {
            public float Function(float v)
            {
                return -(v * (v - 2f));
            }
        }

        private class EaseInOutQuadEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    return 2f * v * v;
                }
                else
                {
                    return -2f * v * v + 4f * v - 1f;
                }
            }
        }
    }
}
