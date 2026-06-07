#include "bridge.h"
#include <libavcodec/avcodec.h>
#include <libswscale/swscale.h>

void DecodeLoop(int socket) {
    // 1. Setup FFmpeg Decoder (Context, Parser, etc.)
    // 2. Loop: Read bytes from socket -> avcodec_send_packet
    // 3. avcodec_receive_frame -> get YUV frame
    // 4. Use sws_scale to convert YUV to RGB -> g_sharedFrame.pixels
    
    std::lock_guard<std::mutex> lock(g_sharedFrame.mtx);
    g_sharedFrame.newDataAvailable = true;
}