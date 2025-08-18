docker exec -it mongo1 mongosh --quiet --eval "printjson(db.hello())"
docker exec -it mongo1 mongosh --quiet --eval "var s=rs.status(); if(s.ok){printjson(s.members.map(m=>({name:m.name,state:m.stateStr})))} else {printjson(s)}"


docker exec -it mongo1 mongosh --quiet --eval "db.getSiblingDB('test').replica_check.insertOne({msg:'hello'})"
docker exec -it mongo1 mongosh --quiet --eval "printjson(db.getSiblingDB('test').replica_check.find().sort({_id:-1}).limit(1).toArray())"
docker exec -it mongo2 mongosh "mongodb://mongo2:27017/?replicaSet=rs0&readPreference=secondary" --quiet --eval "printjson(db.getSiblingDB('test').replica_check.find().sort({_id:-1}).limit(1).toArray())"
docker exec -it mongo3 mongosh "mongodb://mongo3:27019/?replicaSet=rs0&readPreference=secondary" --quiet --eval "printjson(db.getSiblingDB('test').replica_check.find().sort({_id:-1}).limit(1).toArray())"