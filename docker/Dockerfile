FROM debian:trixie-slim AS steamcmd-install
WORKDIR /steamcmd
RUN apt update && apt install -y tar curl lib32gcc-s1 lib32stdc++6
ADD https://steamcdn-a.akamaihd.net/client/installer/steamcmd_linux.tar.gz .
RUN tar xzvf steamcmd_linux.tar.gz
RUN rm steamcmd_linux.tar.gz
RUN ./steamcmd.sh +login anonymous +quit

FROM debian:trixie-slim AS steamcmd
WORKDIR /

ENV A2S_ADDRESS=0.0.0.0
ENV A2S_PORT=17777
ENV BIND_ADDRESS=0.0.0.0
ENV BIND_PORT=2001
ENV GAME_NAME='My Arma Reforger Server'
ENV GAME_SCENARIO_ID='{59AD59368755F41A}Missions/21_GM_Eden.conf'
ENV GAME_MAX_PLAYERS=64
ENV MAX_FPS=30

RUN apt update \
  && apt install -y --no-install-recommends --no-install-suggests lib32gcc-s1 lib32stdc++6 ca-certificates jq \
  && apt remove --purge -y \
  && apt clean autoclean  \
  && apt autoremove -y \
  && rm -rf /var/lib/apt/lists/
COPY --from=steamcmd-install /steamcmd /steamcmd
COPY entrypoint.sh /

STOPSIGNAL SIGINT
VOLUME [ "/reforger" ]
ENTRYPOINT [ "/entrypoint.sh" ]