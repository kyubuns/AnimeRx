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
            Observable.Concat(
                Sample1(),
                Sample2()
            ).Subscribe();
        }

        private IObservable<Unit> Sample1()
        {
            var anime = new[]
            {
                // easing
                Anime.Play(new Vector3(-5.0f, 0.0f, 0.0f), new Vector3(5.0f, 0.0f, 0.0f), Easing.EaseOutBack(TimeSpan.FromSeconds(2.0f))),
                Anime.Play(new Vector3(5.0f, 0.0f, 0.0f), new Vector3(5.0f, 3.0f, 0.0f), Easing.EaseOutBack(TimeSpan.FromSeconds(2.0f))),
                Anime.Play(new Vector3(5.0f, 3.0f, 0.0f), new Vector3(-5.0f, 0.0f, 0.0f), Easing.EaseOutBack(TimeSpan.FromSeconds(2.0f))),

                // motion
                Anime.Play(new Vector3(-5.0f, 0.0f, 0.0f), new Vector3(5.0f, 0.0f, 0.0f), Motion.Uniform(3.0f)),
                Anime.Play(new Vector3(5.0f, 0.0f, 0.0f), new Vector3(5.0f, 3.0f, 0.0f), Motion.Uniform(3.0f)),
                Anime.Play(new Vector3(5.0f, 3.0f, 0.0f), new Vector3(-5.0f, 0.0f, 0.0f), Motion.Uniform(3.0f)),
            };
            return Observable.Concat(anime).DoToLocalPosition(cube).AsUnitObservable();
        }

        private IObservable<Unit> Sample2()
        {
            var anime = new[]
            {
                Anime.Play(-5f, 5f, Motion.Uniform(1.0f)),
                Anime.Play(-5f, 5f, Motion.Uniform(5.0f)),
            };

            return Observable.CombineLatest(anime).DoToLocalPosition(cube).AsUnitObservable();
        }
    }
}
