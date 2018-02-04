# AnimeRx
Unity + Rx Animation Library

# コンセプト

- 「aからbにn秒で移動する」ではなく  
「aからbに速度vで移動する」で指定する

```csharp
var anime = new[]
{
    // easing - aからbに2.0秒で移動する
    Anime.Play(new Vector3(-5.0f, 0.0f, 0.0f), new Vector3(5.0f, 0.0f, 0.0f), Easing.Linear(TimeSpan.FromSeconds(2.0f))),
    Anime.Play(new Vector3(5.0f, 0.0f, 0.0f), new Vector3(5.0f, 3.0f, 0.0f), Easing.Linear(TimeSpan.FromSeconds(2.0f))),
    Anime.Play(new Vector3(5.0f, 3.0f, 0.0f), new Vector3(-5.0f, 0.0f, 0.0f), Easing.Linear(TimeSpan.FromSeconds(2.0f))),

    // motion - aからbに秒速1で移動する
    Anime.Play(new Vector3(-5.0f, 0.0f, 0.0f), new Vector3(5.0f, 0.0f, 0.0f), Motion.Uniform(1.0f)),
    Anime.Play(new Vector3(5.0f, 0.0f, 0.0f), new Vector3(5.0f, 3.0f, 0.0f), Motion.Uniform(1.0f)),
    Anime.Play(new Vector3(5.0f, 3.0f, 0.0f), new Vector3(-5.0f, 0.0f, 0.0f), Motion.Uniform(1.0f)),
};
Observable.Concat(anime).SubscribeToLocalPosition(cube.transform);
```
