using System;
using System.Collections;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AnimeRx.Dev
{
    public class Development : MonoBehaviour
    {
        [SerializeField] private GameObject cube;
        [SerializeField] private GameObject cube2;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private Slider slider1;
        [SerializeField] private Slider slider2;

        public IEnumerator Start()
        {
            cube.transform.position = new Vector3(0f, 3f, 0f);
            cube2.transform.position = new Vector3(-5f, -0.75f, 0f);

            // cube.SetActive(false);
            // cube2.SetActive(false);

            slider1.gameObject.SetActive(false);
            slider2.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            Sample16();
            yield return null;
        }

        private void Sample1()
        {
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Motion.Uniform(4f))
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample2()
        {
            var animator = Motion.Uniform(5f);
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), animator)
                .Play(new Vector3(0f, 3f, 0f), animator)
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample3()
        {
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Easing.EaseOutQuad(TimeSpan.FromSeconds(2f)))
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample4()
        {
            var positions = new[]
            {
                new Vector3(-5f, 0f, 0f),
                new Vector3(0f, 3f, 0f),
                new Vector3(5f, 0f, 0f),
                new Vector3(0f, -3f, 0f),
                new Vector3(-5f, 0f, 0f),
            };

            Anime.Play(positions, Easing.EaseInOutSine(TimeSpan.FromSeconds(6f)))
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample5()
        {
            var x = Anime.Play(-5f, 5f, Easing.EaseInOutSine(TimeSpan.FromSeconds(3f)));

            var y = Anime.Play(0f, 3f, Easing.EaseInOutSine(TimeSpan.FromSeconds(1.5f)))
                .Play(0f, Easing.EaseInOutSine(TimeSpan.FromSeconds(1.5f)));

            var z = Anime.Stay(0f);

            Observable.CombineLatest(x, y, z)
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample6()
        {
            cube.transform.position
                .Play(new Vector3(3f, 3f, 0f), Easing.EaseOutBack(TimeSpan.FromSeconds(2f)))
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample7()
        {
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f),
                    Easing.EaseInOutSine(TimeSpan.FromSeconds(1f)))
                .Play(new Vector3(-5f, 0f, 0f), Easing.EaseInOutSine(TimeSpan.FromSeconds(1f)))
                .Repeat()
                .SubscribeToPosition(cube);
        }

        private void Sample8()
        {
            Anime.Play(0f, Mathf.PI * 2f, Easing.EaseOutCubic(TimeSpan.FromSeconds(3f)))
                .Select(x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0.0f))
                .Select(x => x * 3f)
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample9()
        {
            var leftCube1 = Anime
                .Play(new Vector3(-5f, 0f, 0f), new Vector3(-0.5f, 0f, 0f), Easing.Linear(TimeSpan.FromSeconds(2.5f)))
                .DoToPosition(cube);

            var rightCube1 = Anime
                .Play(new Vector3(5f, 0f, 0f), new Vector3(0.5f, 0f, 0f), Easing.EaseOutCubic(TimeSpan.FromSeconds(1f)))
                .DoToPosition(cube2);

            var leftCube2 = Anime
                .Play(new Vector3(-0.5f, 0f, 0f), new Vector3(-0.5f, 3f, 0f),
                    Easing.EaseOutCubic(TimeSpan.FromSeconds(1f)))
                .DoToPosition(cube);

            var rightCube2 = Anime
                .Play(new Vector3(0.5f, 0f, 0f), new Vector3(0.5f, 3f, 0f),
                    Easing.EaseOutCubic(TimeSpan.FromSeconds(1f)))
                .DoToPosition(cube2);

            Observable.WhenAll(leftCube1, rightCube1)
                .ContinueWith(Observable.WhenAll(leftCube2, rightCube2))
                .StopRecording()
                .Subscribe();
        }

        private void Sample10()
        {
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 0f), Easing.EaseOutExpo(TimeSpan.FromSeconds(2f)))
                .Wait(TimeSpan.FromSeconds(1f))
                .Play(new Vector3(5f, 0f, 0f), Easing.EaseOutExpo(TimeSpan.FromSeconds(2f)))
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample11()
        {
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Motion.From(curve, TimeSpan.FromSeconds(3f)))
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private IEnumerator Sample12()
        {
            var hp = new ReactiveProperty<int>(100);
            var gauge = new ReactiveProperty<float>(100.0f);

            // HPゲージは、実際の値に1.5秒かけて追いつく
            hp
                .Select(x => Anime.Play(gauge.Value, x, Easing.EaseOutSine(TimeSpan.FromSeconds(1.5))))
                .Switch()
                .Subscribe(x => gauge.Value = x);

            gauge.Subscribe(x =>
            {
                // HPゲージの長さにする
                Debug.LogFormat("hp: {0}", x);
            });

            yield return new WaitForSeconds(1.0f);

            Debug.Log("ダメージを受けてHPが30に！");
            hp.Value = 30;

            yield return new WaitForSeconds(1.0f);

            Debug.Log("回復してHPが80に！");
            hp.Value = 80;
        }

        private void Sample13()
        {
            var hp = new ReactiveProperty<float>(1.0f);
            var gauge = new ReactiveProperty<float>(1.0f);

            slider1.OnValueChangedAsObservable().Subscribe(x => hp.Value = x);

            hp
                .Select(x => Anime.Play(gauge.Value, x, Easing.EaseOutCubic(TimeSpan.FromSeconds(1.0))))
                .Switch()
                .Subscribe(x => gauge.Value = x);

            gauge.Subscribe(x => { slider2.value = x; });

            Anime.Wait(TimeSpan.FromSeconds(0.0f))
                .DoOnCompleted(() => slider1.value = 0.3f)
                .Wait(TimeSpan.FromSeconds(1.0f))
                .DoOnCompleted(() => slider1.value = 0.8f)
                .Wait(TimeSpan.FromSeconds(1.0f))
                .DoOnCompleted(() => slider1.value = 0.0f)
                .Wait(TimeSpan.FromSeconds(0.5f))
                .DoOnCompleted(() => slider1.value = 1.0f)
                .Subscribe();
        }

        private void Sample14()
        {
            Anime.PlayRelative(new Vector3(-5f, 0.75f, 0f), new Vector3(5f, 0f, 0f), Easing.EaseInCubic(Velocity.FromPerSecond(2f)))
                .PlayRelative(new Vector3(5f, 0f, 0f), Easing.EaseOutCubic(Velocity.FromPerSecond(2f)))
                .SubscribeToPosition(cube);

            Anime.PlayRelative(new Vector3(-5f, -0.75f, 0f), new Vector3(5f, 0f, 0f), Easing.EaseInCubic(Velocity.FromPerSecond(2f)))
                .PlayRelative(new Vector3(5f, 0f, 0f), Easing.EaseOutCubic(Velocity.FromPerSecond(2f)))
                .SubscribeToPosition(cube2);
        }

        private void Sample15()
        {
            var circle = Anime.Play(0f, Mathf.PI * 2f * 6f, Easing.Linear(TimeSpan.FromSeconds(6f)))
                .Select(x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0.0f));

            var radius = Anime.Play(3f, 0f, Easing.EaseInOutSine(TimeSpan.FromSeconds(3f)))
                .Play(3f, Easing.EaseInOutSine(TimeSpan.FromSeconds(3f)));

            Observable.CombineLatest(
                    circle,
                    radius,
                    Tuple.Create
                )
                .Select(x => x.Item1 * x.Item2)
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample16()
        {
            Anime.PlayRelative(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Easing.Linear(TimeSpan.FromSeconds(1f)))
                .PlayRelative(new Vector3(5f, 0f, 0f), Easing.Linear(TimeSpan.FromSeconds(1f)))
                .Do(x => Debug.LogFormat("cube1 {0} {1}", Time.time, x.x))
                .SubscribeToPosition(cube);

            Anime.PlayRelative(new Vector3(-5f, -1f, 0f), new Vector3(5f, 0f, 0f), Easing.Linear(TimeSpan.FromSeconds(1f)))
                .PlayRelative(new Vector3(5f, 0f, 0f), Easing.Linear(TimeSpan.FromSeconds(1f)))
                .Do(x => Debug.LogFormat("cube2 {0} {1}", Time.time, x.x))
                .SubscribeToPosition(cube2);

            Observable.Interval(TimeSpan.FromSeconds(5f))
                .Subscribe(_ => Sample16());
        }

        public void Update()
        {
            Debug.LogFormat("update {0} {1} {2}", Time.time, cube.transform.position.x, cube2.transform.position.x);
        }
    }

    public static class Util
    {
        public static IObservable<T> StopRecording<T>(this IObservable<T> source)
        {
            return source.DoOnCompleted(() =>
            {
                Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(x => EditorApplication.isPlaying = false);
            });
        }
    }
}
