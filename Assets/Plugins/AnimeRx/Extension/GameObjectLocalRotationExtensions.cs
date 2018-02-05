using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class GameObjectLocalRotationExtensions
    {
        public static IDisposable SubscribeToLocalRotation(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.SubscribeToLocalRotation(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalRotation(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.SubscribeToLocalRotation(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalRotationX(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalRotationX(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalRotationY(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalRotationY(gameObject.transform);
        }

        public static IDisposable SubscribeToLocalRotationZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.SubscribeToLocalRotationZ(gameObject.transform);
        }

        public static IObservable<Vector3> DoToLocalRotation(this IObservable<Vector3> source, GameObject gameObject)
        {
            return source.DoToLocalRotation(gameObject.transform);
        }

        public static IObservable<IList<float>> DoToLocalRotation(this IObservable<IList<float>> source, GameObject gameObject)
        {
            return source.DoToLocalRotation(gameObject.transform);
        }

        public static IObservable<float> DoToLocalRotationX(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalRotationX(gameObject.transform);
        }

        public static IObservable<float> DoToLocalRotationY(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalRotationY(gameObject.transform);
        }

        public static IObservable<float> DoToLocalRotationZ(this IObservable<float> source, GameObject gameObject)
        {
            return source.DoToLocalRotationZ(gameObject.transform);
        }
    }
}
