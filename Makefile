
.PHONY: build

build:
	go build -o bin/

clean:
	go mod tidy
	go fmt ./...

serve:
	go install github.com/hajimehoshi/wasmserve@latest
	wasmserve .
	# sudo apt update
	# sudo apt install -y \
    #               libc6-dev \
    #               libglu1-mesa-dev \
    #               libgl1-mesa-dev \
    #               libxcursor-dev \
    #               libxi-dev \
    #               libxinerama-dev \
    #               libxrandr-dev \
    #               libxxf86vm-dev \
    #               libasound2-dev \
    #               pkg-config