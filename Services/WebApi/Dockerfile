# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
USER 0

# Expose port
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
USER 0
WORKDIR /src

# Copy the rest of the application
COPY . .

# Restore dependencies
RUN dotnet restore "./Services/WebApi/WebApi.csproj"

# Run unit tests
WORKDIR "/src/Tests/WebApiTest/"
RUN dotnet test --configuration Release --verbosity normal

WORKDIR "/src/Services/WebApi/"
RUN dotnet build "WebApi.csproj" --configuration Release --output /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" --configuration Release --output /app/publish /p:UseAppHost=false

FROM runtime AS final
WORKDIR /app
COPY ["Services/WebApi/appsettings.*", "."]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
USER 1001