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

