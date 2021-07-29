#!/bin/bash
# declare STRING variable
STRING="BUILDING ALL PROJECTS"
#print variable on a screen
echo $STRING

dotnet build ./Core/Core.csproj
dotnet build ./Infrastructure/Infrastructure.csproj
dotnet build ./API/API.csproj