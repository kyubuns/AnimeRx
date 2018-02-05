using System;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Unit> Delay(TimeSpan duration)
        {
            return Delay(duration, DefaultScheduler);
        }

        public static IObservable<Unit> Delay(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal(duration, scheduler);
        }

        public static IObservable<T> Delay<T>(TimeSpan duration)
        {
            return Delay<T>(duration, DefaultScheduler);
        }

        public static IObservable<T> Delay<T>(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal(duration, scheduler).Select(_ => default(T));
        }

        public static IObservable<T> Delay<T>(this IObservable<T> self, TimeSpan duration)
        {
            return self.Concat(Delay<T>(duration));
        }

        public static IObservable<T> Delay<T>(this IObservable<T> self, TimeSpan duration, IScheduler scheduler)
        {
            return self.Concat(Delay<T>(duration, scheduler));
        }

        private static IObservable<Unit> DelayInternal(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler);
        }
    }
}
