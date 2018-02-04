using UnityEngine;

namespace AnimeRx
{
    public class TimeScheduler : IScheduler
    {
        public float Now
        {
            get { return Time.time; }
        }
    }
}