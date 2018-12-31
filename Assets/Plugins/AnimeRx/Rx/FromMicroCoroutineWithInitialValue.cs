using System;
using System.Collections;
using System.Threading;
using UniRx;
using UniRx.Operators;

namespace AnimeRx
{
    internal static class RxExtensions
    {
        public static IObservable<T> FromMicroCoroutineWithInitialValue<T>(
            Func<IObserver<T>, CancellationToken, IEnumerator> coroutine, T initialValue,
            FrameCountType frameCountType = FrameCountType.Update)
        {
            return new FromMicroCoroutineWithInitialValueObservable<T>(coroutine, initialValue, frameCountType);
        }

        internal class FromMicroCoroutineWithInitialValueObservable<T> : OperatorObservableBase<T>
        {
            readonly T initialValue;
            readonly Func<IObserver<T>, CancellationToken, IEnumerator> coroutine;
            readonly FrameCountType frameCountType;

            public FromMicroCoroutineWithInitialValueObservable(
                Func<IObserver<T>, CancellationToken, IEnumerator> coroutine, T initialValue,
                FrameCountType frameCountType)
                : base(false)
            {
                this.initialValue = initialValue;
                this.coroutine = coroutine;
                this.frameCountType = frameCountType;
            }

            protected override IDisposable SubscribeCore(IObserver<T> observer, IDisposable cancel)
            {
                observer.OnNext(this.initialValue);

                var microCoroutineObserver = new FromMicroCoroutine(observer, cancel);

#if (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
                var moreCancel = new CancellationDisposable();
                var token = moreCancel.Token;
#else
                var moreCancel = new BooleanDisposable();
                var token = new CancellationToken(moreCancel);
#endif

                switch (frameCountType)
                {
                    case FrameCountType.Update:
                        MainThreadDispatcher.StartUpdateMicroCoroutine(coroutine(microCoroutineObserver, token));
                        break;
                    case FrameCountType.FixedUpdate:
                        MainThreadDispatcher.StartFixedUpdateMicroCoroutine(coroutine(microCoroutineObserver, token));
                        break;
                    case FrameCountType.EndOfFrame:
                        MainThreadDispatcher.StartEndOfFrameMicroCoroutine(coroutine(microCoroutineObserver, token));
                        break;
                    default:
                        throw new ArgumentException("Invalid FrameCountType:" + frameCountType);
                }

                return moreCancel;
            }

            class FromMicroCoroutine : OperatorObserverBase<T, T>
            {
                public FromMicroCoroutine(IObserver<T> observer, IDisposable cancel) : base(observer, cancel)
                {
                }

                public override void OnNext(T value)
                {
                    try
                    {
                        base.observer.OnNext(value);
                    }
                    catch
                    {
                        Dispose();
                        throw;
                    }
                }

                public override void OnError(Exception error)
                {
                    try
                    {
                        observer.OnError(error);
                    }
                    finally
                    {
                        Dispose();
                    }
                }

                public override void OnCompleted()
                {
                    try
                    {
                        observer.OnCompleted();
                    }
                    finally
                    {
                        Dispose();
                    }
                }
            }
        }
    }
}
