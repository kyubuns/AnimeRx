using System;
using UniRx;

namespace AnimeRx
{
    public class CompositeAnimator : IAnimator
    {
        private readonly Tuple<IAnimator, float>[] animators;
        private readonly float[] times;
        private readonly float totalDistance;

        public CompositeAnimator(Tuple<IAnimator, float>[] animators)
        {
            this.animators = animators;

            times = new float[animators.Length];
            for (var i = 0; i < animators.Length; ++i)
            {
                totalDistance += animators[i].Item2;
                times[i] = animators[i].Item1.CalcFinishTime(animators[i].Item2);
            }
        }

        public float CalcFinishTime(float distance)
        {
            var total = 0.0f;
            for (var i = 0; i < animators.Length; ++i)
            {
                total += times[i];
            }
            return total;
        }

        public float CalcPosition(float time, float distance)
        {
            var i = 0;
            var calcedDistance = 0.0f;
            for (; i < animators.Length - 1; ++i)
            {
                if (times[i] < time)
                {
                    time -= times[i];
                    calcedDistance += animators[i].Item2;
                    continue;
                }

                break;
            }

            return (animators[i].Item1.CalcPosition(time, animators[i].Item2) * animators[i].Item2 + calcedDistance) / totalDistance;
        }
    }
}
