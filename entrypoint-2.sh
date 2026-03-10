#!/bin/env bash

while true
do
  /srv/reforger/cos-2/ArmaReforgerServer \
    -config /srv/reforger/cos-2/config.json \
    -profile /srv/reforger/cos-2 -maxFPS 60 -loadsessionsave -nothrow
done