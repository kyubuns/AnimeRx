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
            return self.Select(x => Observable.Return(x).Concat(Play(x, to, animator, scheduler))).Switch();
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
            return self.Select(x => Observable.Return(x).Concat(Play(x, x + relative, animator, scheduler))).Switch();
        }
    }
}
