using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Color> Play(Color from, Color to, IAnimator animator)
        {
            return Play(from, to, animator, new TimeScheduler());
        }

        public static IObservable<Color> Play(Color from, Color to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Vector4.Distance(from, to), scheduler)
                .Select(x => Color.LerpUnclamped(from, to, x));
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color from, Color to, IAnimator animator)
        {
            return Play(self, from, to, animator, new TimeScheduler());
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color from, Color to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color to, IAnimator animator)
        {
            return Play(self, to, animator, new TimeScheduler());
        }

        public static IObservable<Color> Play(this IObservable<Color> self, Color to, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, to, animator, scheduler)).Switch();
        }

        public static IObservable<Color> PlayRelative(Color from, Color relative, IAnimator animator)
        {
            return PlayRelative(from, relative, animator, new TimeScheduler());
        }

        public static IObservable<Color> PlayRelative(Color from, Color relative, IAnimator animator, IScheduler scheduler)
        {
            return Play(from, from + relative, animator, scheduler);
        }

        public static IObservable<Color> PlayRelative(this IObservable<Color> self, Color from, Color relative, IAnimator animator)
        {
            return PlayRelative(self, from, relative, animator, new TimeScheduler());
        }

        public static IObservable<Color> PlayRelative(this IObservable<Color> self, Color from, Color relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, from + relative, animator, scheduler));
        }
    }
}