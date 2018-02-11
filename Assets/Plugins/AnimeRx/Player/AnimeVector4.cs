using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Vector4> Play(Vector4 from, Vector4 to, IAnimator animator)
        {
            return Play(from, to, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> Play(Vector4 from, Vector4 to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Vector4.Distance(from, to), scheduler)
                .Select(x => Vector4.LerpUnclamped(from, to, x));
        }

        public static IObservable<Vector4> PlayIn(Vector4 from, Vector4 inEnd, Vector4 to, IAnimator inAnimator)
        {
            return PlayIn(from, inEnd, to, inAnimator, DefaultScheduler);
        }

        public static IObservable<Vector4> PlayIn(Vector4 from, Vector4 inEnd, Vector4 to, IAnimator inAnimator, IScheduler scheduler)
        {
            var velocity = inAnimator.CalcFinishVelocity(Vector4.Distance(inEnd, from));
            var linearAnimator = Easing.Linear(velocity);
            var compositeAnimator = new CompositeAnimator(new[]
            {
                Tuple.Create(inAnimator, Vector4.Distance(inEnd, from)),
                Tuple.Create(linearAnimator, Vector4.Distance(to, inEnd)),
            });
            return Play(new[] {from, inEnd, to}, compositeAnimator, scheduler);
        }

        public static IObservable<Vector4> PlayOut(Vector4 from, Vector4 outStart, Vector4 to, IAnimator outAnimator)
        {
            return PlayOut(from, outStart, to, outAnimator, DefaultScheduler);
        }

        public static IObservable<Vector4> PlayOut(Vector4 from, Vector4 outStart, Vector4 to, IAnimator outAnimator, IScheduler scheduler)
        {
            var velocity = outAnimator.CalcStartVelocity(Vector4.Distance(to, outStart));
            var linearAnimator = Easing.Linear(velocity);
            var compositeAnimator = new CompositeAnimator(new[]
            {
                Tuple.Create(linearAnimator, Vector4.Distance(outStart, from)),
                Tuple.Create(outAnimator, Vector4.Distance(to, outStart)),
            });
            return Play(new[] {from, outStart, to}, compositeAnimator, scheduler);
        }

        public static IObservable<Vector4> PlayInOut(Vector4 from, Vector4 inEnd, Vector4 outStart, Vector4 to, IAnimator inAnimator, IAnimator outAnimator)
        {
            return PlayInOut(from, inEnd, outStart, to, inAnimator, outAnimator, DefaultScheduler);
        }

        public static IObservable<Vector4> PlayInOut(Vector4 from, Vector4 inEnd, Vector4 outStart, Vector4 to, IAnimator inAnimator, IAnimator outAnimator, IScheduler scheduler)
        {
            var inVelocity = inAnimator.CalcFinishVelocity(Vector4.Distance(inEnd, from)).PerSecond;
            var outVelocity = outAnimator.CalcStartVelocity(Vector4.Distance(to, outStart)).PerSecond;
            IAnimator linearAnimator;

            if (Math.Abs(inVelocity - outVelocity) < EqualDelta)
            {
                linearAnimator = Motion.Uniform((float) ((inVelocity + outVelocity) / 2.0));
            }
            else
            {
                var accel = (outVelocity * outVelocity - inVelocity * inVelocity) / (2.0f * Vector4.Distance(outStart, inEnd));
                linearAnimator = Motion.Acceleration((float) accel, (float) inVelocity);
            }

            var compositeAnimator = new CompositeAnimator(new[]
            {
                Tuple.Create(inAnimator, Vector4.Distance(inEnd, from)),
                Tuple.Create(linearAnimator, Vector4.Distance(outStart, inEnd)),
                Tuple.Create(outAnimator, Vector4.Distance(to, outStart)),
            });
            return Play(new[] {from, inEnd, outStart, to}, compositeAnimator, scheduler);
        }

        public static IObservable<Vector4> Play(Vector4[] path, IAnimator animator)
        {
            return Play(path, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> Play(Vector4[] path, IAnimator animator, IScheduler scheduler)
        {
            var distance = new List<float>();
            var sum = 0.0f;
            for (var i = 0; i < path.Length - 1; ++i)
            {
                var d = Vector4.Distance(path[i], path[i + 1]);
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
                    return Vector4.LerpUnclamped(path[i], path[i + 1], (a - b) / (distance[i] - b));
                });
        }

        public static IObservable<Vector4> Play(this IObservable<Vector4> self, Vector4 from, Vector4 to, IAnimator animator)
        {
            return Play(self, from, to, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> Play(this IObservable<Vector4> self, Vector4 from, Vector4 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }

        public static IObservable<Vector4> Play(this IObservable<Vector4> self, Vector4 from, Vector4[] path, IAnimator animator)
        {
            return Play(self, from, path, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> Play(this IObservable<Vector4> self, Vector4 from, Vector4[] path, IAnimator animator, IScheduler scheduler)
        {
            var merged = new Vector4[path.Length + 1];
            merged[0] = from;
            Array.Copy(path, 0, merged, 1, path.Length);
            return self.Concat(Play(merged, animator, scheduler));
        }

        public static IObservable<Vector4> Play(this IObservable<Vector4> self, Vector4 to, IAnimator animator)
        {
            return Play(self, to, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> Play(this IObservable<Vector4> self, Vector4 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, to, animator, scheduler)).Switch();
        }

        public static IObservable<Vector4> Play(this IObservable<Vector4> self, Vector4[] path, IAnimator animator)
        {
            return Play(self, path, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> Play(this IObservable<Vector4> self, Vector4[] path, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x =>
            {
                var merged = new Vector4[path.Length + 1];
                merged[0] = x;
                Array.Copy(path, 0, merged, 1, path.Length);
                return Observable.Return(x).Concat(Play(merged, animator, scheduler));
            }).Switch();
        }

        public static IObservable<Vector4> PlayRelative(Vector4 from, Vector4 relative, IAnimator animator)
        {
            return PlayRelative(from, relative, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> PlayRelative(Vector4 from, Vector4 relative, IAnimator animator, IScheduler scheduler)
        {
            return Play(from, from + relative, animator, scheduler);
        }

        public static IObservable<Vector4> PlayRelative(this IObservable<Vector4> self, Vector4 from, Vector4 relative, IAnimator animator)
        {
            return PlayRelative(self, from, relative, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> PlayRelative(this IObservable<Vector4> self, Vector4 from, Vector4 relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, from + relative, animator, scheduler));
        }

        public static IObservable<Vector4> PlayRelative(this IObservable<Vector4> self, Vector4 relative, IAnimator animator)
        {
            return PlayRelative(self, relative, animator, DefaultScheduler);
        }

        public static IObservable<Vector4> PlayRelative(this IObservable<Vector4> self, Vector4 relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, x + relative, animator, scheduler)).Switch();
        }

        public static IObservable<Vector4> Lerp(this IObservable<float> self, Vector4 from, Vector4 to)
        {
            return self.Select(x => Vector4.LerpUnclamped(from, to, x));
        }
    }
}
