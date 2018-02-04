using System;
using UniRx;
using UnityEngine;

namespace AnimeRx.Dev
{
    public class Development : MonoBehaviour
    {
        [SerializeField] private GameObject cube;

        public void Start()
        {
            var anime = new[]
            {
                // easing
                Anime.Play(new Vector3(-5.0f, 0.0f, 0.0f), new Vector3(5.0f, 0.0f, 0.0f), Easing.Linear(TimeSpan.FromSeconds(2.0f))),
                Anime.Play(new Vector3(5.0f, 0.0f, 0.0f), new Vector3(5.0f, 3.0f, 0.0f), Easing.Linear(TimeSpan.FromSeconds(2.0f))),
                Anime.Play(new Vector3(5.0f, 3.0f, 0.0f), new Vector3(-5.0f, 0.0f, 0.0f), Easing.Linear(TimeSpan.FromSeconds(2.0f))),

                // motion
                Anime.Play(new Vector3(-5.0f, 0.0f, 0.0f), new Vector3(5.0f, 0.0f, 0.0f), Motion.Uniform(1.0f)),
                Anime.Play(new Vector3(5.0f, 0.0f, 0.0f), new Vector3(5.0f, 3.0f, 0.0f), Motion.Uniform(1.0f)),
                Anime.Play(new Vector3(5.0f, 3.0f, 0.0f), new Vector3(-5.0f, 0.0f, 0.0f), Motion.Uniform(1.0f)),
            };
            Observable.Concat(anime).SubscribeToLocalPosition(cube);
        }
    }
}
