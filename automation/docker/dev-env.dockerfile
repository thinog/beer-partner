FROM bitnami/minideb:latest

# general
RUN apt-get update && \
    apt-get install curl gnupg unzip software-properties-common ca-certificates apt-transport-https -y && \
    apt-get update

# aws-cli
RUN apt-get install awscli -y

# aws-sam-cli
RUN cd /tmp && \
    curl -sSLO https://github.com/aws/aws-sam-cli/releases/latest/download/aws-sam-cli-linux-x86_64.zip && \
    unzip aws-sam-cli-linux-x86_64.zip -d sam-installation && \
    ./sam-installation/install && \
    rm -rf aws-sam-cli-linux-x86_64.zip ./sam-installation

# docker
RUN curl -fsSL https://download.docker.com/linux/debian/gpg | apt-key add - && \
    echo "deb [arch=amd64] https://download.docker.com/linux/debian buster stable" > /etc/apt/sources.list.d/docker.list && \
    apt-get update && \
    apt-cache policy docker-ce && \
    apt-get install docker-ce docker-ce-cli containerd.io docker-compose -y

# dotnet-sdk
RUN curl -sSL https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    echo "deb https://packages.microsoft.com/debian/10/prod buster main" > /etc/apt/sources.list.d/microsoft-packages.list && \
    apt-get update && \
    apt-get install dotnet-sdk-3.1 -y 
    # dotnet tool install -g amazon.lambda.tools

WORKDIR /var/opt