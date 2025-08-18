docker exec -it cfg1 mongosh --quiet --host cfg1:27017 --eval "rs.initiate({_id:'rs-config',configsvr:true,members:[{_id:0,host:'cfg1:27017',priority:2},{_id:1,host:'cfg2:27017'},{_id:2,host:'cfg3:27017'}]})"
docker exec -it cfg1 mongosh --quiet --host cfg1:27017 --eval "var s=rs.status(); printjson(s.members.map(m=>({name:m.name,state:m.stateStr})))"

docker exec -it mongos mongosh --quiet --host cfg1:27017 --eval "db.adminCommand({ ping: 1 })"
docker exec -it mongos mongosh --quiet --host cfg2:27017 --eval "db.adminCommand({ ping: 1 })"
docker exec -it mongos mongosh --quiet --host cfg3:27017 --eval "db.adminCommand({ ping: 1 })"


docker exec -it mongos mongosh --quiet --host mongos:27017 --eval "db.adminCommand({ ping: 1 })"
docker exec -it mongos mongosh --quiet --host mongos:27017 --eval "sh.status()"



docker exec -it mongos mongosh --quiet --eval "printjson(db.getSiblingDB('test').users.stats().sharded)"
docker exec -it mongos mongosh --quiet --eval "db.getSiblingDB('test').users.insertMany(Array.from({length:20000},(_,i)=>({n:i})))"
docker exec -it mongos mongosh --quiet --eval "printjson(db.getSiblingDB('test').users.countDocuments({}))"
docker exec -it mongos mongosh --quiet --eval "db.getSiblingDB('test').users.getShardDistribution()"








