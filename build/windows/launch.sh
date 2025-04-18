#!/bin/bash

func="$1"
projectName="MapZter"

case $func in
  "build")
    dotnet build "../src/$projectName.API/$projectName.API.csproj"
    ;;
  "run")
    dotnet run --project "../src/$projectName.API/$projectName.API.csproj"
    ;;
  *)
    echo 
    exit 1
    trace_out "Could not recognize <$func> function." "" "1" 
    ;;
esac