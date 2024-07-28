#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WarlockMVC/WarlockMVC.csproj", "WarlockMVC/"]
COPY ["Warlock.DataAccess/Warlock.DataAccess.csproj", "Warlock.DataAccess/"]
COPY ["Warlock.Models/Warlock.Models.csproj", "Warlock.Models/"]
COPY ["Warlock.Utility/Warlock.Utility.csproj", "Warlock.Utility/"]
RUN dotnet restore "./WarlockMVC/WarlockMVC.csproj"
COPY . .
WORKDIR "/src/WarlockMVC"
RUN dotnet build "./WarlockMVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WarlockMVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarlockMVC.dll"]