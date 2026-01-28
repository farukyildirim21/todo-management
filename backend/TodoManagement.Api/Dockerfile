# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Solution ve projeleri kopyala
COPY backend/*.sln ./
COPY backend/TodoManagement.Domain/*.csproj TodoManagement.Domain/
COPY backend/TodoManagement.Application/*.csproj TodoManagement.Application/
COPY backend/TodoManagement.Infrastructure/*.csproj TodoManagement.Infrastructure/
COPY backend/TodoManagement.Api/*.csproj TodoManagement.Api/

# NuGet restore
RUN dotnet restore TodoManagement.Api/TodoManagement.Api.csproj

# Tüm source kodu kopyala
COPY backend/. .

# Publish
WORKDIR /src/TodoManagement.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TodoManagement.Api.dll"]
