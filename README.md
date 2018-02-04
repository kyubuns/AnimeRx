# AnimeRx
Unity + Rx Animation Library

# コンセプト

- 「aからbにn秒で移動する」も出来るし  
「aからbに速度vで移動する」で指定も出来る。  
（等加速度運動も対応予定。）

```csharp
Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Motion.Uniform(1f))
    .SubscribeToPosition(cube);
```

```csharp
var anime = new[]
{
    // easing
    Anime.Play(vector[0], vector[1], Easing.EaseOutCirc(TimeSpan.FromSeconds(2.0f))),
    Anime.Play(vector[1], vector[2], Easing.EaseOutCirc(TimeSpan.FromSeconds(2.0f))),
    Anime.Play(vector[2], vector[0], Easing.EaseOutCirc(TimeSpan.FromSeconds(2.0f))),

    // motion
    Anime.Play(vector[0], vector[1], Motion.Uniform(2.0f)),
    Anime.Play(vector[1], vector[2], Motion.Uniform(2.0f)),
    Anime.Play(vector[2], vector[0], Motion.Uniform(2.0f)),
};
Observable.Concat(anime).SubscribeToPosition(cube);
```

# Special Thanks

- Inspired by [fumobox/TweenRx](https://github.com/fumobox/TweenRx)
- [yKimisaki](https://github.com/yKimisaki)
