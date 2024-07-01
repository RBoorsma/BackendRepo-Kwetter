FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["User_Profile/UserService/UserService.csproj", "User_Profile/UserService/"]
COPY ["User_Profile/UserService.Core/UserService.Core.csproj", "User_Profile/UserService.Core/"]
COPY ["Messaging/Messaging.csproj", "Messaging/"]
COPY ["User_Profile/UserService.DAL/UserService.DAL.csproj", "User_Profile/UserService.DAL/"]
RUN dotnet restore "User_Profile/UserService/UserService.csproj"
COPY . .
WORKDIR "/src/User_Profile/UserService"
RUN dotnet build "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]
