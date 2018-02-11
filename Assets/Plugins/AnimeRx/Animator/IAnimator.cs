namespace AnimeRx
{
    public interface IAnimator
    {
        float CalcFinishTime(float distance);
        float CalcPosition(float time, float distance);
    }

    public static class AnimatorExtensions
    {
        private const float Delta = 0.000001f;

        public static Velocity CalcFinishVelocity(this IAnimator animator, float distance)
        {
            var finishTime = animator.CalcFinishTime(distance);
            var pos1 = animator.CalcPosition(finishTime - Delta, distance);
            var pos2 = animator.CalcPosition(finishTime, distance);
            return Velocity.FromPerSecond((pos2 - pos1) * distance / Delta);
        }

        public static Velocity CalcStartVelocity(this IAnimator animator, float distance)
        {
            var pos1 = animator.CalcPosition(0.0f, distance);
            var pos2 = animator.CalcPosition(Delta, distance);
            return Velocity.FromPerSecond((pos2 - pos1) * distance / Delta);
        }
    }
}
