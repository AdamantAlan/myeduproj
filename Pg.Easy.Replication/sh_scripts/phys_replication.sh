##############################Master
docker exec -it postgres1 bash

# В psql под своим суперпользователем контейнера
psql -U user1 -d db1 -c "CREATE ROLE repuser WITH REPLICATION LOGIN PASSWORD 'rep_pass';"

# Включаем нужные параметры через ALTER SYSTEM (чтобы не редактировать файлы руками)
psql -U user1 -d db1 -c "ALTER SYSTEM SET wal_level = replica;"
psql -U user1 -d db1 -c "ALTER SYSTEM SET max_wal_senders = 10;"
psql -U user1 -d db1 -c "ALTER SYSTEM SET max_replication_slots = 10;"

# Разрешим подключения для репликации
echo "host replication repuser 0.0.0.0/0 md5" >> /var/lib/postgresql/data/pg_hba.conf

# Перезапустим постгрес внутри контейнера
su - postgres -c "/usr/lib/postgresql/16/bin/pg_ctl -D /var/lib/postgresql/data -m fast -w restart"
docker restart postgres1


######################REPLICA

docker exec -it postgres2 bash
# Очищаем пустой/инициализированный ранее data-dir
rm -rf /var/lib/postgresql/data/*

# Берём базовую копию с мастера + сразу настраиваем standby и слот "replica2"
PGPASSWORD=rep_pass pg_basebackup \
  -h postgres1 -p 5432 -U repuser \
  -D /var/lib/postgresql/data \
  -Fp -Xs -P \
  -R -C -S replica2


su - postgres -c "/usr/lib/postgresql/16/bin/pg_ctl -D /var/lib/postgresql/data -w restart"
docker restart postgres2

###################CHECK

### Мастер
docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT pid, application_name, state, sync_state, sent_lsn, write_lsn, flush_lsn, replay_lsn FROM pg_stat_replication;"

### Реплика
docker exec -it postgres2 psql -U user1 -d postgres -c "SELECT pg_is_in_recovery();"
docker exec -it postgres3 psql -U user1 -d postgres -c "SELECT pg_is_in_recovery();"

# Создадим что-то на мастере
docker exec -it postgres1 psql -U user1 -d db1 -c "CREATE TABLE my_test_rep2(a int); INSERT INTO my_test_rep2 VALUES (1),(2);"

# Проверим на репликах (таблица появится, но подключаться надо к их БД)
docker exec -it postgres2 psql -U user1 -d db1 -c "\dt"
docker exec -it postgres3 psql -U user1 -d db1 -c "\dt"


docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT * FROM my_test_rep2"