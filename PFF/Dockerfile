# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and projects
COPY ["PFF.slnx", "./"]
COPY ["PFF.Api/PFF.Api.csproj", "PFF.Api/"]
COPY ["PFF.Domain/PFF.Domain.csproj", "PFF.Domain/"]
COPY ["Tools/PFF.Tools.csproj", "Tools/"]

# Restore dependencies
RUN dotnet restore "PFF.slnx"

# Copy all source code
COPY . .

# Build and publish
RUN dotnet publish "PFF.Api/PFF.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Add non-root user for security
RUN useradd -m -u 1001 appuser

# Copy published app
COPY --from=build /app/publish .

# Set ownership to appuser
RUN chown -R appuser:appuser /app
USER appuser

EXPOSE 7050
ENV ASPNETCORE_URLS=http://+:7050
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "PFF.Api.dll"]
