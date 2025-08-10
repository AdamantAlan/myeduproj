###################Master
docker exec -it postgres1 bash

# В psql под своим суперпользователем контейнера
psql -U user1 -d db1 -c "CREATE ROLE repuser WITH REPLICATION LOGIN PASSWORD 'rep_pass';"
psql -U user1 -d db1 -c "ALTER SYSTEM SET wal_level = logical;"
psql -U user1 -d db1 -c "ALTER SYSTEM SET max_replication_slots = 10;"
psql -U user1 -d db1 -c "ALTER SYSTEM SET max_wal_senders = 10;"
psql -U user1 -d db1 -c "GRANT USAGE ON SCHEMA public TO repuser;"
psql -U user1 -d db1 -c "GRANT SELECT ON ALL TABLES IN SCHEMA public TO repuser;"
psql -U user1 -d db1 -c "ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT ON TABLES TO repuser;"

# Разрешим подключения для репликации
echo "host replication repuser 0.0.0.0/0 md5" >> /var/lib/postgresql/data/pg_hba.conf

# Перезапуск postgres и контейнера
su - postgres -c "/usr/lib/postgresql/16/bin/pg_ctl -D /var/lib/postgresql/data -m fast -w restart"
docker restart postgres1

#Создание публикации
docker exec -it postgres1 psql -U user1 -d db1 -c "CREATE PUBLICATION pub_all FOR ALL TABLES;"

#######REPLICA
docker exec -it postgres2 psql -U user2 -d postgres -c "CREATE DATABASE db1;"
docker exec -it postgres2 psql -U user2 -d db1 -c "CREATE TABLE IF NOT EXISTS test_logical(id int primary key, note text);"
docker exec -it postgres3 psql -U user3 -d postgres -c "CREATE DATABASE db1;"
docker exec -it postgres3 psql -U user3 -d db1 -c "CREATE TABLE IF NOT EXISTS test_logical(id int primary key, note text);"

docker exec -it postgres2 psql -U user2 -d db1 -c " CREATE SUBSCRIPTION sub_from_pg1_pg2 CONNECTION 'host=postgres1 port=5432 dbname=db1 user=repuser password=rep_pass application_name=pg2_sub' PUBLICATION pub_all WITH (copy_data = true, create_slot = true, slot_name = sub_pg2); "
docker exec -it postgres3 psql -U user3 -d db1 -c " CREATE SUBSCRIPTION sub_from_pg1_pg3 CONNECTION 'host=postgres1 port=5432 dbname=db1 user=repuser password=rep_pass application_name=pg3_sub' PUBLICATION pub_all WITH (copy_data = true, create_slot = true, slot_name = sub_pg3); "

docker exec -it postgres2 psql -U user2 -d db1 -c "ALTER SUBSCRIPTION sub_from_pg1_pg2 REFRESH PUBLICATION WITH (copy_data = true);"
docker exec -it postgres3 psql -U user3 -d db1 -c "ALTER SUBSCRIPTION sub_from_pg1_pg3 REFRESH PUBLICATION WITH (copy_data = true);"


##########CHECK

# Логические слоты
docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT slot_name, plugin, active FROM pg_replication_slots;"

# Подписчики (через walsender видны в pg_stat_replication)
docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT application_name, state, sent_lsn, write_lsn, flush_lsn, replay_lsn FROM pg_stat_replication;"

docker exec -it postgres2 psql -U user2 -d db1 -c "SELECT * FROM pg_stat_subscription;"
docker exec -it postgres3 psql -U user3 -d db1 -c "SELECT * FROM pg_stat_subscription;"

# На паблишере: создаём/вставляем
docker exec -it postgres1 psql -U user1 -d db1 -c "CREATE TABLE IF NOT EXISTS test_logical(id int primary key, note text);"
docker exec -it postgres1 psql -U user1 -d db1 -c "INSERT INTO test_logical VALUES (3,'hello'),(4,'world');"

# На подписчиках: проверяем
docker exec -it postgres2 psql -U user2 -d db1 -c "SELECT * FROM test_logical;"
docker exec -it postgres3 psql -U user3 -d db1 -c "SELECT * FROM test_logical;"