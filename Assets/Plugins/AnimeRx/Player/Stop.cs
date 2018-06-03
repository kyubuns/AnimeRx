using System;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Unit> Stop(TimeSpan duration)
        {
            return Stop(duration, DefaultScheduler);
        }

        public static IObservable<Unit> Stop(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler);
        }

        public static IObservable<T> Stop<T>(TimeSpan duration)
        {
            return Stop<T>(duration, DefaultScheduler);
        }

        public static IObservable<T> Stop<T>(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler).Select(_ => default(T));
        }

        public static IObservable<T> Stop<T>(TimeSpan duration, T value)
        {
            return Stop(duration, value, DefaultScheduler);
        }

        public static IObservable<T> Stop<T>(TimeSpan duration, T value, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler).Select(_ => default(T)).Stay(value);
        }

        public static IObservable<T> Stop<T>(this IObservable<T> self, TimeSpan duration)
        {
            return self.Concat(Stop<T>(duration));
        }

        public static IObservable<T> Stop<T>(this IObservable<T> self, TimeSpan duration, IScheduler scheduler)
        {
            return self.Concat(Stop<T>(duration, scheduler));
        }
    }
}
