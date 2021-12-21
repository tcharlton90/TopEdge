FROM openpriv/android-go-mobile:2021.03 AS TopEdgeBase

# Install Ebiten dependencies
RUN apt update && apt install -y \
                  libc6-dev \
                  libglu1-mesa-dev \
                  libgl1-mesa-dev \
                  libxcursor-dev \
                  libxi-dev \
                  libxinerama-dev \
                  libxrandr-dev \
                  libxxf86vm-dev \
                  libasound2-dev \
                  pkg-config

FROM TopEdgeBase AS EbitenImage
RUN go env -w GO111MODULE="on"
# Install WASM binding
RUN go get github.com/hajimehoshi/wasmserve@latest
# Install Android binding
RUN go get github.com/hajimehoshi/ebiten/v2/cmd/ebitenmobile@latest

FROM EbitenImage AS TopEdgeBuilder

RUN mkdir -p /home/topedge
WORKDIR /home/topedge

COPY . .

RUN go mod download
RUN gomobile init
RUN make

#CMD ['wasmserve', ''./path/to/yourgame']