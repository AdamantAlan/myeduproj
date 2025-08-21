docker exec -it redis-master redis-cli

docker exec -it redis-replica-2 redis-cli
docker exec -it redis-replica-1 redis-cli
KEYS *
GET sum:1