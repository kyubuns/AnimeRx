using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class TransformExtensions
    {
        public static IDisposable SubscribeToPosition(this IObservable<Vector3> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.position = x);
        }

        public static IDisposable SubscribeToPosition(this IObservable<Vector2> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.position = x);
        }

        public static IDisposable SubscribeToPosition(this IObservable<IList<float>> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.position = x.Count >= 3 ? new Vector3(x[0], x[1], x[2]) : new Vector3(x[0], x[1]));
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

        public static IObservable<Vector2> DoToPosition(this IObservable<Vector2> source, Transform transform)
        {
            return source.Do(x => transform.position = x);
        }

        public static IObservable<IList<float>> DoToPosition(this IObservable<IList<float>> source, Transform transform)
        {
            return source.Do(x => transform.position = x.Count >= 3 ? new Vector3(x[0], x[1], x[2]) : new Vector3(x[0], x[1]));
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

        public static IDisposable SubscribeToLocalPosition(this IObservable<Vector3> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.localPosition = x);
        }

        public static IDisposable SubscribeToLocalPosition(this IObservable<Vector2> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.localPosition = x);
        }

        public static IDisposable SubscribeToLocalPosition(this IObservable<IList<float>> source, Transform transform)
        {
            return source.SubscribeWithState(transform, (x, t) => t.localPosition = x.Count >= 3 ? new Vector3(x[0], x[1], x[2]) : new Vector3(x[0], x[1]));
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

        public static IObservable<Vector2> DoToLocalPosition(this IObservable<Vector2> source, Transform transform)
        {
            return source.Do(x => transform.localPosition = x);
        }

        public static IObservable<IList<float>> DoToLocalPosition(this IObservable<IList<float>> source, Transform transform)
        {
            return source.Do(x => transform.localPosition = x.Count >= 3 ? new Vector3(x[0], x[1], x[2]) : new Vector3(x[0], x[1]));
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