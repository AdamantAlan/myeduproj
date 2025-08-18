#!/usr/bin/env bash
set -euo pipefail

echo "[init] Config RS..."
mongosh --quiet --host cfg1:27017 <<'JS'
rs.initiate({
  _id: "rs-config",
  configsvr: true,
  members: [
    { _id: 0, host: "cfg1:27017", priority: 2 },
    { _id: 1, host: "cfg2:27017", priority: 1 },
    { _id: 2, host: "cfg3:27017", priority: 1 }
  ]
});
function waitPrimary(n=120){for(let i=0;i<n;i++){try{const s=rs.status();if(s.members.some(m=>m.stateStr==="PRIMARY"))return}catch(e){};sleep(1000)};throw new Error("config RS primary timeout")}
waitPrimary();
JS

echo "[init] Shard RS #1..."
mongosh --quiet --host shard1a:27017 <<'JS'
rs.initiate({ _id: "rs-shard-01", members: [{ _id: 0, host: "shard1a:27017" }] });
function waitPrimary(n=60){for(let i=0;i<n;i++){try{const s=rs.status();if(s.members.some(m=>m.stateStr==="PRIMARY"))return}catch(e){};sleep(1000)};throw new Error("shard01 primary timeout")}
waitPrimary();
JS

echo "[init] Shard RS #2..."
mongosh --quiet --host shard2a:27017 <<'JS'
rs.initiate({ _id: "rs-shard-02", members: [{ _id: 0, host: "shard2a:27017" }] });
function waitPrimary(n=60){for(let i=0;i<n;i++){try{const s=rs.status();if(s.members.some(m=>m.stateStr==="PRIMARY"))return}catch(e){};sleep(1000)};throw new Error("shard02 primary timeout")}
waitPrimary();
JS

echo "[init] Add shards to mongos..."
mongosh --quiet --host mongos:27017 <<'JS'
sh.addShard("rs-shard-01/shard1a:27017");
sh.addShard("rs-shard-02/shard2a:27017");
print("Shards added"); 
printjson(sh.status());
JS

# (опционально) включим шардирование DB/коллекции
mongosh --quiet --host mongos:27017 <<'JS'
sh.enableSharding("test");
sh.shardCollection("test.users", { _id: "hashed" });
printjson(db.getSiblingDB("config").collections.findOne({ _id: "test.users" }));
JS

echo "[init] DONE."
