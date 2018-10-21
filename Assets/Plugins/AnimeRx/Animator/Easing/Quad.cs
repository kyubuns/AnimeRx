using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InQuad(float duration)
        {
            return new EasingDurationAnimator(duration, new InQuadEasing());
        }

        public static IAnimator OutQuad(float duration)
        {
            return new EasingDurationAnimator(duration, new OutQuadEasing());
        }

        public static IAnimator InOutQuad(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutQuadEasing());
        }

        public static IAnimator InQuad(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InQuadEasing());
        }

        public static IAnimator OutQuad(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutQuadEasing());
        }

        public static IAnimator InOutQuad(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutQuadEasing());
        }

        private class InQuadEasing : IEasing
        {
            public float Function(float v)
            {
                return v * v;
            }
        }

        private class OutQuadEasing : IEasing
        {
            public float Function(float v)
            {
                return -(v * (v - 2f));
            }
        }

        private class InOutQuadEasing : IEasing
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
