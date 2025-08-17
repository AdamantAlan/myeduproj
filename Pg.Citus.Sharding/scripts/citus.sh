docker exec -it postgres1 psql -U user1 -d db1 -c "CREATE EXTENSION IF NOT EXISTS citus;"
docker exec -it postgres2 psql -U user1 -d db1 -c "CREATE EXTENSION IF NOT EXISTS citus;"
docker exec -it postgres3 psql -U user1 -d db1 -c "CREATE EXTENSION IF NOT EXISTS citus;"

docker exec -it postgres1 psql -U user1 -d db1 -c "INSERT INTO pg_dist_authinfo(nodeid, rolename, authinfo) VALUES (0, 'user1', 'password=pass1') ON CONFLICT DO NOTHING;"
docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT citus_set_coordinator_host('postgres1', 5432);"

docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT * FROM citus_add_node('postgres2', 5432);"
docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT * FROM citus_add_node('postgres3', 5432);"


docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT citus_is_coordinator();"
docker exec -it postgres1 psql -U user1 -d db1 -c "SELECT * FROM citus_get_active_worker_nodes();"