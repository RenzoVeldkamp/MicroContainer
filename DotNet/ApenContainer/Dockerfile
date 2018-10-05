FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ApenContainer.csproj", ""]
RUN dotnet restore "ApenContainer.csproj"
COPY . .
WORKDIR "src/"
RUN dotnet build "ApenContainer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ApenContainer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApenContainer.dll"]

# docker container run -d -p 5001:80 --name apen centricms/apendotnet:latest