#!/bin/bash

sock="/var/run/docker.sock:/var/run/docker.sock"
image_name="beer-partner-dev-env"
current_dir="$(pwd):/var/opt"
aws_files="$(echo ~/.aws/credentials):/root/.aws/credentials"

if [[ $(uname -s) == MINGW* ]]
then
    sock="/$sock"
    current_dir="/$current_dir"
    aws_files="/$aws_files"
fi

docker build -t $image_name - < automation/docker/dev-env.dockerfile && \

docker run -it --rm \
    -v $sock \
    -v $current_dir \
    -v $aws_files \
    --env DEV_ENV=true \
    --name $image_name \
    $image_name