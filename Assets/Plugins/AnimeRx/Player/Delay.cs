using System;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Unit> Delay(TimeSpan duration)
        {
            return Delay(duration, new TimeScheduler());
        }

        public static IObservable<Unit> Delay(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal(duration, scheduler);
        }

        public static IObservable<T> Delay<T>(TimeSpan duration)
        {
            return Delay<T>(duration, new TimeScheduler());
        }

        public static IObservable<T> Delay<T>(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal(duration, scheduler).Select(_ => default(T));
        }

        private static IObservable<Unit> DelayInternal(TimeSpan duration, IScheduler scheduler)
        {
            return DelayInternal((float) duration.TotalSeconds, scheduler);
        }
    }
}