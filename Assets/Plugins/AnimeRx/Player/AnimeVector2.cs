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
            return self.Concat(Play(from, to, animator));
        }

        public static IObservable<Vector2> Play(this IObservable<Vector2> self, Vector2 from, Vector2 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }
    }
}