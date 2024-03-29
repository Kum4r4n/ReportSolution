#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Payroll/Host/Payroll.API/Payroll.API.csproj", "src/Services/Payroll/Host/Payroll.API/"]
COPY ["src/Services/Payroll/Infrastructure/Payroll.Infrastructure/Payroll.Infrastructure.csproj", "src/Services/Payroll/Infrastructure/Payroll.Infrastructure/"]
COPY ["src/Services/Payroll/Core/Payroll.Application/Payroll.Application.csproj", "src/Services/Payroll/Core/Payroll.Application/"]
COPY ["src/Services/Payroll/Core/Payroll.Domain/Payroll.Domain.csproj", "src/Services/Payroll/Core/Payroll.Domain/"]
RUN dotnet restore "./src/Services/Payroll/Host/Payroll.API/./Payroll.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Payroll/Host/Payroll.API"
RUN dotnet build "./Payroll.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Payroll.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payroll.API.dll"]