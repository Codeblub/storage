#!/bin/bash
# Install Plank
sudo apt-get install -y plank
# Add plank to your fluxbox startup
feh --bg-scale /workspaces/storage/your-wallpaper.jpg
echo "plank &" >> ~/.fluxbox/startup
ulauncher &