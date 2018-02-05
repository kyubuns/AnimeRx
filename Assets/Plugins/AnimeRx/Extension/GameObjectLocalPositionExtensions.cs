using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class GameObjectLocalPositionExtensions
    {
        public static IDisposable SubscribeToLocalPosition(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.SubscribeToLocalPosition(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalPosition(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.SubscribeToLocalPosition(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalPositionX(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalPositionX(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalPositionY(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalPositionY(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalPositionZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalPositionZ(gameObject.transform);
        }

        public static IObservable<Vector3> DoToLocalPosition(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.DoToLocalPosition(gameObject.transform);
        }

        public static IObservable<IList<float>> DoToLocalPosition(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.DoToLocalPosition(gameObject.transform);
        }

        public static IObservable<float> DoToLocalPositionX(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalPositionX(gameObject.transform);
        }

        public static IObservable<float> DoToLocalPositionY(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalPositionY(gameObject.transform);
        }

        public static IObservable<float> DoToLocalPositionZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalPositionZ(gameObject.transform);
        }
    }
}
