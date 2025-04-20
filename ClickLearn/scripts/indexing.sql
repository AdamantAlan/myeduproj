--------------------------------SKIPPING INDEX(MINMAX)----------------------------------
CREATE TABLE sales
(
    date Date,
    product_id UInt32,
    quantity UInt32,
    price Float32
)
ENGINE = MergeTree()
ORDER BY (date, product_id)
SETTINGS index_granularity = 8192;

ALTER TABLE sales
ADD INDEX minmax_price_idx (price) TYPE minmax GRANULARITY 4;

--------------------------------SKIPPING INDEX(SET)----------------------------------
CREATE TABLE users
(
    user_id UInt32,
    name String,
    age UInt8,
    country_code String
)
ENGINE = MergeTree()
ORDER BY user_id
SETTINGS index_granularity = 8192;

ALTER TABLE users
ADD INDEX set_country_idx (country_code) TYPE set(100) GRANULARITY 4; -- 100 unique value per granula

--------------------------------SKIPPING INDEX(bloom_filter)----------------------------------
CREATE TABLE products
(
    product_id UInt32,
    name String,
    description String,
    price Float32
)
ENGINE = MergeTree()
ORDER BY product_id
SETTINGS index_granularity = 8192;

ALTER TABLE products
ADD INDEX bloom_descr_idx (description) TYPE bloom_filter(0.01) GRANULARITY 64; -- 0.01 - указывает на допустимую вероятность ложных срабатываний
























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