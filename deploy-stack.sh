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

if ! command -v aws &> /dev/null
then
    echo "AWS CLI is missing!"
    echo "https://aws.amazon.com/cli/"
    exit
fi

aws s3api create-bucket --bucket beer-partner-sam --acl private

cd automation/sam

$sam build && \
$sam deploy