using System;

namespace AnimeRx
{
    public static partial class Easing
    {
        public static IAnimator InBounce(float duration)
        {
            return new EasingDurationAnimator(duration, new InBounceEasing());
        }

        public static IAnimator OutBounce(float duration)
        {
            return new EasingDurationAnimator(duration, new OutBounceEasing());
        }

        public static IAnimator InOutBounce(float duration)
        {
            return new EasingDurationAnimator(duration, new InOutBounceEasing());
        }

        public static IAnimator InBounce(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InBounceEasing());
        }

        public static IAnimator OutBounce(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new OutBounceEasing());
        }

        public static IAnimator InOutBounce(Velocity velocity)
        {
            return new EasingVelocityAnimator(velocity, new InOutBounceEasing());
        }

        private class InBounceEasing : IEasing
        {
            public float Function(float v)
            {
                return Bounce(v);
            }

            public static float Bounce(float v)
            {
                return 1 - OutBounceEasing.Bounce(1 - v);
            }
        }

        private class OutBounceEasing : IEasing
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

        private class InOutBounceEasing : IEasing
        {
            public float Function(float v)
            {
                if (v < 0.5f)
                {
                    return 0.5f * InBounceEasing.Bounce(v * 2f);
                }
                else
                {
                    return 0.5f * OutBounceEasing.Bounce(v * 2f - 1f) + 0.5f;
                }
            }
        }
    }
}
