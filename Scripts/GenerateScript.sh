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
sed -i '' 's/float.LerpUnclamped/Mathf.LerpUnclamped/g' Player/AnimeVector1.cs

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

cp Extension/GameObjectLocalPositionExtensions.cs Extension/GameObjectPositionExtensions.cs
sed -i '' 's/LocalPosition/Position/g' Extension/GameObjectPositionExtensions.cs

cp Extension/GameObjectLocalPositionExtensions.cs Extension/GameObjectLocalScaleExtensions.cs
sed -i '' 's/LocalPosition/LocalScale/g' Extension/GameObjectLocalScaleExtensions.cs

