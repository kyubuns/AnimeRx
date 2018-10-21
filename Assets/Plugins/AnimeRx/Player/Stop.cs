using System;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Unit> Sleep(TimeSpan duration)
        {
            return Sleep(duration, DefaultScheduler);
        }

        public static IObservable<Unit> Sleep(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler);
        }

        public static IObservable<T> Sleep<T>(TimeSpan duration)
        {
            return Sleep<T>(duration, DefaultScheduler);
        }

        public static IObservable<T> Sleep<T>(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler).Select(_ => default(T));
        }

        public static IObservable<T> Sleep<T>(TimeSpan duration, T value)
        {
            return Sleep(duration, value, DefaultScheduler);
        }

        public static IObservable<T> Sleep<T>(TimeSpan duration, T value, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler).Select(_ => default(T)).Stay(value);
        }

        public static IObservable<T> Sleep<T>(this IObservable<T> self, TimeSpan duration)
        {
            return self.Concat(Sleep<T>(duration));
        }

        public static IObservable<T> Sleep<T>(this IObservable<T> self, TimeSpan duration, IScheduler scheduler)
        {
            return self.Concat(Sleep<T>(duration, scheduler));
        }
    }
}
