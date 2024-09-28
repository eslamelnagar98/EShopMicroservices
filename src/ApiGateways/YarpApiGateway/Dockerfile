#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ApiGateways/YarpApiGateway/YarpApiGateway.csproj", "ApiGateways/YarpApiGateway/"]
RUN dotnet restore "./ApiGateways/YarpApiGateway/YarpApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateways/YarpApiGateway"
RUN dotnet build "./YarpApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN mkdir -p /app/pfx

RUN dotnet dev-certs https -ep /app/pfx/certificate.pfx -p crypticpassword

RUN chown -R app:app /app/pfx

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YarpApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

COPY --from=build /app/pfx /app/pfx
ENTRYPOINT ["dotnet", "YarpApiGateway.dll"]