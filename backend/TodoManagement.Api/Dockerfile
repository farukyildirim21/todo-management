FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /workspace
COPY . .

WORKDIR /workspace/TodoManagement.Api
RUN DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0 dotnet restore
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoManagement.Api.dll"]
