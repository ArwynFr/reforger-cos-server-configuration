#!/bin/env bash

jq "
  .game.password = \"${GAME_PASSWORD}\" |
  .game.passwordAdmin = \"${GAME_PASSWORD_ADMIN}\"
" /config.json > /reforger/config.json

/steamcmd/steamcmd.sh +force_install_dir /reforger +login anonymous +app_update 1874900 validate +quit
/reforger/ArmaReforgerServer -config /reforger/config.json -profile /reforger -maxFPS ${MAX_FPS} -loadsessionsave -nothrow
