FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/OrdersApi/OrdersApi.csproj src/OrdersApi/
RUN dotnet restore src/OrdersApi/OrdersApi.csproj

COPY src/OrdersApi/ src/OrdersApi/
RUN dotnet publish src/OrdersApi/OrdersApi.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN adduser --disabled-password --gecos "" appuser
USER appuser

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "OrdersApi.dll"]
