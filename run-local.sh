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

#docker-compose up -d dynamodb

cd automation/sam

$sam build && \
$sam local start-api --port 8080 --host localhost --skip-pull-image