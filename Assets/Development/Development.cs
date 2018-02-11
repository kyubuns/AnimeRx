using System;
using System.Collections;
using System.Linq;
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
        [SerializeField] private GameObject cube3;
        [SerializeField] private GameObject sphere;
        [SerializeField] private GameObject sphere2;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private Slider slider1;
        [SerializeField] private Slider slider2;

        public IEnumerator Start()
        {
            cube.transform.position = new Vector3(0f, 0f, 0f);
            cube2.transform.position = new Vector3(0f, 3f, 0f);
            cube3.transform.position = new Vector3(0f, 3f, 0f);

            // cube.SetActive(false);
            cube2.SetActive(false);
            cube3.SetActive(false);
            sphere.SetActive(false);
            sphere2.SetActive(false);

            slider1.gameObject.SetActive(false);
            slider2.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            Sample24();
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
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Easing.OutQuad(TimeSpan.FromSeconds(2f)))
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

            Anime.Play(positions, Easing.InOutSine(TimeSpan.FromSeconds(6f)))
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample5()
        {
            var x = Anime.Play(-5f, 5f, Easing.InOutSine(TimeSpan.FromSeconds(3f)));

            var y = Anime.Play(0f, 3f, Easing.InOutSine(TimeSpan.FromSeconds(1.5f)))
                .Play(0f, Easing.InOutSine(TimeSpan.FromSeconds(1.5f)));

            var z = Anime.Stay(0f);

            Observable.CombineLatest(x, y, z)
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample6()
        {
            cube.transform.position
                .Play(new Vector3(3f, 3f, 0f), Easing.OutBack(TimeSpan.FromSeconds(2f)))
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        private void Sample7()
        {
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f),
                    Easing.InOutSine(TimeSpan.FromSeconds(1f)))
                .Play(new Vector3(-5f, 0f, 0f), Easing.InOutSine(TimeSpan.FromSeconds(1f)))
                .Repeat()
                .SubscribeToPosition(cube);
        }

        private void Sample8()
        {
            Anime.Play(0f, Mathf.PI * 2f, Easing.OutCubic(TimeSpan.FromSeconds(3f)))
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
                .Play(new Vector3(5f, 0f, 0f), new Vector3(0.5f, 0f, 0f), Easing.OutCubic(TimeSpan.FromSeconds(1f)))
                .DoToPosition(cube2);

            var leftCube2 = Anime
                .Play(new Vector3(-0.5f, 0f, 0f), new Vector3(-0.5f, 3f, 0f),
                    Easing.OutCubic(TimeSpan.FromSeconds(1f)))
                .DoToPosition(cube);

            var rightCube2 = Anime
                .Play(new Vector3(0.5f, 0f, 0f), new Vector3(0.5f, 3f, 0f),
                    Easing.OutCubic(TimeSpan.FromSeconds(1f)))
                .DoToPosition(cube2);

            Observable.WhenAll(leftCube1, rightCube1)
                .ContinueWith(Observable.WhenAll(leftCube2, rightCube2))
                .StopRecording()
                .Subscribe();
        }

        private void Sample10()
        {
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 0f), Easing.OutExpo(TimeSpan.FromSeconds(2f)))
                .Wait(TimeSpan.FromSeconds(1f))
                .Play(new Vector3(5f, 0f, 0f), Easing.OutExpo(TimeSpan.FromSeconds(2f)))
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
                .Select(x => Anime.Play(gauge.Value, x, Easing.OutSine(TimeSpan.FromSeconds(1.5))))
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
                .Select(x => Anime.Play(gauge.Value, x, Easing.OutCubic(TimeSpan.FromSeconds(1.0))))
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
            Anime.PlayRelative(new Vector3(-5f, 0.75f, 0f), new Vector3(5f, 0f, 0f),
                    Easing.InCubic(Velocity.FromPerSecond(2f)))
                .PlayRelative(new Vector3(5f, 0f, 0f), Easing.OutCubic(Velocity.FromPerSecond(2f)))
                .SubscribeToPosition(cube);

            Anime.PlayRelative(new Vector3(-5f, -0.75f, 0f), new Vector3(5f, 0f, 0f),
                    Easing.InCubic(Velocity.FromPerSecond(2f)))
                .PlayRelative(new Vector3(5f, 0f, 0f), Easing.OutCubic(Velocity.FromPerSecond(2f)))
                .SubscribeToPosition(cube2);
        }

        private void Sample15()
        {
            var circle = Anime.Play(0f, Mathf.PI * 2f * 6f, Easing.Linear(TimeSpan.FromSeconds(6f)))
                .Select(x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0.0f));

            var radius = Anime.Play(3f, 0f, Easing.InOutSine(TimeSpan.FromSeconds(3f)))
                .Play(3f, Easing.InOutSine(TimeSpan.FromSeconds(3f)));

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
            Anime.PlayRelative(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f),
                    Easing.Linear(TimeSpan.FromSeconds(1f)))
                .PlayRelative(new Vector3(5f, 0f, 0f), Easing.Linear(TimeSpan.FromSeconds(1f)))
                .Do(x => Debug.LogFormat("cube1 {0} {1}", Time.time, x.x))
                .SubscribeToPosition(cube);

            Anime.PlayRelative(new Vector3(-5f, -1f, 0f), new Vector3(5f, 0f, 0f),
                    Easing.Linear(TimeSpan.FromSeconds(1f)))
                .PlayRelative(new Vector3(5f, 0f, 0f), Easing.Linear(TimeSpan.FromSeconds(1f)))
                .Do(x => Debug.LogFormat("cube2 {0} {1}", Time.time, x.x))
                .SubscribeToPosition(cube2);

            Observable.Interval(TimeSpan.FromSeconds(5f))
                .Subscribe(_ => Sample16());
        }

        private void Sample17()
        {
            var flow = Anime.Play(Easing.InOutExpo(TimeSpan.FromSeconds(2.5f)))
                .Wait(TimeSpan.FromSeconds(0.5f))
                .Play(1.0f, 0.0f, Easing.InOutExpo(TimeSpan.FromSeconds(2.5f)));

            flow
                .Lerp(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f))
                .SubscribeToPosition(cube);

            flow
                .Range(0.0f, 0.5f)
                .Lerp(new Vector3(-5f, -1f, 0f), new Vector3(0f, -1f, 0f))
                .SubscribeToPosition(cube2);
        }

        private void Sample18()
        {
            var circle = Anime
                .Play(0f, Mathf.PI * 2f, Easing.OutCubic(TimeSpan.FromSeconds(3f)))
                .Select(x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0.0f))
                .Select(x => x * 3f);

            circle
                .SubscribeToPosition(cube);

            circle
                .Delay(TimeSpan.FromSeconds(0.3f))
                .SubscribeToPosition(cube2);

            circle
                .Delay(TimeSpan.FromSeconds(0.55f))
                .SubscribeToPosition(cube3);
        }

        private void Sample19()
        {
            Anime.Play(new Vector3(0f, 0f, 0f), new Vector3(3f, 0f, 0f), new Sample19Animator())
                .PlayRelative(new Vector3(0f, 3f, 0f), Easing.Linear(TimeSpan.FromSeconds(1.0f)))
                .SubscribeToPosition(cube);
        }

        public class Sample19Animator : IAnimator
        {
            public float CalcFinishTime(float distance)
            {
                return 3.0f;
            }

            public float CalcPosition(float time, float distance)
            {
                return 0.0f;
            }
        }

        public void Sample20()
        {
            var circle = Anime
                .Play(Mathf.PI, Mathf.PI * 2f * 3f, Easing.InOutSine(TimeSpan.FromSeconds(3f)))
                .Select(x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0f));

            var straight = Anime
                .Play(-3f, 3f, Easing.InOutSine(TimeSpan.FromSeconds(3f)))
                .Select(x => new Vector3(0f, x, 0f));

            Observable.CombineLatest(circle, straight)
                .Select(x => x[0] + x[1])
                .StopRecording()
                .SubscribeToPosition(cube);
        }

        public void Sample21()
        {
            var x = Anime.Play(1.0f, 0.5f, Easing.InOutSine(TimeSpan.FromSeconds(1f)))
                .Play(1.0f, Easing.OutElastic(TimeSpan.FromSeconds(0.8f)));

            var y = Anime.Play(1.0f, 1.2f, Easing.InOutSine(TimeSpan.FromSeconds(1f)))
                .Play(1.0f, Easing.OutElastic(TimeSpan.FromSeconds(0.8f)));

            Observable.CombineLatest(x, y)
                .Select(s => new Vector3(s[0], s[1], 1f))
                .SubscribeToLocalScale(sphere);
        }

        public void Sample22()
        {
            Anime.Play(7.5f, 3f, Easing.OutElastic(TimeSpan.FromSeconds(1f)))
                .SubscribeToPositionY(sphere2);
        }

        public void Sample23()
        {
            /*
            Anime.Shake(new Vector3(3f, 3f, 0f), Shaker.Simple(TimeSpan.FromSeconds(1f)))ssss
                .SubscribeToPosition(sphere2);
            */
        }

        public void Sample24()
        {
            var from = -5f;
            var to = 0f;

            var distance = Mathf.Abs(from - to);
            var delta = 0.00001f;
            var animator = Easing.InCubic(TimeSpan.FromSeconds(1f));
            var finishTime = animator.CalcFinishTime(distance);
            var pos1 = animator.CalcPosition(finishTime - delta, distance);
            var pos2 = animator.CalcPosition(finishTime, distance);
            var velocity = Velocity.FromPerSecond((pos2 - pos1) * distance / delta);
            Debug.Log(pos1);
            Debug.Log(pos2);
            Debug.Log(velocity.PerSecond);

            Anime.Play(from, to, animator)
                .Play(5f, Easing.Linear(velocity))
                .Loop()
                .SubscribeToPositionX(cube);
        }

        public void Update()
        {
            // Debug.LogFormat("update {0} {1} {2}", Time.time, cube.transform.position.x, cube2.transform.position.x);
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

        public static IObservable<T> StopRecordingSoon<T>(this IObservable<T> source)
        {
            return source.DoOnCompleted(() =>
            {
                EditorApplication.isPlaying = false;
            });
        }
    }
}
