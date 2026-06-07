#include <windows.h>
#include <thread>
#include "bridge.h"

// Define global buffer
FrameBuffer g_sharedFrame = { std::vector<unsigned char>(1920*1080*4, 0), 1920, 1080 };

int main() {
    // 1. Create Win32 Window
    // 2. Start Network Thread (connects to Codespace)
    // 3. Start Decoder Thread (loops: network -> decode -> g_sharedFrame)
    // 4. Run Message Loop (calls PaintWindow on WM_PAINT)
    return 0;
}