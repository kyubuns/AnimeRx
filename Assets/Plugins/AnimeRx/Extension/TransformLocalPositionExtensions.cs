using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class TransformLocalPositionExtensions
    {
        public static IObservable<Vector3> AnimeLocalPosition(this Transform transform, Vector3 from, Vector3 to, IAnimator animator)
        {
            return Anime.Play(from, to, animator);
        }

        public static IObservable<Vector3> AnimeLocalPosition(this Transform transform, Vector3 to, IAnimator animator)
        {
            return Anime.Play(transform.localPosition, to, animator);
        }

        public static IObservable<Vector3> AnimeLocalPosition(this Transform transform, Vector3 from, Vector3 to, IAnimator animator, IScheduler scheduler)
        {
            return Anime.Play(from, to, animator, scheduler);
        }

        public static IObservable<Vector3> AnimeLocalPosition(this Transform transform, Vector3 to, IAnimator animator, IScheduler scheduler)
        {
            return Anime.Play(transform.localPosition, to, animator, scheduler);
        }

        public static IDisposable SubscribeToLocalPosition(this IObservable<Vector3> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.localPosition = x);
        }

        public static IDisposable SubscribeToLocalPosition(this IObservable<IList<float>> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.localPosition = new Vector3(x[0], x[1], x[2]));
        }

        public static IDisposable SubscribeToLocalPositionX(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.localPosition;
                p.x = x;
                t.localPosition = p;
            });
        }

        public static IDisposable SubscribeToLocalPositionY(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.localPosition;
                p.y = x;
                t.localPosition = p;
            });
        }

        public static IDisposable SubscribeToLocalPositionZ(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.localPosition;
                p.z = x;
                t.localPosition = p;
            });
        }

        public static IObservable<Vector3> DoToLocalPosition(this IObservable<Vector3> source, Transform transform)
        {
            return source.Do(x => transform.localPosition = x);
        }

        public static IObservable<IList<float>> DoToLocalPosition(this IObservable<IList<float>> source, Transform transform)
        {
            return source.Do(x => transform.localPosition = new Vector3(x[0], x[1], x[2]));
        }

        public static IObservable<float> DoToLocalPositionX(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.localPosition;
                p.x = x;
                transform.localPosition = p;
            });
        }

        public static IObservable<float> DoToLocalPositionY(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.localPosition;
                p.y = x;
                transform.localPosition = p;
            });
        }

        public static IObservable<float> DoToLocalPositionZ(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.localPosition;
                p.z = x;
                transform.localPosition = p;
            });
        }
    }
}
