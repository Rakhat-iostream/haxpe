FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers

COPY . .
RUN dotnet restore ./src/Haxpe.HttpApi.Host/Haxpe.HttpApi.Host.csproj

# copy everything else and build app
WORKDIR /source/src/Haxpe.HttpApi.Host
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Haxpe.HttpApi.Host.dll"]