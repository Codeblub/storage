#!/bin/bash
# Install everything
sudo apt-get update
sudo apt-get install -y plank ulauncher feh

# Add to fluxbox startup
echo "feh --bg-scale /workspaces/storage/your-wallpaper.jpg &" >> ~/.fluxbox/startup
echo "plank &" >> ~/.fluxbox/startup
echo "ulauncher --hide-window &" >> ~/.fluxbox/startup