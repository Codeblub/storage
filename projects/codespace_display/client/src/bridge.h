#pragma once
#include <mutex>
#include <vector>

struct FrameBuffer {
    std::vector<unsigned char> pixels; // Raw RGB data
    int width, height;
    std::mutex mtx;                    // Prevents thread conflicts
    bool newDataAvailable = false;
};

extern FrameBuffer g_sharedFrame;