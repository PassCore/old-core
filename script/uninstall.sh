#!/bin/bash
#Passcore installer
#Author: Kevin
#Date: 20191026

echo "Passcore Core Uninstaller"
echo "by Kevin"

sleep 1

#Variable
path=/usr/share/passcore
bin=/usr/bin

#Remove passcore files
echo "-> Remove passcore"
sudo rm -fR $path
sudo rm -fR $bin/passcore
sudo rm -fR tmp

sleep 1

#Finished
echo "All the operations finished. Bye~"