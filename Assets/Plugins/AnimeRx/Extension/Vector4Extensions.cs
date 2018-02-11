using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class Vector4Extensions
    {
        public static IObservable<Vector4> Play(this Vector4 source, Vector4 to, IAnimator animator)
        {
            return Anime.Play(source, to, animator);
        }

        public static IObservable<Vector4> Play(this Vector4 source, Vector4 to, IAnimator animator, IScheduler scheduler)
        {
            return Anime.Play(source, to, animator, scheduler);
        }

        public static IObservable<Vector4> Play(this Vector4 source, Vector4[] path, IAnimator animator)
        {
            var merged = new Vector4[path.Length + 1];
            merged[0] = source;
            Array.Copy(path, 0, merged, 1, path.Length);
            return Anime.Play(merged, animator);
        }

        public static IObservable<Vector4> Play(this Vector4 source, Vector4[] path, IAnimator animator, IScheduler scheduler)
        {
            var merged = new Vector4[path.Length + 1];
            merged[0] = source;
            Array.Copy(path, 0, merged, 1, path.Length);
            return Anime.Play(merged, animator, scheduler);
        }

        public static IObservable<Vector4> PlayRelative(this Vector4 source, Vector4 relative, IAnimator animator)
        {
            return Anime.PlayRelative(source, relative, animator);
        }

        public static IObservable<Vector4> PlayRelative(this Vector4 source, Vector4 relative, IAnimator animator, IScheduler scheduler)
        {
            return Anime.PlayRelative(source, relative, animator, scheduler);
        }

        public static IObservable<Vector4> Sum(this IObservable<IList<Vector4>> source)
        {
            return source.Select(x =>
            {
                return x.Aggregate(new Vector4(), (current, xx) => current + xx);
            });
        }
    }
}
