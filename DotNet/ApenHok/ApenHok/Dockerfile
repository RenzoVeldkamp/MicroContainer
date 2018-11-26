FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ApenHok/ApenHok.csproj ApenHok/
COPY ApenHok.Communication/ApenHok.Communication.csproj ApenHok.Communication/
COPY ApenHok.Data/ApenHok.Data.csproj ApenHok.Data/
RUN dotnet restore ApenHok/ApenHok.csproj
COPY . .
WORKDIR /src/ApenHok
RUN dotnet build ApenHok.csproj -c Release -o /app

# FROM build as pack
# RUN dotnet pack /src/ApenHok.Communication/ApenHok.Communication.csproj -c Release
# COPY ./src/ApenHok.Communication/bin/Release/ApenHok.Communication.1.0.1.nupkg /app

FROM build AS publish
RUN dotnet publish ApenHok.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApenHok.dll"]
