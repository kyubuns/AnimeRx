using UnityEngine;

namespace AnimeRx
{
    public class UnscaledTimeScheduler : IScheduler
    {
        private float startTime;

        public void Start()
        {
            startTime = Time.unscaledTime;
        }

        public float Now
        {
            get { return Time.unscaledTime - startTime; }
        }
    }
}