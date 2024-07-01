FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RegisterOrchestration/RegisterOrchService/RegisterOrchService.csproj", "RegisterOrchestration/RegisterOrchService/"]
COPY ["Messaging/Messaging.csproj", "Messaging/"]
COPY ["RegisterOrchestration/RegisterOrchService.Core/RegisterOrchService.Core.csproj", "RegisterOrchestration/RegisterOrchService.Core/"]
RUN dotnet restore "RegisterOrchestration/RegisterOrchService/RegisterOrchService.csproj"
COPY . .
WORKDIR "/src/RegisterOrchestration/RegisterOrchService"
RUN dotnet build "RegisterOrchService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RegisterOrchService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegisterOrchService.dll"]
