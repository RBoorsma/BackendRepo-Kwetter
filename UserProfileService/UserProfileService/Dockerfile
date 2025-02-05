﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserProfileService/UserProfileService/UserProfileService.csproj", "UserProfileService/UserProfileService/"]
COPY ["UserProfileService/UserProfileService.Core/UserProfileService.Core.csproj", "UserProfileService/UserProfileService.Core/"]
COPY ["Messaging/Messaging.csproj", "Messaging/"]
COPY ["UserProfileService/UserProfileService.DAL/UserProfileService.DAL.csproj", "UserProfileService/UserProfileService.DAL/"]
RUN dotnet restore "UserProfileService/UserProfileService/UserProfileService.csproj"
COPY . .
WORKDIR "/src/UserProfileService/UserProfileService"
RUN dotnet build "UserProfileService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserProfileService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserProfileService.dll"]
