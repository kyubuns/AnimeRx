using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class GameObjectPositionExtensions
    {
        public static IDisposable SubscribeToPosition(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.SubscribeToPosition(gameObject.transform);
        }

        public static IDisposable SubscribeToPosition(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.SubscribeToPosition(gameObject.transform);
        }

        public static IDisposable SubscribeToPositionX(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToPositionX(gameObject.transform);
        }

        public static IDisposable SubscribeToPositionY(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToPositionY(gameObject.transform);
        }

        public static IDisposable SubscribeToPositionZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToPositionZ(gameObject.transform);
        }

        public static IObservable<Vector3> DoToPosition(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.DoToPosition(gameObject.transform);
        }

        public static IObservable<IList<float>> DoToPosition(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.DoToPosition(gameObject.transform);
        }

        public static IObservable<float> DoToPositionX(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToPositionX(gameObject.transform);
        }

        public static IObservable<float> DoToPositionY(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToPositionY(gameObject.transform);
        }

        public static IObservable<float> DoToPositionZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToPositionZ(gameObject.transform);
        }
    }
}
