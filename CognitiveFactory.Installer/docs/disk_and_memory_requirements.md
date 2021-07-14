# Windows

## Windows Subsystem for Linux

### Linux filesystem

C:\Users\laure\AppData\Local\Packages\CanonicalGroupLimited.Ubuntu18.04onWindows_79rhkp1fndgsc\LocalState

ext4.vhdx : **1.5 Go**

### Linux swap

C:\Users\laure\AppData\Local\Temp

swap.vhdx : **66 Mo** (only while VM launched)

### Vmmem process

START Docker

memory : **1.2 Go** (only while VM launched)

STOP Docker

wait exactly ONE minute : process stopped and memory freed

wsl --shutdown

immediate : process stopped and memory freed

## Docker Desktop

**Windows install**

C:\Program Files\Docker : **2.2 Go**

**Linux install**

C:\Users\laure\AppData\Local\Docker\wsl\distro

ext4.vhdx : **105 Mo**

**Linux data**

C:\Users\laure\AppData\Local\Docker\wsl\data

ext4.vhdx : **890 Mo**

# Kubernetes

## YugabyteDB

### Docker images

Pulling the image takes a while : **612 Mo** 

### Memory

?? Vmmem : 12 Go after install ??