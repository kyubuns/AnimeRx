using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Vector2> Play(Vector2 from, Vector2 to, IAnimator animator)
        {
            return Play(from, to, animator, DefaultScheduler);
        }

        public static IObservable<Vector2> Play(Vector2 from, Vector2 to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Vector2.Distance(from, to), scheduler)
                .Select(x => Vector2.LerpUnclamped(from, to, x));
        }

        public static IObservable<Vector2> Play(Vector2[] path, IAnimator animator)
        {
            return Play(path, animator, DefaultScheduler);
        }

        public static IObservable<Vector2> Play(Vector2[] path, IAnimator animator, IScheduler scheduler)
        {
            var distance = new List<float>();
            var sum = 0.0f;
            for (var i = 0; i < path.Length - 1; ++i)
            {
                var d = Vector2.Distance(path[i], path[i + 1]);
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
                    return Vector2.LerpUnclamped(path[i], path[i + 1], (a - b) / (distance[i] - b));
                });
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 from, Vector2 to, IAnimator animator)
        {
            return Play(self, from, to, animator, DefaultScheduler);
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 from, Vector2 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 to, IAnimator animator)
        {
            return Play(self, to, animator, DefaultScheduler);
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, to, animator, scheduler)).Switch();
        }

        public static IObservable<Vector2> PlayRelative(Vector2 from, Vector2 relative, IAnimator animator)
        {
            return PlayRelative(from, relative, animator, DefaultScheduler);
        }

        public static IObservable<Vector2> PlayRelative(Vector2 from, Vector2 relative, IAnimator animator, IScheduler scheduler)
        {
            return Play(from, from + relative, animator, scheduler);
        }

        public static IObservable<Vector2> PlayRelative(this IObservable<Vector2> self, Vector2 from, Vector2 relative, IAnimator animator)
        {
            return PlayRelative(self, from, relative, animator, DefaultScheduler);
        }

        public static IObservable<Vector2> PlayRelative(this IObservable<Vector2> self, Vector2 from, Vector2 relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, from + relative, animator, scheduler));
        }
    }
}
