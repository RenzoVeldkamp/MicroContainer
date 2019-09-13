FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ApenHok/ApenHok.csproj", "ApenHok/"]
COPY ["ApenHok.Communication/ApenHok.Communication.csproj", "ApenHok.Communication/"]
COPY ["ApenHok.Data/ApenHok.Data.csproj", "ApenHok.Data/"]
RUN dotnet restore "ApenHok/ApenHok.csproj"
COPY . .
WORKDIR "/src/ApenHok"
RUN dotnet build "ApenHok.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ApenHok.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApenHok.dll"]