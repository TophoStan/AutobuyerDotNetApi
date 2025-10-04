FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AutoBuy.WebApi/AutoBuy.WebApi.csproj", "AutoBuy.WebApi/"]
COPY ["AutobuyerPlayer/AutobuyerPlayer.csproj", "AutobuyerPlayer/"]
COPY ["Environment/Environment.csproj", "Environment/"]
COPY ["AutobuyerPlayer.Contracts/AutobuyerPlayer.Contracts.csproj", "AutobuyerPlayer.Contracts/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Data.Contracts/Data.Contracts.csproj", "Data.Contracts/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "AutoBuy.WebApi/AutoBuy.WebApi.csproj"
COPY . .
WORKDIR "/src/AutoBuy.WebApi"
RUN dotnet build "./AutoBuy.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AutoBuy.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoBuy.WebApi.dll"]
