using UnityEngine;

namespace AnimeRx
{
    public class UnscaledTimeScheduler : IScheduler
    {
        public float Now
        {
            get { return Time.unscaledTime; }
        }
    }
}