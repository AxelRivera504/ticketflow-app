# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/CatalogApi/CatalogApi.csproj src/CatalogApi/
RUN dotnet restore src/CatalogApi/CatalogApi.csproj

COPY src/CatalogApi/ src/CatalogApi/
RUN dotnet publish src/CatalogApi/CatalogApi.csproj -c Release -o /app/publish --no-restore

# Etapa final (solo runtime, imagen liviana)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "CatalogApi.dll"]
