using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator Linear(TimeSpan duration)
        {
            return new EasingAnimator(duration, new LinearEasing());
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
