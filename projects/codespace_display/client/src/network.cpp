#include <winsock2.h>
#include <ws2tcpip.h>
#include <iostream>

#pragma comment(lib, "ws2_32.lib")

bool ConnectToHost(const char* ip_address) {
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);

    SOCKET sock = socket(AF_INET, SOCK_STREAM, 0);
    sockaddr_in server;
    server.sin_family = AF_INET;
    server.sin_port = htons(5555);
    inet_pton(AF_INET, ip_address, &server.sin_addr);

    if (connect(sock, (struct sockaddr*)&server, sizeof(server)) < 0) {
        std::cerr << "[Client] Connection Failed!" << std::endl;
        return false;
    }

    std::cout << "[Client] Connected to Host!" << std::endl;
    return true;
}