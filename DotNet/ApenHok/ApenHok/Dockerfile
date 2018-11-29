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

FROM build AS publish
RUN dotnet publish ApenHok.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
VOLUME ["/apenvolume"]
ENTRYPOINT ["dotnet", "ApenHok.dll"]
