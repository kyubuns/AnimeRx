using System;
using UniRx;
namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Unit> Sleep(float duration)
        {
            return Sleep(duration, DefaultScheduler);
        }

        public static IObservable<Unit> Sleep(float duration, IScheduler scheduler)
        {
            return SleepInternal(duration, scheduler);
        }

        public static IObservable<T> Sleep<T>(float duration)
        {
            return Sleep<T>(duration, DefaultScheduler);
        }

        public static IObservable<T> Sleep<T>(float duration, IScheduler scheduler)
        {
            return SleepInternal(duration, scheduler).Select(_ => default(T));
        }

        public static IObservable<T> Sleep<T>(float duration, T value)
        {
            return Sleep(duration, value, DefaultScheduler);
        }

        public static IObservable<T> Sleep<T>(float duration, T value, IScheduler scheduler)
        {
            return SleepInternal(duration, scheduler).Select(_ => default(T)).Stay(value);
        }

        public static IObservable<T> Sleep<T>(this IObservable<T> self, float duration)
        {
            return self.Concat(Sleep<T>(duration));
        }

        public static IObservable<T> Sleep<T>(this IObservable<T> self, float duration, IScheduler scheduler)
        {
            return self.Concat(Sleep<T>(duration, scheduler));
        }
    }
}
