using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Vector2> Play(Vector2 from, Vector2 to, IAnimator animator)
        {
            return Play(from, to, animator, new TimeScheduler());
        }

        public static IObservable<Vector2> Play(Vector2 from, Vector2 to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Vector2.Distance(from, to), scheduler)
                .Select(x => Vector2.LerpUnclamped(from, to, x));
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 from, Vector2 to, IAnimator animator)
        {
            return Play(self, from, to, animator, new TimeScheduler());
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 from, Vector2 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 to, IAnimator animator)
        {
            return Play(self, to, animator, new TimeScheduler());
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, to, animator, scheduler)).Switch();
        }

        public static IObservable<Vector2> PlayRelative(Vector2 from, Vector2 relative, IAnimator animator)
        {
            return PlayRelative(from, relative, animator, new TimeScheduler());
        }

        public static IObservable<Vector2> PlayRelative(Vector2 from, Vector2 relative, IAnimator animator, IScheduler scheduler)
        {
            return Play(from, from + relative, animator, scheduler);
        }

        public static IObservable<Vector2> PlayRelative(this IObservable<Vector2> self, Vector2 from, Vector2 relative, IAnimator animator)
        {
            return PlayRelative(self, from, relative, animator, new TimeScheduler());
        }

        public static IObservable<Vector2> PlayRelative(this IObservable<Vector2> self, Vector2 from, Vector2 relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, from + relative, animator, scheduler));
        }
    }
}