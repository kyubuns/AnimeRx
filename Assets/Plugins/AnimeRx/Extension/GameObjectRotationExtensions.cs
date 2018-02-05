using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class GameObjectRotationExtensions
    {
        public static IDisposable SubscribeToRotation(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.SubscribeToRotation(gameObject.transform);
        }

        public static IDisposable SubscribeToRotation(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.SubscribeToRotation(gameObject.transform);
        }

        public static IDisposable SubscribeToRotationX(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToRotationX(gameObject.transform);
        }

        public static IDisposable SubscribeToRotationY(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToRotationY(gameObject.transform);
        }

        public static IDisposable SubscribeToRotationZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToRotationZ(gameObject.transform);
        }

        public static IObservable<Vector3> DoToRotation(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.DoToRotation(gameObject.transform);
        }

        public static IObservable<IList<float>> DoToRotation(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.DoToRotation(gameObject.transform);
        }

        public static IObservable<float> DoToRotationX(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToRotationX(gameObject.transform);
        }

        public static IObservable<float> DoToRotationY(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToRotationY(gameObject.transform);
        }

        public static IObservable<float> DoToRotationZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToRotationZ(gameObject.transform);
        }
    }
}
