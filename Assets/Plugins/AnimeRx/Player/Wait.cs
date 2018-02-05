using System;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Unit> Wait(TimeSpan duration)
        {
            return Wait(duration, DefaultScheduler);
        }

        public static IObservable<Unit> Wait(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler);
        }

        public static IObservable<T> Wait<T>(TimeSpan duration)
        {
            return Wait<T>(duration, DefaultScheduler);
        }

        public static IObservable<T> Wait<T>(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler).Select(_ => default(T));
        }

        public static IObservable<T> Wait<T>(TimeSpan duration, T value)
        {
            return Wait(duration, value, DefaultScheduler);
        }

        public static IObservable<T> Wait<T>(TimeSpan duration, T value, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler).Select(_ => default(T)).Stay(value);
        }

        public static IObservable<T> Wait<T>(this IObservable<T> self, TimeSpan duration)
        {
            return self.Concat(Wait<T>(duration));
        }

        public static IObservable<T> Wait<T>(this IObservable<T> self, TimeSpan duration, IScheduler scheduler)
        {
            return self.Concat(Wait<T>(duration, scheduler));
        }
    }
}
