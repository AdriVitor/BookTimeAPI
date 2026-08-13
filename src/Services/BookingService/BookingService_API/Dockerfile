#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8082

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/BookingService/BookingService_API/BookingService_API.csproj", "src/Services/BookingService/BookingService_API/"]
COPY ["src/Libs/Http/Communication.Http.csproj", "src/Libs/Http/"]
COPY ["src/Libs/MessageBus/Communication.MessageBus.csproj", "src/Libs/MessageBus/"]
COPY ["src/Libs/Shared/Shared.csproj", "src/Libs/Shared/"]
COPY ["src/Services/BookingService/BookingService_Application/BookingService_Application.csproj", "src/Services/BookingService/BookingService_Application/"]
COPY ["src/Services/BookingService/BookingService_Domain/BookingService_Domain.csproj", "src/Services/BookingService/BookingService_Domain/"]
COPY ["src/Services/BookingService/BookingService_Infra/BookingService_Infraestructure.csproj", "src/Services/BookingService/BookingService_Infra/"]
RUN dotnet restore "./src/Services/BookingService/BookingService_API/BookingService_API.csproj"
COPY . .
WORKDIR "/src/src/Services/BookingService/BookingService_API"
RUN dotnet build "./BookingService_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookingService_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingService_API.dll"]