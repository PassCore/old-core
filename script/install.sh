#!/bin/bash
#Passcore installer
#Author: Kevin
#Date: 20191026

echo "Passcore Core Installer"
echo "by Kevin"

sleep 1

#Variable
path=/usr/share/passcore
bin=/usr/bin

#Remove old passcore
echo "-> Remove old things"
sudo rm -fR $path
sudo rm -fR $bin/passcore
sudo rm -fR tmp

#Copy new passcore
echo "--> Copy something"
sudo mkdir $path
sudo cp ./core/* $path

#Build start script
echo "---> Build and copy script"
mkdir tmp
echo "dotnet $path/core.dll" > tmp/passcore
sudo cp tmp/passcore $bin/passcore
sudo chmod +x $bin/passcore

#Clean files
echo "----> Clean useless files"
rm -fr tmp

sleep 1

#Finished
echo "All the operations finished. Type 'passcore' to run."