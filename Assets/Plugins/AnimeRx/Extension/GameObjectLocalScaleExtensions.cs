using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class GameObjectLocalScaleExtensions
    {
        public static IDisposable SubscribeToLocalScale(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.SubscribeToLocalScale(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalScale(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.SubscribeToLocalScale(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalScaleX(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalScaleX(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalScaleY(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalScaleY(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalScaleZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalScaleZ(gameObject.transform);
        }

        public static IObservable<Vector3> DoToLocalScale(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.DoToLocalScale(gameObject.transform);
        }

        public static IObservable<IList<float>> DoToLocalScale(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.DoToLocalScale(gameObject.transform);
        }

        public static IObservable<float> DoToLocalScaleX(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalScaleX(gameObject.transform);
        }

        public static IObservable<float> DoToLocalScaleY(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalScaleY(gameObject.transform);
        }

        public static IObservable<float> DoToLocalScaleZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalScaleZ(gameObject.transform);
        }
    }
}
