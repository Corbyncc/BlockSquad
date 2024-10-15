# Use the latest version of Debian
FROM debian:latest

# Set environment variables to avoid prompts during package installation
ENV DEBIAN_FRONTEND=noninteractive

# Expose the necessary ports for Unturned server (27015 and 27016 for both TCP and UDP)
EXPOSE 27015/tcp
EXPOSE 27015/udp
EXPOSE 27016/tcp
EXPOSE 27016/udp

# Install dependencies for SteamCMD, wget, and unzip (for downloading and extracting files)
RUN apt-get update && apt-get install -y \
    wget \
    ca-certificates \
    lib32gcc-s1 \
    unzip \
    && apt-get clean

# Create a directory for SteamCMD and download it
RUN mkdir -p /steamcmd && \
    wget https://steamcdn-a.akamaihd.net/client/installer/steamcmd_linux.tar.gz -O /steamcmd/steamcmd.tar.gz && \
    tar -xvzf /steamcmd/steamcmd.tar.gz -C /steamcmd && \
    rm /steamcmd/steamcmd.tar.gz

# Set the working directory to the SteamCMD folder
WORKDIR /steamcmd

# Run SteamCMD to install the Unturned dedicated server
RUN ./steamcmd.sh +force_install_dir /unturned +login anonymous +app_update 1110390 validate +exit

# Create the /unturned/Modules directory
RUN mkdir -p /unturned/Modules

# Download and extract the zip file into /unturned/Modules
# Replace the example URL with the actual URL of the zip file you want to download
RUN wget -O /tmp/module.zip https://github.com/openmod/openmod/releases/download/3.8.5/OpenMod.Unturned.Module.zip && \
    unzip /tmp/module.zip -d /unturned/Modules && \
    rm /tmp/module.zip

# Make ExampleServer.sh executable
RUN chmod +x /unturned/ExampleServer.sh

# Set the working directory to /unturned
WORKDIR /unturned

# Run the Unturned ExampleServer.sh
CMD ["./ExampleServer.sh"]
