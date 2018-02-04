using System.Collections;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        private static IObservable<float> PlayInternal(IAnimator animator, float distance, IScheduler scheduler)
        {
            return Observable.FromCoroutine<float>((observer, token) =>
                AnimationCoroutine(animator, distance, scheduler, observer, token));
        }

        private static IEnumerator AnimationCoroutine(IAnimator animator, float distance, IScheduler scheduler, IObserver<float> observer, CancellationToken token)
        {
            scheduler.Start();

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    observer.OnCompleted();
                    yield break;
                }

                var now = scheduler.Now;
                observer.OnNext(animator.CalcPosition(now, distance));

                if (animator.CalcFinishTime(distance) < now)
                {
                    break;
                }

                yield return null;
            }

            observer.OnCompleted();
        }
    }
}