#!/bin/bash

set -e
run_cmd="dotnet OzonEdu.Merchandise.dll --no-build -v d"

>&2 echo "Run Merchandise DB migrations."
dotnet OzonEdu.Merchandise.Migrator.dll --no-build -v d
>&2 echo "Merchandise DB Migrations complete, starting app."
>&2 echo "Run Merchandise: $run_cmd"
exec $run_cmd