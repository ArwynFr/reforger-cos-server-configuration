#!/bin/env bash

while true
do
  /srv/reforger/cos-1/ArmaReforgerServer \
    -config /srv/reforger/cos-1/config.json \
    -profile /srv/reforger/cos-1 -maxFPS 60 -loadsessionsave -nothrow
done