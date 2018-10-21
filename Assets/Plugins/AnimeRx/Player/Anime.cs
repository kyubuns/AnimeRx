using System;
using System.Collections;
using System.Threading;
using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        private static IScheduler defaultScheduler = new TimeScheduler();
        public const double TimeDelta = 0.00001;
        private const double EqualDelta = 0.02;

        public static IScheduler DefaultScheduler
        {
            get { return defaultScheduler; }
            set { defaultScheduler = value; }
        }

        public static IObservable<float> Play(IAnimator animator)
        {
            return Play(animator, DefaultScheduler);
        }

        public static IObservable<float> Play(IAnimator animator, IScheduler scheduler)
        {
            return PlayInternal(animator, 1.0f, scheduler);
        }

        private static IObservable<float> PlayInternal(IAnimator animator, float distance, IScheduler scheduler)
        {
            return Observable
                .Defer(() => Observable.Return(scheduler.Now))
                .ContinueWith(start =>
                    RxExtensions.FromMicroCoroutineWithInitialValue((observer, token) => AnimationCoroutine(animator, start, distance, scheduler, observer, token), animator.CalcPosition(scheduler.Now - start, distance))
                );
        }

        private static IObservable<Unit> SleepInternal(float duration, IScheduler scheduler)
        {
            return Observable
                .Defer(() => Observable.Return(scheduler.Now))
                .ContinueWith(start =>
                    RxExtensions.FromMicroCoroutineWithInitialValue<Unit>((observer, token) => DelayCoroutine(start, duration, scheduler, observer, token), Unit.Default)
                );
        }

        private static IEnumerator AnimationCoroutine(IAnimator animator, float start, float distance, IScheduler scheduler, IObserver<float> observer, CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    observer.OnCompleted();
                    yield break;
                }

                var now = scheduler.Now - start;
                if (animator.CalcFinishTime(distance) < now)
                {
                    break;
                }

                observer.OnNext(animator.CalcPosition(now, distance));
                yield return null;
            }

            observer.OnNext(animator.CalcPosition(animator.CalcFinishTime(distance), distance));
            observer.OnCompleted();
        }

        private static IEnumerator DelayCoroutine(float start, float duration, IScheduler scheduler, IObserver<Unit> observer, CancellationToken token)
        {
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

                observer.OnNext(Unit.Default);
                yield return null;
            }

            observer.OnNext(Unit.Default);
            observer.OnCompleted();
        }
    }
}
