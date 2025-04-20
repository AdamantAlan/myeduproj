Create TABLE myData
(DateStamp Date,
Name String,
Value Int32)
ENGINE = MergeTree
PRIMARY KEY Name
ORDER BY (Name, DateStamp);

Create TABLE myData_terget
(DateStamp Date,
Name String,
Value Int32)
ENGINE = MergeTree
PRIMARY KEY Name
ORDER BY (Name, DateStamp);

CREATE MATERIALIZED VIEW myData_im_view
ENGINE = MergeTree
ORDER BY (Value) AS
    SELECT * FROM myData;

CREATE MATERIALIZED VIEW myData_rm_view
REFRESH EVERY 10 MINUTE APPEND TO myData_terget
    AS SELECT * FROM myData;

CREATE MATERIALIZED VIEW myData_rm_view2
REFRESH EVERY 10 SECOND ORDER BY Name --APPEND TO myData_terget
    AS SELECT * FROM myData ;

SELECT version();

insert into myData (DateStamp, Name, "Value")
values ('2025-04-12', 'User2', 10);

select *
from myData;

select *
from myData_im_view;

select *
from myData_rm_view2;

ALTER TABLE myData
UPDATE Value = 50
WHERE true
SETTINGS apply_mutations_on_fly = 1;

ALTER TABLE myData
DELETE
WHERE Value = 10
SETTINGS apply_mutations_on_fly = 1;