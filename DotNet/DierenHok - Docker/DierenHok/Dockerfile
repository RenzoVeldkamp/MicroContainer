FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DierenHok/DierenHok.csproj", "DierenHok/"]
COPY ["DierenHok.Data/DierenHok.Data.csproj", "DierenHok.Data/"]
COPY ["DierenHok.Communication/DierenHok.Communication.csproj", "DierenHok.Communication/"]
RUN dotnet restore "DierenHok/DierenHok.csproj"
COPY . .
WORKDIR "/src/DierenHok"
RUN dotnet build "DierenHok.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DierenHok.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DierenHok.dll"]
