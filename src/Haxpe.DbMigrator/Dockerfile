FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers

COPY . .
RUN dotnet restore ./src/Haxpe.DbMigrator/Haxpe.DbMigrator.csproj

# copy everything else and build app
WORKDIR /source/src/Haxpe.DbMigrator
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Haxpe.DbMigrator.dll"]