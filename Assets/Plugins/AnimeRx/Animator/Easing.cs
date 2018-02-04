using System;
using UnityEngine;

namespace AnimeRx
{
    public static class Easing
    {
        private abstract class EasingAnimator : IAnimator
        {
            private readonly float duration;

            protected EasingAnimator(TimeSpan duration)
            {
                this.duration = (float) duration.TotalSeconds;
            }

            public float CalcFinishTime(float distance)
            {
                return duration;
            }

            public float CalcPosition(float time, float distance)
            {
                return Function(Mathf.Clamp01(time / duration));
            }

            protected abstract float Function(float v);
        }

        public static IAnimator Linear(TimeSpan duration)
        {
            return new LinearAnimator(duration);
        }

        private class LinearAnimator : EasingAnimator
        {
            public LinearAnimator(TimeSpan duration) : base(duration)
            {
            }

            protected override float Function(float v)
            {
                return v;
            }
        }
    }
}
