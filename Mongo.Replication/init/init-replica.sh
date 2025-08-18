#!/usr/bin/env bash
set -e
set -u
set -o pipefail

sleep 2

mongosh --host mongo1:27017 --quiet <<'JS'
rs.initiate({
  _id: "rs0",
  members: [
    { _id: 0, host: "mongo1:27017", priority: 2 },
    { _id: 1, host: "mongo2:27017", priority: 1 },
    { _id: 2, host: "mongo3:27017", priority: 1 }
  ]
});

function waitPrimary(maxTries=60) {
  for (let i=0; i<maxTries; i++) {
    const s = rs.status();
    const p = s.members.find(m => m.stateStr === "PRIMARY");
    if (p) { print("PRIMARY:", p.name); return; }
    sleep(1000);
  }
  throw new Error("Primary not elected in time");
}
waitPrimary();
printjson(rs.status());
JS

echo "Replica set rs0 is initialized."