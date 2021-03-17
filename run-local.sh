#!/bin/bash

if command -v sam.cmd &> /dev/null
then
    sam="sam.cmd"
elif command -v sam &> /dev/null
then
    sam="sam"
else
    echo "SAM is missing!"
    echo "https://aws.amazon.com/serverless/sam/"
    exit
fi

current_dir=`pwd`

docker-compose --file automation/docker/mongodb-local.yaml up --detach

function terminate() {
    docker ps -a | awk '{ print $1,$2 }' | grep aws-sam-cli-emulation-image-dotnetcore3.1 | awk '{print $1 }' | xargs -I {} docker rm {} -f
    cd $current_dir; docker-compose --file automation/docker/mongodb-local.yaml down
}

trap terminate INT

cd automation/sam

$sam build && \
$sam local start-api --port 8080 --host localhost --warm-containers EAGER --docker-network mongodb