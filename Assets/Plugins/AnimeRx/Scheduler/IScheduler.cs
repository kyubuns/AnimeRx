namespace AnimeRx
{
    public interface IScheduler
    {
        void Start();
        float Now { get; }
    }
}