using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator EaseInBounce(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInBounceEasing());
        }

        public static IAnimator EaseOutBounce(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseOutBounceEasing());
        }

        public static IAnimator EaseInOutBounce(TimeSpan duration)
        {
            return new EasingDurationAnimator(duration, new EaseInOutBounceEasing());
        }

        public static IAnimator EaseInBounce(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInBounceEasing());
        }

        public static IAnimator EaseOutBounce(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseOutBounceEasing());
        }

        public static IAnimator EaseInOutBounce(float velocity)
        {
            return new EasingVelocityAnimator(velocity, new EaseInOutBounceEasing());
        }

        private class EaseInBounceEasing : IEasing
        {
            public float Function(float v)
            {
                return Bounce(v);
            }

            public static float Bounce(float v)
            {
                return 1 - EaseOutBounceEasing.Bounce(1 - v);
            }
        }

        private class EaseOutBounceEasing : IEasing
        {
            public float Function(float v)
            {
                return Bounce(v);
            }

            public static float Bounce(float v)
            {
                if (v < 4f / 11.0f)
                {
                    return (121f * v * v) / 16.0f;
                }
                else if (v < 8f / 11.0f)
                {
                    return (363f / 40.0f * v * v) - (99f / 10.0f * v) + 17f / 5.0f;
                }
                else if (v < 9f / 10.0f)
                {
                    return (4356f / 361.0f * v * v) - (35442f / 1805.0f * v) + 16061f / 1805.0f;
                }
                else
                {
                    return (54f / 5.0f * v * v) - (513f / 25.0f * v) + 268f / 25.0f;
                }
            }
        }

        private class EaseInOutBounceEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    return 0.5f * EaseInBounceEasing.Bounce(v * 2f);
                }
                else
                {
                    return 0.5f * EaseOutBounceEasing.Bounce(v * 2f - 1f) + 0.5f;
                }
            }
        }
    }
}
