using System;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Motion
    {
        public static IAnimator From(AnimationCurve curve, TimeSpan duration)
        {
            return new AnimationCurveAnimator(curve, duration);
        }

        private class AnimationCurveAnimator : IAnimator
        {
            private readonly AnimationCurve curve;
            private readonly float duration;

            public AnimationCurveAnimator(AnimationCurve curve, TimeSpan duration)
            {
                this.curve = curve;
                this.duration = (float) duration.TotalSeconds;
            }

            public float CalcFinishTime(float distance)
            {
                return duration;
            }

            public float CalcPosition(float time, float distance)
            {
                return curve.Evaluate(time / duration);
            }
        }
    }
}
