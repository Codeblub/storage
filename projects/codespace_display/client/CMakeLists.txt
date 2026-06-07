# Add to client/CMakeLists.txt
find_package(PkgConfig REQUIRED)
pkg_check_modules(FFMPEG REQUIRED libavcodec libavformat libavutil libswscale)

target_link_libraries(client PRIVATE ${FFMPEG_LIBRARIES} ws2_32)