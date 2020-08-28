#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS base
WORKDIR /app
EXPOSE 80
RUN  apt-get update \
  && apt-get install -y wget \
  && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["07.Jobs/Jobs/Jobs.csproj", "07.Jobs/Jobs/"]
RUN dotnet restore "07.Jobs/Jobs/Jobs.csproj"
COPY . .
WORKDIR "/src/07.Jobs/Jobs"
RUN dotnet build "Jobs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jobs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/app