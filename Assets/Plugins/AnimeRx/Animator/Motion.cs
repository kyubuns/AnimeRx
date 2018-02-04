using UnityEngine;

namespace AnimeRx
{
    public class Motion
    {
        public static IAnimator Uniform(float speed)
        {
            return new UniformAnimator(speed);
        }

        private class UniformAnimator : IAnimator
        {
            private readonly float speed;

            public UniformAnimator(float speed)
            {
                this.speed = speed;
            }

            public float CalcFinishTime(float distance)
            {
                return distance / speed;
            }

            public float CalcPosition(float time, float distance)
            {
                return Mathf.Clamp01(time * speed / distance);
            }
        }
    }
}