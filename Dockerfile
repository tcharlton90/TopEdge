#FROM openpriv/android-go-mobile:2021.03 AS TopEdgeBase
FROM golang:stretch AS TopEdgeBase

RUN apt update && apt install -y default-jre \
                                 unzip

ENV SDK_URL="https://dl.google.com/android/repository/sdk-tools-linux-4333796.zip" \
    ANDROID_HOME="/usr/local/android-sdk" \
    ANDROID_SDK=$ANDROID_HOME \
    ANDROID_VERSION=29 \
    ANDROID_BUILD_TOOLS_VERSION=30.0.2

## Download Android SDK
RUN mkdir "$ANDROID_HOME" .android \
    && cd "$ANDROID_HOME" \
    && curl -o sdk.zip $SDK_URL \
    && unzip sdk.zip \
    && rm sdk.zip \
    && yes | $ANDROID_HOME/tools/bin/sdkmanager --licenses

## Install Android Build Tool and Libraries
RUN $ANDROID_HOME/tools/bin/sdkmanager --update
RUN $ANDROID_HOME/tools/bin/sdkmanager "build-tools;${ANDROID_BUILD_TOOLS_VERSION}" \
    "platforms;android-${ANDROID_VERSION}" \
    "platform-tools"

# Install NDK
ENV NDK_VER="21.0.6113669"
RUN $ANDROID_HOME/tools/bin/sdkmanager "ndk;$NDK_VER"
RUN ln -sf $ANDROID_HOME/ndk/$NDK_VER $ANDROID_HOME/ndk-bundle

RUN mkdir -p /home/topedge
WORKDIR /home/topedge

COPY . .

RUN go install golang.org/x/mobile/cmd/gomobile@latest
RUN go mod download
RUN gomobile init

RUN go install github.com/hajimehoshi/wasmserve@latest

RUN gomobile build -target android github.com/tcharlton90/TopEdge

CMD ["wasmserve", "."]