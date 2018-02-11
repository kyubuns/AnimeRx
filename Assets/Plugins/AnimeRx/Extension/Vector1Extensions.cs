using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class Vector1Extensions
    {
        public static IObservable<float> Play(this float source, float to, IAnimator animator)
        {
            return Anime.Play(source, to, animator);
        }

        public static IObservable<float> Play(this float source, float to, IAnimator animator, IScheduler scheduler)
        {
            return Anime.Play(source, to, animator, scheduler);
        }

        public static IObservable<float> Play(this float source, float[] path, IAnimator animator)
        {
            var merged = new float[path.Length + 1];
            merged[0] = source;
            Array.Copy(path, 0, merged, 1, path.Length);
            return Anime.Play(merged, animator);
        }

        public static IObservable<float> Play(this float source, float[] path, IAnimator animator, IScheduler scheduler)
        {
            var merged = new float[path.Length + 1];
            merged[0] = source;
            Array.Copy(path, 0, merged, 1, path.Length);
            return Anime.Play(merged, animator, scheduler);
        }

        public static IObservable<float> PlayRelative(this float source, float relative, IAnimator animator)
        {
            return Anime.PlayRelative(source, relative, animator);
        }

        public static IObservable<float> PlayRelative(this float source, float relative, IAnimator animator, IScheduler scheduler)
        {
            return Anime.PlayRelative(source, relative, animator, scheduler);
        }

        public static IObservable<float> Sum(this IObservable<IList<float>> source)
        {
            return source.Select(x =>
            {
                return x.Aggregate(0.0f, (current, xx) => current + xx);
            });
        }
    }
}
