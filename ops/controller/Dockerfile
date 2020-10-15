FROM ubuntu

RUN apt-get update -y && apt-get upgrade -y
RUN apt-get install wget iputils-ping telnet -y
RUN cd ~ && wget -c https://github.com/wal-g/wal-g/releases/download/v0.2.17/wal-g.linux-amd64.tar.gz -O - | tar -xz

CMD ["/bin/bash"]
