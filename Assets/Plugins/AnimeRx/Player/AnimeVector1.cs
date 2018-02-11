using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<float> Play(float from, float to, IAnimator animator)
        {
            return Play(from, to, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(float from, float to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Mathf.Abs(from - to), scheduler)
                .Select(x => Mathf.LerpUnclamped(from, to, x));
        }

        public static IObservable<float> PlayIn(float from, float inEnd, float to, IAnimator inAnimator)
        {
            return PlayIn(from, inEnd, to, inAnimator, DefaultScheduler);
        }

        public static IObservable<float> PlayIn(float from, float inEnd, float to, IAnimator inAnimator, IScheduler scheduler)
        {
            var velocity = inAnimator.CalcFinishVelocity(Mathf.Abs(inEnd - from));
            var linearAnimator = Easing.Linear(velocity);
            var compositeAnimator = new CompositeAnimator(new[]
            {
                Tuple.Create(inAnimator, Mathf.Abs(inEnd - from)),
                Tuple.Create(linearAnimator, Mathf.Abs(to - inEnd)),
            });
            return Play(new[] {from, inEnd, to}, compositeAnimator, scheduler);
        }

        public static IObservable<float> PlayOut(float from, float outStart, float to, IAnimator outAnimator)
        {
            return PlayOut(from, outStart, to, outAnimator, DefaultScheduler);
        }

        public static IObservable<float> PlayOut(float from, float outStart, float to, IAnimator outAnimator, IScheduler scheduler)
        {
            var velocity = outAnimator.CalcStartVelocity(Mathf.Abs(to - outStart));
            var linearAnimator = Easing.Linear(velocity);
            var compositeAnimator = new CompositeAnimator(new[]
            {
                Tuple.Create(linearAnimator, Mathf.Abs(outStart - from)),
                Tuple.Create(outAnimator, Mathf.Abs(to - outStart)),
            });
            return Play(new[] {from, outStart, to}, compositeAnimator, scheduler);
        }

        public static IObservable<float> PlayInOut(float from, float inEnd, float outStart, float to, IAnimator inAnimator, IAnimator outAnimator)
        {
            return PlayInOut(from, inEnd, outStart, to, inAnimator, outAnimator, DefaultScheduler);
        }

        public static IObservable<float> PlayInOut(float from, float inEnd, float outStart, float to, IAnimator inAnimator, IAnimator outAnimator, IScheduler scheduler)
        {
            var inVelocity = inAnimator.CalcFinishVelocity(Mathf.Abs(inEnd - from)).PerSecond;
            var outVelocity = outAnimator.CalcStartVelocity(Mathf.Abs(to - outStart)).PerSecond;
            IAnimator linearAnimator;

            if (Math.Abs(inVelocity - outVelocity) < EqualDelta)
            {
                linearAnimator = Motion.Uniform((float) ((inVelocity + outVelocity) / 2.0));
            }
            else
            {
                var accel = (outVelocity * outVelocity - inVelocity * inVelocity) / (2.0f * Mathf.Abs(outStart - inEnd));
                linearAnimator = Motion.Acceleration((float) accel, (float) inVelocity);
            }

            var compositeAnimator = new CompositeAnimator(new[]
            {
                Tuple.Create(inAnimator, Mathf.Abs(inEnd - from)),
                Tuple.Create(linearAnimator, Mathf.Abs(outStart - inEnd)),
                Tuple.Create(outAnimator, Mathf.Abs(to - outStart)),
            });
            return Play(new[] {from, inEnd, outStart, to}, compositeAnimator, scheduler);
        }

        public static IObservable<float> Play(float[] path, IAnimator animator)
        {
            return Play(path, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(float[] path, IAnimator animator, IScheduler scheduler)
        {
            var distance = new List<float>();
            var sum = 0.0f;
            for (var i = 0; i < path.Length - 1; ++i)
            {
                var d = Mathf.Abs(path[i] - path[i + 1]);
                distance.Add(sum + d);
                sum += d;
            }
            return PlayInternal(animator, sum, scheduler)
                .Select(x =>
                {
                    var a = x * sum;
                    var i = 0;
                    for (; i < distance.Count - 1; i++)
                    {
                        if (distance[i] > a) break;
                    }

                    var b = i == 0 ? 0 : distance[i - 1];
                    return Mathf.LerpUnclamped(path[i], path[i + 1], (a - b) / (distance[i] - b));
                });
        }

        public static IObservable<float> Play(this IObservable<float> self, float from, float to, IAnimator animator)
        {
            return Play(self, from, to, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(this IObservable<float> self, float from, float to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }

        public static IObservable<float> Play(this IObservable<float> self, float from, float[] path, IAnimator animator)
        {
            return Play(self, from, path, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(this IObservable<float> self, float from, float[] path, IAnimator animator, IScheduler scheduler)
        {
            var merged = new float[path.Length + 1];
            merged[0] = from;
            Array.Copy(path, 0, merged, 1, path.Length);
            return self.Concat(Play(merged, animator, scheduler));
        }

        public static IObservable<float> Play(this IObservable<float> self, float to, IAnimator animator)
        {
            return Play(self, to, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(this IObservable<float> self, float to, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, to, animator, scheduler)).Switch();
        }

        public static IObservable<float> Play(this IObservable<float> self, float[] path, IAnimator animator)
        {
            return Play(self, path, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(this IObservable<float> self, float[] path, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x =>
            {
                var merged = new float[path.Length + 1];
                merged[0] = x;
                Array.Copy(path, 0, merged, 1, path.Length);
                return Observable.Return(x).Concat(Play(merged, animator, scheduler));
            }).Switch();
        }

        public static IObservable<float> PlayRelative(float from, float relative, IAnimator animator)
        {
            return PlayRelative(from, relative, animator, DefaultScheduler);
        }

        public static IObservable<float> PlayRelative(float from, float relative, IAnimator animator, IScheduler scheduler)
        {
            return Play(from, from + relative, animator, scheduler);
        }

        public static IObservable<float> PlayRelative(this IObservable<float> self, float from, float relative, IAnimator animator)
        {
            return PlayRelative(self, from, relative, animator, DefaultScheduler);
        }

        public static IObservable<float> PlayRelative(this IObservable<float> self, float from, float relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, from + relative, animator, scheduler));
        }

        public static IObservable<float> PlayRelative(this IObservable<float> self, float relative, IAnimator animator)
        {
            return PlayRelative(self, relative, animator, DefaultScheduler);
        }

        public static IObservable<float> PlayRelative(this IObservable<float> self, float relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, x + relative, animator, scheduler)).Switch();
        }

        public static IObservable<float> Lerp(this IObservable<float> self, float from, float to)
        {
            return self.Select(x => Mathf.LerpUnclamped(from, to, x));
        }
    }
}
