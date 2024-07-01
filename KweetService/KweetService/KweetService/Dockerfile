FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KweetService/KweetService/KweetService/KweetService.csproj", "KweetService/KweetService/KweetService/"]
COPY ["KweetService/KweetService.Core/KweetService.Core.csproj", "KweetService/KweetService.Core/"]
COPY ["Messaging/Messaging.csproj", "Messaging/"]
COPY ["KweetService/KweetService.DAL/KweetService.DAL.csproj", "KweetService/KweetService.DAL/"]
RUN dotnet restore "KweetService/KweetService/KweetService/KweetService.csproj"
COPY . .
WORKDIR "/src/KweetService/KweetService/KweetService"
RUN dotnet build "KweetService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KweetService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KweetService.dll"]
