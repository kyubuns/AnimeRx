namespace AnimeRx
{
    public interface IAnimator
    {
        float CalcFinishTime(float distance);
        float CalcPosition(float time, float distance);
    }
}