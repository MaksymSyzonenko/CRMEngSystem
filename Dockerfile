# ��������� �������� ������
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# ��������� ���������� ������
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CRMEngSystem.csproj", "."]
RUN dotnet restore "./CRMEngSystem.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CRMEngSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ���������� ����������
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRMEngSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ��������� �����
FROM base AS final
WORKDIR /app
