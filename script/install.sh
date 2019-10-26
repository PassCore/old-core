#!/bin/bash
#Passcore installer
#Author: Kevin
#Date: 20191026

#Variable
$path=/usr/share/passcore
$bin=/usr/bin/

#Remove old things
rm -fR $path
rm -fR $bin/passcore

#Copy something
mkdir $path
cp ./core/* $path

#Build run script
echo "dotnet $path/core.dll" > $bin/passcore
chmod +x $bin/passcore
