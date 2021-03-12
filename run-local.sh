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

docker-compose --file automation/docker/dynamodb-local.yaml up --detach

trap 'cd $current_dir; docker-compose --file automation/docker/dynamodb-local.yaml down' INT

aws dynamodb create-table --cli-input-json file://dynamodb-local-configs.json --endpoint-url http://localhost:8000 > /dev/null

cd automation/sam

$sam build && \
$sam local start-api --port 8080 --host localhost --warm-containers EAGER --docker-network dynamodb