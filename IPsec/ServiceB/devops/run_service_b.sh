#!/bin/bash

set -e

IMAGE_NAME="myagkowdima/serviceb"
CONTAINER_NAME="serviceb"
CONTAINER_PORT=5000
HOST_PORT=502

echo "Stopping old container..."
docker stop $CONTAINER_NAME || true

echo "Removing old container..."
docker rm $CONTAINER_NAME || true

echo "Starting container..."
docker run -d \
  -p $HOST_PORT:$CONTAINER_PORT \
  --name $CONTAINER_NAME \
  -e SERVICE_A=http://XX.XX.XX.109:501 \
  --restart unless-stopped \
  $IMAGE_NAME

echo "Container started!"