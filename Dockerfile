FROM openpriv/android-go-mobile:2021.03

COPY . .

RUN go mod download
RUN gomobile init
RUN make