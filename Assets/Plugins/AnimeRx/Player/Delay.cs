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
            return DelayInternal((float) duration.TotalSeconds, scheduler);
        }
    }
}