# Use .Net Core 3.1 image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Add nuget repository
#COPY ./NuGet/DarkFactor.Common.Lib.*.nupkg ./NuGet/
#RUN dotnet nuget add source /app/NuGet --name DarkFactorLocal

# Flush all nuget repos
#RUN dotnet nuget locals all -c

# Copy files
COPY ./ ./

# Restore and build web
RUN dotnet restore BugReportServer.csproj
RUN dotnet publish BugReportServer.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BugReportServer.dll"]

EXPOSE 5200:80
