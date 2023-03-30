# Use .Net Core 3.1 image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy files
COPY ./ ./

# Copy nuget file
COPY ./NuGet_Docker.config ./NuGet.config

# Restore and build web
RUN dotnet restore BugReportServer.csproj
RUN dotnet publish BugReportServer.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BugReportServer.dll"]

EXPOSE 5200:80
