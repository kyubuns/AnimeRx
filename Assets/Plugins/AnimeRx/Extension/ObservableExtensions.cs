using System;
using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static class ObservableExtensions
    {
        public static IObservable<float> Range(this IObservable<float> source, float min, float max)
        {
            return source.Select(x => Mathf.Clamp(x - min, 0.0f, max) / (max - min));
        }

        public static IObservable<T> Loop<T>(this IObservable<T> source)
        {
            return source.Repeat();
        }

        public static IObservable<T> Loop<T>(this IObservable<T> source, int repeatCount)
        {
            return Observable.Range(0, repeatCount).Select(x => source).Concat();
        }
    }
}
