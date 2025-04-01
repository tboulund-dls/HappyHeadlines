FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["profanity-service/api/api.csproj", "profanity-service/api/"]
COPY ["profanity-service/service/service.csproj", "profanity-service/service/"]
COPY ["profanity-service/infrastrucure/infrastrucure.csproj", "profanity-service/infrastrucure/"]
RUN dotnet restore "profanity-service/api/api.csproj"

COPY profanity-service/api/ profanity-service/api/
COPY profanity-service/service/ profanity-service/service/
COPY profanity-service/infrastrucure/ profanity-service/infrastrucure/

WORKDIR "profanity-service/api"
RUN dotnet build "api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]
