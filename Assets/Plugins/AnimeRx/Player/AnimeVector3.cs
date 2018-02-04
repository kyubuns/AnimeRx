using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<Vector3> Play(Vector3 from, Vector3 to, IAnimator animator)
        {
            return Play(from, to, animator, new TimeScheduler());
        }

        public static IObservable<Vector3> Play(Vector3 from, Vector3 to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Vector3.Distance(from, to), scheduler)
                .Select(x => Vector3.LerpUnclamped(from, to, x));
        }

        public static IObservable<Vector3> Play(this IObservable<Vector3> self, Vector3 from, Vector3 to, IAnimator animator)
        {
            return Play(self, from, to, animator, new TimeScheduler());
        }

        public static IObservable<Vector3> Play(this IObservable<Vector3> self, Vector3 from, Vector3 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }

        public static IObservable<Vector3> Play(this IObservable<Vector3> self, Vector3 to, IAnimator animator)
        {
            return Play(self, to, animator, new TimeScheduler());
        }

        public static IObservable<Vector3> Play(this IObservable<Vector3> self, Vector3 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Select(x => Play(x, to, animator, scheduler)).Switch();
        }

        public static IObservable<Vector3> PlayRelative(Vector3 from, Vector3 relative, IAnimator animator)
        {
            return PlayRelative(from, relative, animator, new TimeScheduler());
        }

        public static IObservable<Vector3> PlayRelative(Vector3 from, Vector3 relative, IAnimator animator, IScheduler scheduler)
        {
            return Play(from, from + relative, animator, scheduler);
        }

        public static IObservable<Vector3> PlayRelative(this IObservable<Vector3> self, Vector3 from, Vector3 relative, IAnimator animator)
        {
            return PlayRelative(self, from, relative, animator, new TimeScheduler());
        }

        public static IObservable<Vector3> PlayRelative(this IObservable<Vector3> self, Vector3 from, Vector3 relative, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, from + relative, animator, scheduler));
        }
    }
}