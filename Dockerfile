# Установка базового образа
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Установка сборочного образа
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CRMEngSystem.csproj", "."]
RUN dotnet restore "./CRMEngSystem.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CRMEngSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикация приложения
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRMEngSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app

# Установка cron
RUN apt-get update && apt-get install -y cron

# Копирование скрипта резервного копирования в контейнер
COPY backup.sh /usr/local/bin/backup.sh

# Установка прав на выполнение скрипта
RUN chmod +x /usr/local/bin/backup.sh

# Добавление cron job
RUN echo "0 0 * * * /usr/local/bin/backup.sh" >> /etc/crontab

# Копирование опубликованных файлов
COPY --from=publish /app/publish .

# Запуск cron и приложения
CMD cron && dotnet CRMEngSystem.dll
