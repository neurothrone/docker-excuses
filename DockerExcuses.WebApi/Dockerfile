FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DockerExcuses.WebApi/DockerExcuses.WebApi.csproj", "DockerExcuses.WebApi/"]
RUN dotnet restore "DockerExcuses.WebApi/DockerExcuses.WebApi.csproj"
COPY . .
WORKDIR "/src/DockerExcuses.WebApi"
RUN dotnet build "./DockerExcuses.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DockerExcuses.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerExcuses.WebApi.dll"]
