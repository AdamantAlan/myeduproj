docker exec -it redis-node-1 redis-cli -p 7001
docker exec -it redis-node-2 redis-cli -p 7002
docker exec -it redis-node-3 redis-cli -p 7003
docker exec -it redis-node-4 redis-cli -p 7004
docker exec -it redis-node-5 redis-cli -p 7005
docker exec -it redis-node-6 redis-cli -p 7006

KEYS *
GET sum:1