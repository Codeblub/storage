#include <windows.h>
#include "bridge.h"

void PaintWindow(HWND hWnd) {
    if (!g_sharedFrame.newDataAvailable) return;

    std::lock_guard<std::mutex> lock(g_sharedFrame.mtx);
    HDC hdc = GetDC(hWnd);
    
    BITMAPINFO bmi = {};
    bmi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
    bmi.bmiHeader.biWidth = 1920;
    bmi.bmiHeader.biHeight = -1080;
    bmi.bmiHeader.biPlanes = 1;
    bmi.bmiHeader.biBitCount = 32;

    StretchDIBits(hdc, 0, 0, 1920, 1080, 0, 0, 1920, 1080, 
                  g_sharedFrame.pixels.data(), &bmi, DIB_RGB_COLORS, SRCCOPY);
    
    ReleaseDC(hWnd, hdc);
    g_sharedFrame.newDataAvailable = false;
}