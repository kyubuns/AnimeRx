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
            const float delta = 0.000001f;

            var finishTime = animator.CalcFinishTime(distance);
            var pos1 = animator.CalcPosition(finishTime - delta, distance);
            var pos2 = animator.CalcPosition(finishTime, distance);
            return Velocity.FromPerSecond((pos2 - pos1) * distance / delta);
        }
    }
}
