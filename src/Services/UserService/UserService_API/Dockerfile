#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/UserService/UserService_API/UserService_API.csproj", "src/Services/UserService/UserService_API/"]
COPY ["src/libs/Communication.Email/Communication.Email.csproj", "src/libs/Communication.Email/"]
COPY ["src/libs/MessageBus/Communication.MessageBus.csproj", "src/libs/MessageBugLib/"]
COPY ["src/Services/UserService/UserService_Application/UserService_Application.csproj", "src/Services/UserService/UserService_Application/"]
COPY ["src/Services/UserService/UserService_Domain/UserService_Domain.csproj", "src/Services/UserService/UserService_Domain/"]
COPY ["src/Services/UserService/UserService_Infraestructure/UserService_Infraestructure.csproj", "src/Services/UserService/UserService_Infraestructure/"]
RUN dotnet restore "./src/Services/UserService/UserService_API/UserService_API.csproj"
COPY . .
WORKDIR "/src/src/Services/UserService/UserService_API"
RUN dotnet build "./UserService_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserService_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService_API.dll"]