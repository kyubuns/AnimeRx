using System;
using System.Collections;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace AnimeRx.Dev
{
    public class Development : MonoBehaviour
    {
        [SerializeField] private GameObject cube;
        [SerializeField] private AnimationCurve curve;

        public IEnumerator Start()
        {
            cube.transform.position = new Vector3(-5f, 0f, 0f);
            yield return new WaitForSeconds(0.5f);
            Sample1();
        }

        private void Sample1()
        {
            Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Motion.Uniform(4f))
                .StopRecording()
                .SubscribeToPosition(cube);
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
