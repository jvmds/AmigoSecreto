FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AmigoSecreto/AmigoSecreto.csproj", "AmigoSecreto/"]
RUN dotnet restore "AmigoSecreto/AmigoSecreto.csproj"
COPY . .
WORKDIR "/src/AmigoSecreto"
RUN dotnet build "AmigoSecreto.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AmigoSecreto.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmigoSecreto.dll"]
