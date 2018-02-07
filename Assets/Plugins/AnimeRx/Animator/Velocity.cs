namespace AnimeRx
{
    public struct Velocity
    {
        private readonly double perSecond;

        private Velocity(double perSecond)
        {
            this.perSecond = perSecond;
        }

        public static Velocity FromPerSecond(double value)
        {
            return new Velocity(value);
        }

        public double PerSecond
        {
            get { return perSecond; }
        }
    }
}
