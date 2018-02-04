using UniRx;
using UnityEngine;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<float> Play(float from, float to, IAnimator animator)
        {
            return Play(from, to, animator, new TimeScheduler());
        }

        public static IObservable<float> Play(float from ,float to, IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, Mathf.Abs(from - to), scheduler).Select(x => Mathf.LerpUnclamped(from, to, x));
        }
    }
}