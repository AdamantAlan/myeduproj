#!/bin/bash

set -e

echo "Building image..."

IMAGE_NAME="servicea"
CONTAINER_NAME="servicea"
CONTAINER_PORT=5000
HOST_PORT=501

docker build -f devops/Dockerfile -t $IMAGE_NAME .

docker stop $CONTAINER_NAME || true

docker rm $CONTAINER_NAME || true

echo "Running container..."

docker run -d \
  -p $HOST_PORT:$CONTAINER_PORT \
  --name $CONTAINER_NAME \
  -e SERVICE_B=http://host.docker.internal:502 \
  --restart unless-stopped \
  $IMAGE_NAME