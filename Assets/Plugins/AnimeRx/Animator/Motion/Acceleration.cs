using UnityEngine;

namespace AnimeRx
{
    public static partial class Motion
    {
        public static IAnimator Acceleration(float acceleration)
        {
            return Acceleration(acceleration, 0.0f);
        }

        public static IAnimator Acceleration(float acceleration, float velocityStart)
        {
            return new AccelerationAnimator(acceleration, velocityStart);
        }

        private class AccelerationAnimator : IAnimator
        {
            private readonly float acceleration;
            private readonly float velocityStart;

            public AccelerationAnimator(float acceleration, float velocityStart)
            {
                this.acceleration = acceleration;
                this.velocityStart = velocityStart;
            }

            public float CalcFinishTime(float distance)
            {
                var d = Mathf.Sqrt(velocityStart * velocityStart + 2f * acceleration * distance);
                return (-velocityStart + d) / acceleration;
            }

            public float CalcPosition(float time, float distance)
            {
                return Mathf.Clamp01((velocityStart * time + 0.5f * acceleration * time * time) / distance);
            }
        }
    }
}
