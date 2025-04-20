------------SummingMergeTree------------------------------------
Create table sales
(
    date Date,
    product_id UInt32,
    sales_amount Float32,
    some_field Float32
)
ENGINE = SummingMergeTree(sales_amount)
PRIMARY KEY product_id
ORDER BY (product_id, date);

INSERT INTO sales (date, product_id, sales_amount, some_field) VALUES
('2025-04-01', 1, 100.0, 200),
('2025-04-01', 1, 200.0, 100),
('2025-04-01', 1, 200.0, 100),
('2025-04-01', 1, 200.0, 100),
('2025-04-01', 1, 200.0, 100),
('2025-04-01', 1, 200.0, 100);

SELECT
    product_id,
    date,
    sales_amount,
    some_field
FROM
    sales;
------------CollapsingMergeTree------------------------------------
CREATE TABLE orders (
    order_id UInt32,
    product_id UInt32,
    quantity Int32,
    Sign Int8
) ENGINE = CollapsingMergeTree(Sign)
ORDER BY (order_id);

INSERT INTO orders (order_id, product_id, quantity, Sign) VALUES (1, 101, 10, 1);
INSERT INTO orders (order_id, product_id, quantity, Sign) VALUES (1, 101, 10, -1);
INSERT INTO orders (order_id, product_id, quantity, Sign) VALUES (1, 101, 20, 1);

SELECT
    order_id,
    product_id,
    quantity,
    Sign
FROM
    orders FINAL;

SELECT
    order_id,
    product_id,
    sum(quantity * Sign) AS final_quantity
FROM
    orders
GROUP BY
    order_id, product_id;
------------ReplacingMergeTree------------------------------------
CREATE TABLE users (
    user_id UInt32,
    name String,
    age UInt8,
    version UInt32
) ENGINE = ReplacingMergeTree(version)
ORDER BY (user_id);

INSERT INTO users (user_id, name, age, version) VALUES (1, 'Alice', 30, 1);
INSERT INTO users (user_id, name, age, version) VALUES (1, 'Alice', 31, 2);

SELECT
    user_id,
    name,
    age,
    version
FROM
    users FINAL;
------------AggregatingMergeTree-----------SUM----------------------
CREATE TABLE raw_sales_temp
(
    date Date,
    product_id UInt32,
    quantity UInt32
)
ENGINE = MergeTree()
ORDER BY (date, product_id);

CREATE TABLE raw_sales
(
    date Date,
    product_id UInt32,
    quantity AggregateFunction(Sum, UInt32)
)
ENGINE = AggregatingMergeTree()
ORDER BY (date, product_id);

INSERT INTO raw_sales_temp
VALUES
('2023-01-01', 1, 10),
('2023-01-01', 1, 20),
('2023-01-01', 2, 15);

INSERT INTO raw_sales
SELECT
    date,
    product_id,
    sumState(quantity) AS quantity
FROM
    raw_sales_temp
GROUP BY
    date,
    product_id;

SELECT
    date,
    product_id,
    sumMerge(quantity) AS total_quantity
FROM raw_sales
GROUP BY date, product_id;
------------AggregatingMergeTree-----------COUNT----------------------
CREATE TABLE raw_sales_count_temp
(
    date Date,
    product_id UInt32
)
ENGINE = MergeTree()
ORDER BY (date, product_id);

CREATE TABLE raw_sales_count
(
    date Date,
    product_id UInt32,
    quantity AggregateFunction(Count, UInt32)
)
ENGINE = AggregatingMergeTree()
ORDER BY (date, product_id);

INSERT INTO raw_sales_count_temp
VALUES
('2023-01-01', 1),
('2023-01-01', 1),
('2023-01-01', 2),
('2023-01-01', 2),
('2023-01-01', 2),
('2023-01-02', 1);

INSERT INTO raw_sales_count
SELECT
    date,
    product_id,
    countState() AS quantity  -- Используем countState для вставки
FROM
    raw_sales_count_temp
GROUP BY
    date,
    product_id;

SELECT
    date,
    product_id,
    countMerge(quantity) AS total_count
FROM
    raw_sales_count
GROUP BY
    date,
    product_id ;