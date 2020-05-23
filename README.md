# AnimeRx - Rx Tween Animation Library for Unity

Created by kyubuns

- ```IObservable<T> Anime.Play(T from, T to, IAnimator animator)```

Task Version! -> [kyubuns/AnimeTask](https://github.com/kyubuns/AnimeTask)

## Installation

- Import UniRx ([AssetStore](https://assetstore.unity.com/packages/tools/unirx-reactive-extensions-for-unity-17276) or [GitHub](https://github.com/neuecc/UniRx))
- Import AnimeRx (https://github.com/kyubuns/AnimeRx/releases)

## Examples

### Basic

![sample1](https://user-images.githubusercontent.com/961165/35796308-7d0512aa-0a9f-11e8-9c66-c1dceeeafb72.gif)

(-5,0,0)から(5,0,0)へ、秒速4mで移動。

```csharp
Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Motion.Uniform(4f))
    .Subscribe(x => cube.transform.position = x);
    //.SubscribeToPosition(cube);
```

### Method Chain

![sample2](https://user-images.githubusercontent.com/961165/35796309-7d2bbdf6-0a9f-11e8-8fe1-acef944a36c0.gif)

(-5,0,0)から(5,0,0)へ移動した後、(0,3,0)に等速で移動。  
この方法を用いた場合、1つ目の移動と2つ目の移動で1フレームの間、値の変化が停止します。  
スムーズに移動させるには下記のPathの方法を使用してください。

```csharp
var animator = Motion.Uniform(5f);
Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), animator)
    .Play(new Vector3(0f, 3f, 0f), animator)
    .SubscribeToPosition(cube);
```

### Easing

![sample3](https://user-images.githubusercontent.com/961165/35796312-7d518662-0a9f-11e8-99e4-e5943212d966.gif)

EaseOutQuadで2秒かけて移動。

```csharp
Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Easing.OutQuad(2f))
    .SubscribeToPosition(cube);
```

### Sleep

![sample10](https://user-images.githubusercontent.com/961165/35796321-7e3ff25c-0a9f-11e8-956a-d85aa2a30e48.gif)

移動した後、1秒まって再度動き出す。

```csharp
Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 0f), Easing.OutExpo(2f))
    .Sleep(1f)
    .Play(new Vector3(5f, 0f, 0f), Easing.OutExpo(2f))
    .SubscribeToPosition(cube);
```

### Path

![sample4](https://user-images.githubusercontent.com/961165/35796313-7d772fc0-0a9f-11e8-91c5-4780b43a5b4f.gif)

指定したpositionに順番に移動。

```csharp
var positions = new[]
{
    new Vector3(-5f, 0f, 0f),
    new Vector3(0f, 3f, 0f),
    new Vector3(5f, 0f, 0f),
    new Vector3(0f, -3f, 0f),
    new Vector3(-5f, 0f, 0f),
};

Anime.Play(positions, Easing.InOutSine(6f))
    .SubscribeToPosition(cube);
```

### Combine

![sample5](https://user-images.githubusercontent.com/961165/35796315-7d9e024e-0a9f-11e8-908c-f35b4326ee42.gif)

x, y, zの各座標を別々にアニメーションさせて合成。

```csharp
var x = Anime.Play(-5f, 5f, Easing.InOutSine(3f));

var y = Anime.Play(0f, 3f, Easing.InOutSine(1.5f))
    .Play(0f, Easing.InOutSine(1.5f));

var z = Anime.Stay(0f);

Observable.CombineLatest(x, y, z)
    .SubscribeToPosition(cube);
```

### AnimationCurve

![sample11](https://user-images.githubusercontent.com/961165/35796322-7e6983e2-0a9f-11e8-807c-ff3a2967a2cf.gif)

UnityEngine.AnimationCurveを利用して移動。

```csharp
Anime.Play(new Vector3(-5f, 0f, 0f), new Vector3(5f, 0f, 0f), Motion.From(curve, 3f))
    .SubscribeToPosition(cube);
```

### Extensions

![sample6](https://user-images.githubusercontent.com/961165/35796317-7dc739de-0a9f-11e8-8aef-599e7e325efc.gif)

cube.transform.positionから(3,3,0)へ移動。

```csharp
cube.transform.position
    .Play(new Vector3(3f, 3f, 0f), Easing.OutBack(2f))
    .SubscribeToPosition(cube);
```

### Circle

![sample8](https://user-images.githubusercontent.com/961165/35796318-7dedcb62-0a9f-11e8-907c-e0ee65298b17.gif)

IObservble<float>を円運動に変換。

```csharp
Anime.Play(0f, Mathf.PI * 2f, Easing.OutCubic(3f))
    .Select(x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0.0f))
    .Select(x => x * 3f)
    .SubscribeToPosition(cube);
```

### Range / Lerp

![sample17](https://user-images.githubusercontent.com/961165/35954448-0deb1e8e-0ccd-11e8-92ba-1d952a90332e.gif)

途中まで一緒についていく。  
特定の範囲だけついていく。

```csharp
var flow = Anime.Play(Easing.InOutExpo(2.5f))
    .Stop(0.5f)
    .Play(1.0f, 0.0f, Easing.InOutExpo(2.5f));

flow
    .Range(0.0f, 0.5f)
    .Lerp(new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 0f))
    .SubscribeToPosition(cube2);

flow
    .Lerp(new Vector3(-5f, -1f, 0f), new Vector3(5f, -1f, 0f))
    .SubscribeToPosition(cube);
```

### PlayIn/PlayOut/PlayInOut

![sample24](https://user-images.githubusercontent.com/961165/36070628-70652970-0f42-11e8-980b-5d2b26d28847.gif)

Animationから等速運動に繋げる。

```csharp
Anime.PlayIn(-5f, 0f, 5f, Easing.InCubic(1.0f))
    .SubscribeToPositionX(cube);
```

### Delay

![sample18](https://user-images.githubusercontent.com/961165/36021790-3d5a335a-0dca-11e8-9481-7376cd25a4c0.gif)

Observable.Delay in UniRx

```csharp
var circle = Anime.Play(0f, Mathf.PI * 2f, Easing.OutCubic(3f))
    .Select(x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0.0f))
    .Select(x => x * 3f);

circle
    .SubscribeToPosition(cube);

circle
    .Delay(0.3f)
    .SubscribeToPosition(cube2);

circle
    .Delay(0.55f)
    .SubscribeToPosition(cube3);
```

### Blend

![sample20](https://user-images.githubusercontent.com/961165/36059327-d082cd18-0e79-11e8-9292-c8f035ba4e00.gif)

2つの移動を足し合わせる。

```csharp
var circle = Anime
    .Play(Mathf.PI, Mathf.PI * 2f * 3f, Easing.InOutSine(3f))
    .Select(x => new Vector3(Mathf.Sin(x), Mathf.Cos(x), 0f));

var straight = Anime
    .Play(-3f, 3f, Easing.InOutSine(3f))
    .Select(x => new Vector3(0f, x, 0f));

Observable.CombineLatest(circle, straight)
    .Sum()
    .SubscribeToPosition(cube);
```

### WhenAll

![sample9](https://user-images.githubusercontent.com/961165/35796319-7e1568d4-0a9f-11e8-8e08-28ff53093e8c.gif)

WhenAllを使ってアニメーションのタイミングを合わせる。

```csharp
var leftCube1 = Anime
    .Play(new Vector3(-5f, 0f, 0f), new Vector3(-0.5f, 0f, 0f), Easing.Linear(2.5f))
    .DoToPosition(cube);

var rightCube1 = Anime
    .Play(new Vector3(5f, 0f, 0f), new Vector3(0.5f, 0f, 0f), Easing.OutCubic(1f))
    .DoToPosition(cube2);

var leftCube2 = Anime
    .Play(new Vector3(-0.5f, 0f, 0f), new Vector3(-0.5f, 3f, 0f), Easing.OutCubic(1f))
    .DoToPosition(cube);

var rightCube2 = Anime
    .Play(new Vector3(0.5f, 0f, 0f), new Vector3(0.5f, 3f, 0f), Easing.OutCubic(1f))
    .DoToPosition(cube2);

Observable.WhenAll(leftCube1, rightCube1)
    .ContinueWith(Observable.WhenAll(leftCube2, rightCube2))
    .Subscribe();
```

## Requirements

- Unity 2017.1 or later.
- Support .net3.5 and .net4.6

## Special thanks

- Inspired by [fumobox/TweenRx](https://github.com/fumobox/TweenRx)
- [yKimisaki](https://github.com/yKimisaki)

## License

MIT License (see [LICENSE](LICENSE))

