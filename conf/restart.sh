#!/usr/bin/env bash

dotnet publish /home/arwyn/reforger-cos-server-configuration
ln -s /home/arwyn/reforger-cos-server-configuration/conf/reforger.service /etc/systemd/system/reforger.service
systemctl enable reforger
systemctl restart reforger