using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        private class EasingAnimator : IAnimator
        {
            private readonly float duration;
            private readonly IEasing easing;

            public EasingAnimator(TimeSpan duration, IEasing easing)
            {
                this.duration = (float) duration.TotalSeconds;
                this.easing = easing;
            }

            public float CalcFinishTime(float distance)
            {
                return duration;
            }

            public float CalcPosition(float time, float distance)
            {
                return easing.Function(Mathf.Clamp01(time / duration));
            }
        }
    }
}