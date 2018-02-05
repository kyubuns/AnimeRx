using System;
using UniRx;
using UnityEngine;

namespace AnimeRx.Dev
{
    public class Development : MonoBehaviour
    {
        [SerializeField] private GameObject cube;
        [SerializeField] private AnimationCurve curve;

        public void Start()
        {
            Sample5();
        }

        private void Sample5()
        {
            var vector = new[]
            {
                new Vector3(-5.0f, 0.0f, 0.0f),
                new Vector3(5.0f, 0.0f, 0.0f),
                new Vector3(5.0f, -3.0f, 0.0f),
                new Vector3(-5.0f, 0.0f, 0.0f),
            };

            Anime.Wait(TimeSpan.FromSeconds(1.0f), cube.transform.localPosition)
                .Play(vector, Motion.From(curve, TimeSpan.FromSeconds(4.0f)))
                .Play(new Vector3(-5f, 0f, 0f), Easing.Linear(TimeSpan.FromSeconds(2.0f)))
                .SubscribeToLocalPosition(cube);
        }

        private void Sample4()
        {
            var vector = new[]
            {
                new Vector3(-5.0f, 0.0f, 0.0f),
                new Vector3(5.0f, 0.0f, 0.0f),
                new Vector3(5.0f, -3.0f, 0.0f),
                new Vector3(-5.0f, 0.0f, 0.0f),
            };

            Anime.Play(vector, Easing.EaseInOutBack(TimeSpan.FromSeconds(3.0f)))
                .SubscribeToLocalPosition(cube);
        }

        private IObservable<Unit> Sample1()
        {
            var vector = new[]
            {
                new Vector3(-5.0f, 0.0f, 0.0f),
                new Vector3(5.0f, 0.0f, 0.0f),
                new Vector3(5.0f, 3.0f, 0.0f),
            };

            var anime = new[]
            {
                // easing
                Anime.Wait<Vector3>(TimeSpan.FromSeconds(1.0f)),
                Anime.Play(vector[0], vector[1], Easing.EaseOutCirc(TimeSpan.FromSeconds(2.0f))),
                Anime.Wait<Vector3>(TimeSpan.FromSeconds(1.0f)),
                Anime.Play(vector[1], vector[2], Easing.EaseOutCirc(TimeSpan.FromSeconds(2.0f))),
                Anime.Wait<Vector3>(TimeSpan.FromSeconds(1.0f)),
                Anime.Play(vector[2], vector[0], Easing.EaseOutCirc(TimeSpan.FromSeconds(2.0f))),

                // motion
                Anime.Play(vector[0], vector[1], Motion.Uniform(2.0f)),
                Anime.Play(vector[1], vector[2], Motion.Uniform(2.0f)),
                Anime.Play(vector[2], vector[0], Motion.Uniform(2.0f)),
            };
            return Observable.Concat(anime).DoToLocalPosition(cube).Do(x => Debug.Log(x)).AsUnitObservable();
        }

        private IObservable<Unit> Sample2()
        {
            var anime = new[]
            {
                Anime.Play(-5f, 5f, Motion.Uniform(1.0f)),
                Anime.Play(-5f, 5f, Motion.Uniform(5.0f)),
                Anime.Stay(3.0f),
            };

            return Observable.CombineLatest(anime).DoToLocalPosition(cube).Do(Debug.Log).AsUnitObservable();
        }
    }
}
