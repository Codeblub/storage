~/dufs /workspaces/storage -p 6666 --allow-all -a admin:1414@/:rw > ~/dufs.log 2>&1 &~






#for changing the file

nano ~/start-cloud-drive.sh


to kill server

fuser -k 6666/tcp





###installing stuff

wget https://github.com/sigoden/dufs/releases/download/v0.43.0/dufs-v0.43.0-x86_64-unknown-linux-musl.tar.gz

tar -xvf dufs-*-x86_64-unknown-linux-musl.tar.gz

mv dufs ~/dufs

chmod +x ~/dufs






ssh into is

gh codespace ssh -c fictional-eureka-x5qqvgxqwp4qcpv6j




cd to it


\\fictional-eureka-x5qqvgxqwp4qcpv6j-6666.app.github.dev@SSL\