#!/bin/bash
set -e

dotnet restore
dotnet publish IsPrime --configuration Release --output binaries
dotnet test IsPrimeTests --configuration Release
