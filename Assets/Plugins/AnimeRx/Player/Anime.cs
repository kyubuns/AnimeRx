using System.Collections;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        private static IScheduler defaultScheduler = new TimeScheduler();

        public static IScheduler DefaultScheduler
        {
            get { return defaultScheduler; }
            set { defaultScheduler = value; }
        }

        private static IObservable<float> PlayInternal(IAnimator animator, float distance, IScheduler scheduler)
        {
            return Observable.FromCoroutine<float>((observer, token) =>
                AnimationCoroutine(animator, distance, scheduler, observer, token));
        }

        private static IObservable<Unit> DelayInternal(float duration, IScheduler scheduler)
        {
            return Observable.FromCoroutine<Unit>((observer, token) =>
                DelayCoroutine(duration, scheduler, observer, token));
        }

        private static IEnumerator AnimationCoroutine(IAnimator animator, float distance, IScheduler scheduler, IObserver<float> observer, CancellationToken token)
        {
            var start = scheduler.Now;

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    observer.OnCompleted();
                    yield break;
                }

                var now = scheduler.Now - start;
                observer.OnNext(animator.CalcPosition(now, distance));
                if (animator.CalcFinishTime(distance) < now)
                {
                    break;
                }

                yield return null;
            }

            observer.OnCompleted();
        }

        private static IEnumerator DelayCoroutine(float duration, IScheduler scheduler, IObserver<Unit> observer, CancellationToken token)
        {
            var start = scheduler.Now;

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    observer.OnCompleted();
                    yield break;
                }

                var now = scheduler.Now - start;
                if (duration < now)
                {
                    break;
                }

                yield return null;
            }

            observer.OnCompleted();
        }
    }
}
