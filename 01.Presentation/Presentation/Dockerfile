#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["01.Presentation/Presentation/Presentation.csproj", "01.Presentation/Presentation/"]
COPY ["06.Migrations/Migrations.Data/Migrations.Data.csproj", "06.Migrations/Migrations.Data/"]
COPY ["03.Persistence/Persistence/Persistence.csproj", "03.Persistence/Persistence/"]
COPY ["00.Domain/Domain/Domain.csproj", "00.Domain/Domain/"]
COPY ["04.Infrastructure/Infrastructure/Infrastructure.csproj", "04.Infrastructure/Infrastructure/"]
COPY ["04.Infrastructure/Infrastructure.Crosscutting/Infrastructure.Crosscutting.csproj", "04.Infrastructure/Infrastructure.Crosscutting/"]
COPY ["01.Presentation/Presentation.Validators/Presentation.Validators.csproj", "01.Presentation/Presentation.Validators/"]
COPY ["02.Application/Application/Application.csproj", "02.Application/Application/"]
COPY ["06.Migrations/Migrations.Security/Migrations.Security.csproj", "06.Migrations/Migrations.Security/"]
RUN dotnet restore "01.Presentation/Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/01.Presentation/Presentation"
RUN dotnet build "Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]