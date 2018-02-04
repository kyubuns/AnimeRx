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

        public static IObservable<Vector3> Play(Vector3 from ,Vector3 to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Vector3.Distance(from, to), scheduler).Select(x => Vector3.LerpUnclamped(from, to, x));
        }

        public static IObservable<Vector3> Play(this IObservable<Vector3> self, Vector3 from, Vector3 to, IAnimator animator)
        {
            return self.Concat(Play(from, to, animator));
        }

        public static IObservable<Vector3> Play(this IObservable<Vector3> self, Vector3 from, Vector3 to, IAnimator animator, IScheduler scheduler)
        {
            return self.Concat(Play(from, to, animator, scheduler));
        }
    }
}