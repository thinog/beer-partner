#!/bin/bash

if ! command -v aws &> /dev/null
then
    echo "AWS CLI is missing!"
    echo "https://aws.amazon.com/cli/"
    exit
fi

aws cloudformation delete-stack --stack-name stack-beer-partner

aws s3 rb s3://beer-partner-sam --force