#!/bin/bash

set -e

echo "Building image..."

IMAGE_NAME="serviceb"
DOCKERHUB_USERNAME="myagkowdima"
DOCKERHUB_REPO="serviceb"
TAG="latest"

FULL_IMAGE_NAME="$DOCKERHUB_USERNAME/$DOCKERHUB_REPO:$TAG"

docker build -f devops/Dockerfile -t $IMAGE_NAME .

echo "Tagging image..."
docker tag $IMAGE_NAME $FULL_IMAGE_NAME

echo "Pushing image to Docker Hub..."
docker push $FULL_IMAGE_NAME