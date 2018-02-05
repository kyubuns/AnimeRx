using UnityEngine;

namespace AnimeRx
{
    public static partial class Motion
    {
        public static IAnimator Uniform(float velocity)
        {
            return new UniformAnimator(velocity);
        }

        private class UniformAnimator : IAnimator
        {
            private readonly float velocity;

            public UniformAnimator(float velocity)
            {
                this.velocity = velocity;
            }

            public float CalcFinishTime(float distance)
            {
                return distance / velocity;
            }

            public float CalcPosition(float time, float distance)
            {
                return Mathf.Clamp01(time / CalcFinishTime(distance));
            }
        }
    }
}
