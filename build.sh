#!/bin/bash

mkdir -p license
cp Unity_v2020.x.ulf license/Unity_lic.ulf

docker run -v `pwd`/license:/root/.local/share/unity3d/Unity -it --rm unityci/editor:ubuntu-2020.1.1f1-android-0.3.0 unity-editor \
  -projectPath ./ \
  -quit \
  -batchmode \
  -nographics \
  -buildTarget Android \
  -customBuildTarget Android \
  -customBuildName TopEdge \
  -executeMethod BuildCommand.PerformBuild \
  -logFile /dev/stdout
