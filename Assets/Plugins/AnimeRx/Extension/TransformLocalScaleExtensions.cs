using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class TransformLocalScaleExtensions
    {
        public static IObservable<Vector3> AnimeLocalScale(this Transform transform, Vector3 to, IAnimator animator)
        {
            return Anime.Play(transform.localScale, to, animator);
        }

        public static IObservable<Vector3> AnimeLocalScale(this Transform transform, Vector3 to, IAnimator animator, IScheduler scheduler)
        {
            return Anime.Play(transform.localScale, to, animator, scheduler);
        }

        public static IObservable<Vector3> AnimeLocalScaleRelative(this Transform transform, Vector3 relative, IAnimator animator)
        {
            return Anime.PlayRelative(transform.localScale, relative, animator);
        }

        public static IObservable<Vector3> AnimeLocalScaleRelative(this Transform transform, Vector3 relative, IAnimator animator, IScheduler scheduler)
        {
            return Anime.PlayRelative(transform.localScale, relative, animator, scheduler);
        }

        public static IDisposable SubscribeToLocalScale(this IObservable<Vector3> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.localScale = x);
        }

        public static IDisposable SubscribeToLocalScale(this IObservable<IList<float>> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.localScale = new Vector3(x[0], x[1], x[2]));
        }

        public static IDisposable SubscribeToLocalScaleX(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.localScale;
                p.x = x;
                t.localScale = p;
            });
        }

        public static IDisposable SubscribeToLocalScaleY(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.localScale;
                p.y = x;
                t.localScale = p;
            });
        }

        public static IDisposable SubscribeToLocalScaleZ(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.localScale;
                p.z = x;
                t.localScale = p;
            });
        }

        public static IObservable<Vector3> DoToLocalScale(this IObservable<Vector3> source, Transform transform)
        {
            return source.Do(x => transform.localScale = x);
        }

        public static IObservable<IList<float>> DoToLocalScale(this IObservable<IList<float>> source, Transform transform)
        {
            return source.Do(x => transform.localScale = new Vector3(x[0], x[1], x[2]));
        }

        public static IObservable<float> DoToLocalScaleX(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.localScale;
                p.x = x;
                transform.localScale = p;
            });
        }

        public static IObservable<float> DoToLocalScaleY(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.localScale;
                p.y = x;
                transform.localScale = p;
            });
        }

        public static IObservable<float> DoToLocalScaleZ(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.localScale;
                p.z = x;
                transform.localScale = p;
            });
        }
    }
}
