using UniRx;

namespace AnimeRx
{
    public static partial class Anime
    {
        public static IObservable<T> Stay<T>(T value)
        {
            return Observable.Return(value);
        }
    }
}
