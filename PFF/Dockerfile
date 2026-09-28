# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["PFF.Api/PFF.Api.csproj", "./"]
RUN dotnet restore "PFF.Api/PFF.Api.csproj"

COPY . .
RUN dotnet publish "PFF.Api/PFF.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false 


# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /

COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "PFF.Api/PFF.Api.dll"]