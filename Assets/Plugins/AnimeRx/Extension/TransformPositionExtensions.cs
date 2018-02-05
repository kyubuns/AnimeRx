using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class TransformPositionExtensions
    {
        public static IDisposable SubscribeToPosition(this IObservable<Vector3> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.position = x);
        }

        public static IDisposable SubscribeToPosition(this IObservable<IList<float>> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.position = new Vector3(x[0], x[1], x[2]));
        }

        public static IDisposable SubscribeToPositionX(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.position;
                p.x = x;
                t.position = p;
            });
        }

        public static IDisposable SubscribeToPositionY(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.position;
                p.y = x;
                t.position = p;
            });
        }

        public static IDisposable SubscribeToPositionZ(this IObservable<float> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) =>
            {
                var p = t.position;
                p.z = x;
                t.position = p;
            });
        }

        public static IObservable<Vector3> DoToPosition(this IObservable<Vector3> source, Transform transform)
        {
            return source.Do(x => transform.position = x);
        }

        public static IObservable<IList<float>> DoToPosition(this IObservable<IList<float>> source, Transform transform)
        {
            return source.Do(x => transform.position = new Vector3(x[0], x[1], x[2]));
        }

        public static IObservable<float> DoToPositionX(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.position;
                p.x = x;
                transform.position = p;
            });
        }

        public static IObservable<float> DoToPositionY(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.position;
                p.y = x;
                transform.position = p;
            });
        }

        public static IObservable<float> DoToPositionZ(this IObservable<float> source, Transform transform)
        {
            return source.Do(x =>
            {
                var p = transform.position;
                p.z = x;
                transform.position = p;
            });
        }
    }
}
