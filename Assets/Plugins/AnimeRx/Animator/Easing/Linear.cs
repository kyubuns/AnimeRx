using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator Linear(float duration)
        {
            return new EasingDurationAnimator(duration, new LinearEasing());
        }

        public static IAnimator Linear(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new LinearEasing());
        }

        private class LinearEasing : IEasing
        {
            public float Function(float v)
            {
                return v;
            }
        }
    }
}
