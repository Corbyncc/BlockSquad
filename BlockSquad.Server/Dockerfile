FROM debian:latest

ENV DEBIAN_FRONTEND=noninteractive

EXPOSE 27015/tcp
EXPOSE 27015/udp
EXPOSE 27016/tcp
EXPOSE 27016/udp

RUN apt-get update && apt-get install -y \
    wget \
    curl \
    ca-certificates \
    lib32gcc-s1 \
    unzip \
    jq \
    && apt-get clean

COPY . .

RUN chmod +x /pull-game.sh

RUN /bin/sh ./pull-game.sh

RUN mkdir -p /unturned/Servers/BlockSquad/OpenMod/plugins && \
    unzip /artifact.zip -d /unturned/Servers/BlockSquad/OpenMod/plugins && \
    rm /artifact.zip

RUN mkdir -p /steamcmd && \
    wget https://steamcdn-a.akamaihd.net/client/installer/steamcmd_linux.tar.gz -O /steamcmd/steamcmd.tar.gz && \
    tar -xvzf /steamcmd/steamcmd.tar.gz -C /steamcmd && \
    rm /steamcmd/steamcmd.tar.gz

WORKDIR /steamcmd

RUN ./steamcmd.sh +force_install_dir /unturned +login anonymous +app_update 1110390 validate +exit

RUN wget -O /tmp/module.zip https://github.com/openmod/openmod/releases/download/3.8.5/OpenMod.Unturned.Module.zip && \
    unzip /tmp/module.zip -d /unturned/Modules && \
    rm /tmp/module.zip

WORKDIR /unturned

CMD ["sh", "-c", "./ServerHelper.sh -GSLT $GSLT +InternetServer/BlockSquad"]
