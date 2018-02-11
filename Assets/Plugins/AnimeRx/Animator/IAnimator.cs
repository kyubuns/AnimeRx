namespace AnimeRx
{
    public interface IAnimator
    {
        float CalcFinishTime(float distance);
        float CalcPosition(float time, float distance);
    }

    public static class AnimatorExtensions
    {
        public static Velocity CalcFinishVelocity(this IAnimator animator, float distance)
        {
            var finishTime = animator.CalcFinishTime(distance);
            var pos1 = animator.CalcPosition(finishTime - (float) Anime.TimeDelta, distance);
            var pos2 = animator.CalcPosition(finishTime, distance);
            return Velocity.FromPerSecond((pos2 - pos1) * distance / Anime.TimeDelta);
        }

        public static Velocity CalcStartVelocity(this IAnimator animator, float distance)
        {
            var pos1 = animator.CalcPosition(0.0f, distance);
            var pos2 = animator.CalcPosition((float) Anime.TimeDelta, distance);
            return Velocity.FromPerSecond((pos2 - pos1) * distance / Anime.TimeDelta);
        }
    }
}
