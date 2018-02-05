using System;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<T> Stay<T>(T value)
        {
            return Observable.Return(value);
        }

        public static IObservable<T> Stay<T>(this IObservable<T> self, T value)
        {
            return self.Concat(Stay(value));
        }
    }
}
