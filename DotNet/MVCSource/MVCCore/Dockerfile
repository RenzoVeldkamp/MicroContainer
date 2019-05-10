FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["MVCCore/MVCCore.csproj", "MVCCore/"]
RUN dotnet restore "MVCCore/MVCCore.csproj"
COPY . .
WORKDIR "/src/MVCCore"
RUN dotnet build "MVCCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MVCCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MVCCore.dll"]