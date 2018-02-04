using UnityEngine;

namespace AnimeRx
{
    public class TimeScheduler : IScheduler
    {
        private float startTime;

        public void Start()
        {
            startTime = Time.time;
        }

        public float Now
        {
            get { return Time.time - startTime; }
        }
    }
}