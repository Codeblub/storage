for display

Xvfb :99 -screen 0 1920x1080x24 &

export DISPLAY=:99

xclock &



ffmpeg -f x11grab -video_size 1920x1080 -i :99 -preset ultrafast -tune zerolatency -vcodec libx264 -f mpegts udp://0.0.0.0:5555

## install

sudo apt-get update && sudo apt-get install -y xvfb ffmpeg x11-apps

