#!/bin/bash

set -e

IMAGE_NAME="myagkowdima/servicea"
CONTAINER_NAME="servicea"
CONTAINER_PORT=5000
HOST_PORT=501

echo "Stopping old container..."
docker stop $CONTAINER_NAME || true

echo "Removing old container..."
docker rm $CONTAINER_NAME || true

echo "Starting container..."
docker run -d \
  -p $HOST_PORT:$CONTAINER_PORT \
  --name $CONTAINER_NAME \
  -e SERVICE_B=http://XX.XX.XX.204:502 \
  --restart unless-stopped \
  $IMAGE_NAME

echo "Container started!"