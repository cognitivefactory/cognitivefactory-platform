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

# Linux

## k3d install

/usr/local/bin/k3d : **16 Mo**

## k3d 3 nodes cluster

(base) laurent@YOGA720:~$ df
Filesystem     1K-blocks      Used Available Use% Mounted on
/dev/sdb       263174212  36261396 213474660  15% /
tmpfs            3190232    410040   2780192  13% /mnt/wsl
tools          472585212 394953524  77631688  84% /init
none             3187960         0   3187960   0% /dev
none             3190232        12   3190220   1% /run
none             3190232         0   3190232   0% /run/lock
none             3190232         0   3190232   0% /run/shm
none             3190232         0   3190232   0% /run/user
tmpfs            3190232         0   3190232   0% /sys/fs/cgroup
C:\            472585212 394953524  77631688  84% /mnt/c
D:\             26214396   2704924  23509472  11% /mnt/d
/dev/sdd       263174212   1589860 248146196   1% /mnt/wsl/docker-desktop-data/isocache
none             3190232        12   3190220   1% /mnt/wsl/docker-desktop/shared-sockets/host-services
/dev/sdc       263174212    134168 249601888   1% /mnt/wsl/docker-desktop/docker-desktop-proxy
/dev/loop0        404596    404596         0 100% /mnt/wsl/docker-desktop/cli-tools

Vmmem : 303 Mo

docker system df -v : empty

C:\Users\laure\AppData\Local\Docker\wsl\data : 1582 Mo

1 min 30

Vmmem : 2 Go

4+2 containers runnning
3 images : 45 + 172 + 26 Mo
many volumes created

(base) laurent@YOGA720:~$ df
Filesystem     1K-blocks      Used Available Use% Mounted on
/dev/sdb       263174212  36261428  213474628  15% /
tmpfs            3190232    410040   2780192  13% /mnt/wsl
tools          472585212 397323780  75261432  85% /init
none             3187960         0   3187960   0% /dev
none             3190232        12   3190220   1% /run
none             3190232         0   3190232   0% /run/lock
none             3190232         0   3190232   0% /run/shm
none             3190232         0   3190232   0% /run/user
tmpfs            3190232         0   3190232   0% /sys/fs/cgroup
C:\            472585212 397323780  75261432  85% /mnt/c
D:\             26214396   2704924  23509472  11% /mnt/d
=> /dev/sdd       263174212   2575308//1589860 247160748   2% /mnt/wsl/docker-desktop-data/isocache
none             3190232        12   3190220   1% /mnt/wsl/docker-desktop/shared-sockets/host-services
/dev/sdc       263174212    134168 249601888   1% /mnt/wsl/docker-desktop/docker-desktop-proxy
/dev/loop0        404596    404596         0 100% /mnt/wsl/docker-desktop/cli-tools

C:\Users\laure\AppData\Local\Docker\wsl\data : 2663 Mo
=> + 986 Mo

everything is in vhdx file
this is a separate distribution in wsl
you can access the files from windows with :
\\wsl$\docker-desktop-data\version-pack-data\community\docker

memory (Docker) : 1256 Mo
- registry : 11 Mo
- server-0 : 639 Mo
- agent-0 : 249 Mo
- agent-1 : 211 Mo
- agent-2 : 138 Mo
- serverlb : 8 Mo

memory (top) : 1539 Mo

Vmmem redescendu à 1209 Mo

### Helm

/dev/sdd       263174212  36262092 213473964  15% /
/dev/sdd       263174212  36306152 213429904  15% /

44 Mo


# Kubernetes

## YugabyteDB

### Docker images

Pulling the image takes a while : **612 Mo** 

### Memory

?? Vmmem : 12 Go after install ??