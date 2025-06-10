#!/bin/bash

set -e

MODULE_NAME="$1"
CONTEXT_NAME="${MODULE_NAME}DbContext"
PROJ_NAME="$MODULE_NAME.csproj"
cd ./src/Modules/$MODULE_NAME
dotnet-ef migrations add InitialCreate -o Data/Migrations -p $PROJ_NAME -s ../../Bootstraper/API -c $CONTEXT_NAME
cd ../../..