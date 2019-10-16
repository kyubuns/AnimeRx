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

        public static IObservable<T> Sleep<T>(float duration, T value)
        {
            return Sleep(duration, value, DefaultScheduler);
        }

        public static IObservable<T> Sleep<T>(float duration, T value, IScheduler scheduler)
        {
            return SleepInternal(duration, scheduler).Select(_ => value);
        }

        public static IObservable<T> Sleep<T>(this IObservable<T> self, float duration)
        {
            return Sleep(self, duration, DefaultScheduler);
        }

        public static IObservable<T> Sleep<T>(this IObservable<T> self, float duration, IScheduler scheduler)
        {
            var hot = self.Publish().RefCount();
            return Observable.Merge(hot, hot.ContinueWith(x => Sleep(duration, x, scheduler)));
        }

        public static IObservable<T> Sleep<T>(this IObservable<T> self, float duration, T value)
        {
            return self.Concat(Sleep(duration, value));
        }

        public static IObservable<T> Sleep<T>(this IObservable<T> self, float duration, T value, IScheduler scheduler)
        {
            return self.Concat(Sleep(duration, value, scheduler));
        }
    }
}
