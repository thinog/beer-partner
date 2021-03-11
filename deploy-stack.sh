#!/bin/bash

if ! command -v dotnet &> /dev/null
then
    echo "AWS CLI is missing!"
    echo "https://aws.amazon.com/cli/"
    exit
fi

dotnet lambda deploy-function --project-location ./src/Lambda/Partner/BeerPartner.Lambda.Partner.Create

dotnet lambda deploy-function --project-location ./src/Lambda/Partner/BeerPartner.Lambda.Partner.Get

dotnet lambda deploy-function --project-location ./src/Lambda/Partner/BeerPartner.Lambda.Partner.Search