#!/bin/bash

if ! command -v aws &> /dev/null
then
    echo "AWS CLI is missing!"
    echo "https://aws.amazon.com/cli/"
    exit
fi

aws cloudformation delete-stack --stack-name stack-beer-partner-api-gateway-stages

aws cloudformation delete-stack --stack-name stack-beer-partner-api-partner-create

aws cloudformation delete-stack --stack-name stack-beer-partner-api-partner-get

aws cloudformation delete-stack --stack-name stack-beer-partner-api-partner-search

aws cloudformation delete-stack --stack-name stack-beer-partner-cloudfront

aws cloudformation delete-stack --stack-name stack-beer-partner-api-gateway

aws cloudformation delete-stack --stack-name stack-beer-partner-iam