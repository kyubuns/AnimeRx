using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Color> Play(Color from, Color to, IAnimator animator)
        {
            return Play(from, to, animator, DefaultScheduler);
        }

        public static IObservable<Color> Play(Color from, Color to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Vector4.Distance(from, to), scheduler)
                .Select(x => Color.LerpUnclamped(from, to, x));
        }

        public static IObservable<Color> PlayIn(Color from, Color inEnd, Color to, IAnimator inAnimator)
        {
            return PlayIn(from, inEnd, to, inAnimator, DefaultScheduler);
        }

        public static IObservable<Color> PlayIn(Color from, Color inEnd, Color to, IAnimator inAnimator, IScheduler scheduler)
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

        public static IObservable<Color> PlayOut(Color from, Color outStart, Color to, IAnimator outAnimator)
        {
            return PlayOut(from, outStart, to, outAnimator, DefaultScheduler);
        }

        public static IObservable<Color> PlayOut(Color from, Color outStart, Color to, IAnimator outAnimator, IScheduler scheduler)
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

        public static IObservable<Color> PlayInOut(Color from, Color inEnd, Color outStart, Color to, IAnimator inAnimator, IAnimator outAnimator)
        {
            return PlayInOut(from, inEnd, outStart, to, inAnimator, outAnimator, DefaultScheduler);
        }

        public static IObservable<Color> PlayInOut(Color from, Color inEnd, Color outStart, Color to, IAnimator inAnimator, IAnimator outAnimator, IScheduler scheduler)
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

        public static IObservable<Color> Play(Color[] path, IAnimator animator)
        {
            return Play(path, animator, DefaultScheduler);
        }

        public static IObservable<Color> Play(Color[] path, IAnimator animator, IScheduler scheduler)
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
                    return Color.LerpUnclamped(path[i], path[i + 1], (a - b) / (distance[i] - b));
                });
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color from, Color to, IAnimator animator)
        {
            return Play(self, from, to, animator, DefaultScheduler);
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color from, Color to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color from, Color[] path, IAnimator animator)
        {
            return Play(self, from, path, animator, DefaultScheduler);
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color from, Color[] path, IAnimator animator, IScheduler scheduler)
        {
            var merged = new Color[path.Length + 1];
            merged[0] = from;
            Array.Copy(path, 0, merged, 1, path.Length);
            return self.Concat(Play(merged, animator, scheduler));
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color to, IAnimator animator)
        {
            return Play(self, to, animator, DefaultScheduler);
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color to, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, to, animator, scheduler)).Switch();
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color[] path, IAnimator animator)
        {
            return Play(self, path, animator, DefaultScheduler);
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color[] path, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x =>
            {
                var merged = new Color[path.Length + 1];
                merged[0] = x;
                Array.Copy(path, 0, merged, 1, path.Length);
                return Observable.Return(x).Concat(Play(merged, animator, scheduler));
            }).Switch();
        }

        public static IObservable<Color> PlayRelative(Color from, Color relative, IAnimator animator)
        {
            return PlayRelative(from, relative, animator, DefaultScheduler);
        }

        public static IObservable<Color> PlayRelative(Color from, Color relative, IAnimator animator, IScheduler scheduler)
        {
            return Play(from, from + relative, animator, scheduler);
        }

        public static IObservable<Color> PlayRelative(this IObservable<Color> self, Color from, Color relative, IAnimator animator)
        {
            return PlayRelative(self, from, relative, animator, DefaultScheduler);
        }

        public static IObservable<Color> PlayRelative(this IObservable<Color> self, Color from, Color relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, from + relative, animator, scheduler));
        }

        public static IObservable<Color> PlayRelative(this IObservable<Color> self, Color relative, IAnimator animator)
        {
            return PlayRelative(self, relative, animator, DefaultScheduler);
        }

        public static IObservable<Color> PlayRelative(this IObservable<Color> self, Color relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, x + relative, animator, scheduler)).Switch();
        }

        public static IObservable<Color> Lerp(this IObservable<float> self, Color from, Color to)
        {
            return self.Select(x => Color.LerpUnclamped(from, to, x));
        }
    }
}
