#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Identity/Host/Identity.API/Identity.API.csproj", "src/Services/Identity/Host/Identity.API/"]
COPY ["src/Services/Identity/Core/Identity.Application/Identity.Application.csproj", "src/Services/Identity/Core/Identity.Application/"]
COPY ["src/BuildingBlocks/Common/Common.csproj", "src/BuildingBlocks/Common/"]
COPY ["src/Services/Identity/Core/Identity.Domain/Identity.Domain.csproj", "src/Services/Identity/Core/Identity.Domain/"]
COPY ["src/Services/Identity/Infrastructure/Identity.Infrastructure/Identity.Infrastructure.csproj", "src/Services/Identity/Infrastructure/Identity.Infrastructure/"]
RUN dotnet restore "./src/Services/Identity/Host/Identity.API/./Identity.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Host/Identity.API"
RUN dotnet build "./Identity.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]