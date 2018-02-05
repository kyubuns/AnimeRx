using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<float> Play(float from, float to, IAnimator animator)
        {
            return Play(from, to, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(float from, float to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Mathf.Abs(from - to), scheduler)
                .Select(x => Mathf.LerpUnclamped(from, to, x));
        }

        public static IObservable<float> Play(this IObservable<float> self, float from, float to, IAnimator animator)
        {
            return Play(self, from, to, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(this IObservable<float> self, float from, float to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }

        public static IObservable<float> Play(this IObservable<float> self, float to, IAnimator animator)
        {
            return Play(self, to, animator, DefaultScheduler);
        }

        public static IObservable<float> Play(this IObservable<float> self, float to, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, to, animator, scheduler)).Switch();
        }

        public static IObservable<float> PlayRelative(float from, float relative, IAnimator animator)
        {
            return PlayRelative(from, relative, animator, DefaultScheduler);
        }

        public static IObservable<float> PlayRelative(float from, float relative, IAnimator animator, IScheduler scheduler)
        {
            return Play(from, from + relative, animator, scheduler);
        }

        public static IObservable<float> PlayRelative(this IObservable<float> self, float from, float relative, IAnimator animator)
        {
            return PlayRelative(self, from, relative, animator, DefaultScheduler);
        }

        public static IObservable<float> PlayRelative(this IObservable<float> self, float from, float relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, from + relative, animator, scheduler));
        }
    }
}
