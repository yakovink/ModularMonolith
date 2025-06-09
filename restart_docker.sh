#!/bin/bash

set -e

DOCKER_COMPOSE_FILE="./docker-compose.yml"
PORTS=(3541 3542 9092 9091 9090 6379 5000)

echo ">>>Compose Down Docker"
docker compose -f "$DOCKER_COMPOSE_FILE" -p modularmonolith down --remove-orphans
echo ">>>Kill open pids"
CONTAINERS="$(docker ps -q)"
if [ -n "$CONTAINERS" ]; then
	sudo docker kill "$CONTAINERS"
fi
echo ">>>Starting Docker Compose Up"
docker compose -f "$DOCKER_COMPOSE_FILE" up -d --build
echo ">>>Done."

