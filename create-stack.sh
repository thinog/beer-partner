#!/bin/bash

if ! command -v aws &> /dev/null
then
    echo "AWS CLI is missing!"
    echo "https://aws.amazon.com/cli/"
    exit
fi

aws cloudformation create-stack \
    --stack-name stack-beer-partner-iam \
    --template-body file://automation/cloudformation/01-iam.yaml \
    --parameters ParameterKey=KeyPairName,ParameterValue=TestKey

aws cloudformation create-stack \
    --stack-name stack-beer-partner-api-gateway \
    --template-body file://automation/cloudformation/02-api-gateway.yaml \
    --parameters ParameterKey=KeyPairName,ParameterValue=TestKey

aws cloudformation create-stack \
    --stack-name stack-beer-partner-cloudfront \
    --template-body file://automation/cloudformation/03-cloudfront.yaml \
    --parameters ParameterKey=KeyPairName,ParameterValue=TestKey

aws cloudformation create-stack \
    --stack-name stack-beer-partner-api-partner-create \
    --template-body file://automation/cloudformation/resources/01-api-partner-create.yaml \
    --parameters ParameterKey=KeyPairName,ParameterValue=TestKey

aws cloudformation create-stack \
    --stack-name stack-beer-partner-api-partner-get \
    --template-body file://automation/cloudformation/resources/02-api-partner-get.yaml \
    --parameters ParameterKey=KeyPairName,ParameterValue=TestKey

aws cloudformation create-stack \
    --stack-name stack-beer-partner-api-partner-search \
    --template-body file://automation/cloudformation/resources/03-api-partner-search.yaml \
    --parameters ParameterKey=KeyPairName,ParameterValue=TestKey

aws cloudformation create-stack \
    --stack-name stack-beer-partner-api-gateway-stages \
    --template-body file://automation/cloudformation/99-api-gateway-stages.yaml \
    --parameters ParameterKey=KeyPairName,ParameterValue=TestKey