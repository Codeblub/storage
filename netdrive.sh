#!/bin/bash
wget https://github.com/sigoden/dufs/releases/download/v0.43.0/dufs-v0.43.0-x86_64-unknown-linux-musl.tar.gz
tar -xvf dufs-*-x86_64-unknown-linux-musl.tar.gz
mv dufs ~/dufs
chmod +x ~/dufs
ln -s ~/
ln -s ~/dufs.log

./startdrive.sh