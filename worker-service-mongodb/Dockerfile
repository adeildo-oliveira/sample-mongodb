#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["worker-service-mongodb/worker-service-mongodb.csproj", "worker-service-mongodb/"]
RUN dotnet restore "worker-service-mongodb/worker-service-mongodb.csproj"
COPY . .
WORKDIR "/src/worker-service-mongodb"
RUN dotnet build "worker-service-mongodb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "worker-service-mongodb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "worker-service-mongodb.dll"]