#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/ResourceService/ResourceService_API/ResourceService_API.csproj", "src/Services/ResourceService/ResourceService_API/"]
COPY ["src/libs/MessageBugLib/Communication.MessageBus.csproj", "src/libs/MessageBugLib/"]
COPY ["src/Services/ResourceService/ResourceService_Application/ResourceService_Application.csproj", "src/Services/ResourceService/ResourceService_Application/"]
COPY ["src/Services/ResourceService/ResourceService_Domain/ResourceService_Domain.csproj", "src/Services/ResourceService/ResourceService_Domain/"]
COPY ["src/Services/ResourceService/ResourceService_Infraestructure/ResourceService_Infraestructure.csproj", "src/Services/ResourceService/ResourceService_Infraestructure/"]
RUN dotnet restore "./src/Services/ResourceService/ResourceService_API/ResourceService_API.csproj"
COPY . .
WORKDIR "/src/src/Services/ResourceService/ResourceService_API"
RUN dotnet build "./ResourceService_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ResourceService_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResourceService_API.dll"]