using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Motion
    {
        public static IAnimator Random(TimeSpan timeSpan, float min, float max)
        {
            return new RandomDurationAnimator(timeSpan, min, max);
        }

        private class RandomDurationAnimator : IAnimator
        {
            private readonly float duration;
            private readonly float min;
            private readonly float max;

            public RandomDurationAnimator(TimeSpan duration, float min, float max)
            {
                this.duration = (float) duration.TotalSeconds;
                this.min = min;
                this.max = max;
            }

            public float CalcFinishTime(float distance)
            {
                return duration;
            }

            public float CalcPosition(float time, float distance)
            {
                if (Mathf.Approximately(time, duration))
                {
                    return 0.0f;
                }

                return UnityEngine.Random.Range(min, max);
            }
        }
    }
}
