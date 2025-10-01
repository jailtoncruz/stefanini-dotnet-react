FROM node:22-alpine AS node-base

FROM node-base AS web-builder
WORKDIR /app

COPY StefaniniDotNetReactChallenge.Web/package*.json ./

RUN npm install && npm cache clean --force

COPY StefaniniDotNetReactChallenge.Web/ ./

RUN npm run build


FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS api-builder
WORKDIR /src

COPY *.sln .
COPY StefaniniDotNetReactChallenge.API/*.csproj ./StefaniniDotNetReactChallenge.API/
COPY StefaniniDotNetReactChallenge.API/ ./StefaniniDotNetReactChallenge.API/

RUN dotnet restore "StefaniniDotNetReactChallenge.API/StefaniniDotNetReactChallenge.API.csproj"

COPY . .
WORKDIR /src/StefaniniDotNetReactChallenge.API

RUN dotnet build "StefaniniDotNetReactChallenge.API.csproj" -c Release -o /app/build
RUN dotnet publish "StefaniniDotNetReactChallenge.API.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

RUN apk add --no-cache icu-libs curl
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=api-builder /app/publish .
COPY --from=web-builder /StefaniniDotNetReactChallenge.API/wwwroot ./wwwroot

RUN adduser --disabled-password --home /app --gecos '' dotnetuser && chown -R dotnetuser:dotnetuser /app
USER dotnetuser

EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=10s --start-period=30s --retries=5 \
    CMD curl -f http://localhost:8080/api/health || exit 1

ENTRYPOINT ["dotnet", "StefaniniDotNetReactChallenge.API.dll"]