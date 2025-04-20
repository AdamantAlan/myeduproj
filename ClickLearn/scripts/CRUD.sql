Create TABLE myData_crud
(DateStamp Date,
Name String,
Value Int32)
ENGINE = MergeTree
PRIMARY KEY Name
ORDER BY (Name, DateStamp);


---------------------------------CREATE-------------------------------------
insert into myData_crud (DateStamp, Name, Value) VALUES
('2025-04-12', 'User1', 10),
('2025-04-13', 'User2', 15),
('2025-04-14', 'User3', 20),
('2025-04-15', 'User4', 25),
('2025-04-16', 'User5', 30),
('2025-04-17', 'User6', 35),
('2025-04-18', 'User7', 40),
('2025-04-19', 'User8', 45),
('2025-04-20', 'User9', 50),
('2025-04-21', 'User10', 55);
---------------------------------READ-------------------------------------
select *
from myData_crud;
---------------------------------UPDATE(MUTATION)-------------------------------------
ALTER TABLE myData_crud
UPDATE Value = 70
WHERE true;
---------------------------------UPDATE(Lightweight)-------------------------------------
ALTER TABLE myData_crud
UPDATE Value = 50
WHERE true
SETTINGS apply_mutations_on_fly = 1;
---------------------------------UPDATE(MUTATION)-------------------------------------
ALTER TABLE myData_crud
DELETE
WHERE true;
---------------------------------UPDATE(Lightweight)-------------------------------------
ALTER TABLE myData_crud
DELETE
WHERE true
SETTINGS apply_mutations_on_fly = 1;

--PS UD с другими движками ищи в engines.sql