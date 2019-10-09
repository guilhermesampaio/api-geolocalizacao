FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine3.9 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS unit-test
WORKDIR /unit-tests
COPY . .
RUN dotnet test

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS publish
WORKDIR /src
COPY /src .
WORKDIR /src/Geolocalization.Api
RUN dotnet publish -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish  /app .
EXPOSE 80   
ENTRYPOINT ["dotnet", "Geolocalization.Api.dll"]