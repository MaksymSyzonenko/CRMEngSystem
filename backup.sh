#!/bin/bash

# Параметры подключения к базе данных
DB_HOST="roundhouse.proxy.rlwy.net"
DB_PORT="58461"
DB_NAME="railway"
DB_USER="postgres"
DB_PASSWORD="vtRsPZNZNDmfoybLhjpZmakScbTMCFMy"
BACKUP_DIR="$HOME/Downloads" # Путь к директории "Загрузки" в домашнем каталоге

# Имя файла резервной копии с текущей датой
BACKUP_FILE="${BACKUP_DIR}/backup_$(date +%Y%m%d%H%M%S).sql"

# Выполнение резервного копирования
PGPASSWORD=${DB_PASSWORD} pg_dump -h ${DB_HOST} -p ${DB_PORT} -U ${DB_USER} ${DB_NAME} > ${BACKUP_FILE}

# Опционально: Удаление старых резервных копий (оставить только последние 7 резервных копий)
find ${BACKUP_DIR} -type f -name "*.sql" -mtime +7 -exec rm {} \;
