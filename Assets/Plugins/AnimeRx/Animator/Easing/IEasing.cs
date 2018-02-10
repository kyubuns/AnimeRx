namespace AnimeRx
{
    public static partial class Easing
    {
        private interface IEasing
        {
            float Function(float v);
        }
    }
}
