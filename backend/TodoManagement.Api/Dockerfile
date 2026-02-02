FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /workspace
COPY . .

WORKDIR /workspace/TodoManagement.Api

#SSL validation bypass (LOCAL DEBUG ONLY)
ENV DOTNET_SYSTEM_NET_HTTP_ALLOWINSECURECERTIFICATES=true
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

RUN dotnet restore 
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoManagement.Api.dll"]
