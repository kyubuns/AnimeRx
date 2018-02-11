#!/bin/sh
set -ex

cd `dirname ${0}`
cd ../
cd Assets/Plugins/AnimeRx/

# Player
cp Player/AnimeVector4.cs Player/AnimeVector3.cs
sed -i '' 's/Vector4/Vector3/g' Player/AnimeVector3.cs

cp Player/AnimeVector4.cs Player/AnimeVector2.cs
sed -i '' 's/Vector4/Vector2/g' Player/AnimeVector2.cs

cp Player/AnimeVector4.cs Player/AnimeVector1.cs
sed -i '' 's/Vector4/float/g' Player/AnimeVector1.cs
sed -i '' 's/float.Distance(from, to)/Mathf.Abs(from - to)/g' Player/AnimeVector1.cs
sed -i '' 's/float.Distance(path\[i\], path\[i + 1\])/Mathf.Abs(path\[i\] - path\[i + 1\])/g' Player/AnimeVector1.cs
sed -i '' 's/float.Distance(inEnd, from)/Mathf.Abs(inEnd - from)/g' Player/AnimeVector1.cs
sed -i '' 's/float.Distance(to, outStart)/Mathf.Abs(to - outStart)/g' Player/AnimeVector1.cs
sed -i '' 's/float.Distance(outStart, inEnd)/Mathf.Abs(outStart - inEnd)/g' Player/AnimeVector1.cs
sed -i '' 's/float.Lerp/Mathf.Lerp/g' Player/AnimeVector1.cs

cp Player/AnimeVector4.cs Player/AnimeColor.cs
sed -i '' 's/Vector4/Color/g' Player/AnimeColor.cs
sed -i '' 's/Color.Distance/Vector4.Distance/g' Player/AnimeColor.cs

# Extensions
cp Extension/TransformLocalPositionExtensions.cs Extension/TransformPositionExtensions.cs
sed -i '' 's/LocalPosition/Position/g' Extension/TransformPositionExtensions.cs
sed -i '' 's/localPosition/position/g' Extension/TransformPositionExtensions.cs

cp Extension/TransformLocalPositionExtensions.cs Extension/TransformLocalScaleExtensions.cs
sed -i '' 's/LocalPosition/LocalScale/g' Extension/TransformLocalScaleExtensions.cs
sed -i '' 's/localPosition/localScale/g' Extension/TransformLocalScaleExtensions.cs

cp Extension/TransformLocalPositionExtensions.cs Extension/TransformLocalRotationExtensions.cs
sed -i '' 's/LocalPosition/LocalRotation/g' Extension/TransformLocalRotationExtensions.cs
sed -i '' 's/localPosition/localEulerAngles/g' Extension/TransformLocalRotationExtensions.cs

cp Extension/TransformLocalPositionExtensions.cs Extension/TransformRotationExtensions.cs
sed -i '' 's/LocalPosition/Rotation/g' Extension/TransformRotationExtensions.cs
sed -i '' 's/localPosition/eulerAngles/g' Extension/TransformRotationExtensions.cs

cp Extension/GameObjectLocalPositionExtensions.cs Extension/GameObjectPositionExtensions.cs
sed -i '' 's/LocalPosition/Position/g' Extension/GameObjectPositionExtensions.cs

cp Extension/GameObjectLocalPositionExtensions.cs Extension/GameObjectLocalScaleExtensions.cs
sed -i '' 's/LocalPosition/LocalScale/g' Extension/GameObjectLocalScaleExtensions.cs

cp Extension/GameObjectLocalPositionExtensions.cs Extension/GameObjectLocalRotationExtensions.cs
sed -i '' 's/LocalPosition/LocalRotation/g' Extension/GameObjectLocalRotationExtensions.cs

cp Extension/GameObjectLocalPositionExtensions.cs Extension/GameObjectRotationExtensions.cs
sed -i '' 's/LocalPosition/Rotation/g' Extension/GameObjectRotationExtensions.cs

cp Extension/Vector4Extensions.cs Extension/Vector3Extensions.cs
sed -i '' 's/Vector4/Vector3/g' Extension/Vector3Extensions.cs

cp Extension/Vector4Extensions.cs Extension/Vector2Extensions.cs
sed -i '' 's/Vector4/Vector2/g' Extension/Vector2Extensions.cs

cp Extension/Vector4Extensions.cs Extension/Vector1Extensions.cs
sed -i '' 's/Vector4/float/g' Extension/Vector1Extensions.cs
sed -i '' 's/floatExtensions/Vector1Extensions/g' Extension/Vector1Extensions.cs
