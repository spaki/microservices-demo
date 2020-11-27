#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MSD.ZipCode.V2.API/MSD.ZipCode.V2.API.csproj", "MSD.ZipCode.V2.API/"]
RUN dotnet restore "MSD.ZipCode.V2.API/MSD.ZipCode.V2.API.csproj"
COPY . .
WORKDIR "/src/MSD.ZipCode.V2.API"
RUN dotnet build "MSD.ZipCode.V2.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSD.ZipCode.V2.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSD.ZipCode.V2.API.dll"]