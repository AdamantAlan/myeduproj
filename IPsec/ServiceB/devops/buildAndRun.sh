#!/bin/bash

set -e

echo "Building image..."

IMAGE_NAME="serviceb"
CONTAINER_NAME="serviceb"
CONTAINER_PORT=5000
HOST_PORT=502

docker build -f devops/Dockerfile -t $IMAGE_NAME .

docker stop $CONTAINER_NAME || true

docker rm $CONTAINER_NAME || true

echo "Running container..."

docker run -d \
  -p $HOST_PORT:$CONTAINER_PORT \
  --name $CONTAINER_NAME \
  -e SERVICE_A=http://host.docker.internal:501 \
  --restart unless-stopped \
  $IMAGE_NAME