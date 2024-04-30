#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/CompetencyFramework/CompetencyFramework.API/CompetencyFramework.API.csproj", "Services/CompetencyFramework/CompetencyFramework.API/"]
COPY ["Services/CompetencyFramework/CompetencyFramework.Application/Ordering.Application.csproj", "Services/CompetencyFramework/CompetencyFramework.Application/"]
COPY ["Services/CompetencyFramework/CompetencyFramework.Domain/CompetencyFramework.Domain.csproj", "Services/CompetencyFramework/CompetencyFramework.Domain/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["Services/CompetencyFramework/CompetencyFramework.Infrastructure/CompetencyFramework.Infrastructure.csproj", "Services/CompetencyFramework/CompetencyFramework.Infrastructure/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/CompetencyFramework/CompetencyFramework.API/CompetencyFramework.API.csproj"
COPY . .
WORKDIR "/src/Services/CompetencyFramework/CompetencyFramework.API"
RUN dotnet build "CompetencyFramework.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompetencyFramework.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompetencyFramework.API.dll"]