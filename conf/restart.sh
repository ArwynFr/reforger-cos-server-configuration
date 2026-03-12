#!/usr/bin/env bash

dotnet publish /home/arwyn/reforger-cos-server-configuration
ln -fs /home/arwyn/reforger-cos-server-configuration/conf/reforger.service /etc/systemd/system/reforger.service
systemctl daemon-reload
systemctl enable reforger
systemctl restart reforger

