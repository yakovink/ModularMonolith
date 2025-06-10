#!/bin/bash

CONTAINERS="$(sudo docker ps -q)"
if [ -n "$CONTAINERS" ]; then
	echo ">>>found opened ports"
	sudo docker kill $(echo "$CONTAINERS")
fi