# loading SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app

# copy csproj and restore the dependencies
COPY . .
RUN dotnet restore ./MSD.ZipCode.V1.API/*.csproj

# show message
RUN pwsh -Command Write-Host "Micro Service Demo - ZipCode V1: generating new docker image with alpine, light docker image - by PowerShell Core"

# Build app
COPY . .
RUN dotnet publish ./MSD.ZipCode.V1.API/*.csproj -c Release -o out

# Build image - based on runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "MSD.ZipCode.V1.API.dll"]