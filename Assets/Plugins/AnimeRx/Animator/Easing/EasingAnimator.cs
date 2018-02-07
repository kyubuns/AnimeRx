using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Easing
    {
        private class EasingDurationAnimator : IAnimator
        {
            private readonly float duration;
            private readonly IEasing easing;

            public EasingDurationAnimator(TimeSpan duration, IEasing easing)
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

        private class EasingVelocityAnimator : IAnimator
        {
            private readonly float velocity;
            private readonly IEasing easing;

            public EasingVelocityAnimator(Velocity velocity, IEasing easing)
            {
                this.velocity = (float) velocity.PerSecond;
                this.easing = easing;
            }

            public float CalcFinishTime(float distance)
            {
                return distance / velocity;
            }

            public float CalcPosition(float time, float distance)
            {
                return easing.Function(Mathf.Clamp01(time / CalcFinishTime(distance)));
            }
        }
    }
}
