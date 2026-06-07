#include <iostream>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>

int main() {
    int server_fd = socket(AF_INET, SOCK_STREAM, 0);
    sockaddr_in address;
    address.sin_family = AF_INET;
    address.sin_addr.s_addr = INADDR_ANY;
    address.sin_port = htons(5555);

    bind(server_fd, (struct sockaddr *)&address, sizeof(address));
    listen(server_fd, 1);

    std::cout << "[Host] Waiting for Client..." << std::endl;
    int client_socket = accept(server_fd, nullptr, nullptr);
    
    std::cout << "[Host] Client Connected! Starting Stream..." << std::endl;
    // Here, you would start your FFmpeg pipe logic
    
    close(client_socket);
    return 0;
}