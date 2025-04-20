-------------------------------PARTITION(DATE)----------------------------------
CREATE TABLE web_logs
(
    user_id UInt32,
    url String,
    event_time DateTime,
    status_code UInt16
)
ENGINE = MergeTree()
PARTITION BY toYYYYMM(event_time)
ORDER BY (event_time, user_id);


INSERT INTO web_logs (user_id, url, event_time, status_code)
VALUES
(1, 'https://example.com/page1', '2025-01-12 10:00:00', 200),
(2, 'https://example.com/page2', '2025-02-15 11:05:00', 404),
(3, 'https://example.com/page3', '2025-03-20 12:10:00', 500),
(4, 'https://example.com/page4', '2025-04-18 13:15:00', 200),
(5, 'https://example.com/page5', '2025-05-22 14:20:00', 403),
(6, 'https://example.com/page6', '2025-06-10 15:25:00', 200),
(7, 'https://example.com/page7', '2025-07-05 16:30:00', 200),
(8, 'https://example.com/page8', '2025-08-19 17:35:00', 404),
(9, 'https://example.com/page9', '2025-09-30 18:40:00', 500),
(10, 'https://example.com/page10', '2025-10-25 19:45:00', 200);

SELECT * FROM web_logs;

SELECT
    partition,
    count() AS parts,
    sum(rows) AS rows
FROM system.parts
WHERE (database = 'testdata') AND (`table` = 'web_logs') AND active
GROUP BY partition
ORDER BY partition ASC;

-------------------------------PARTITION(KEY)----------------------------------
CREATE TABLE transactions
(
    transaction_id UInt32,
    amount Float32,
    region String,
    transaction_date DateTime
)
ENGINE = MergeTree()
PARTITION BY region
ORDER BY (region, transaction_id);

INSERT INTO transactions (transaction_id, amount, region, transaction_date)
VALUES
(1, 150.00, 'North', '2025-01-12 10:00:00'),
(2, 200.50, 'South', '2025-02-15 11:05:00'),
(3, 320.75, 'East', '2025-03-20 12:10:00'),
(4, 210.00, 'West', '2025-04-18 13:15:00'),
(5, 175.25, 'North', '2025-05-22 14:20:00'),
(6, 400.00, 'South', '2025-06-10 15:25:00'),
(7, 250.30, 'East', '2025-07-05 16:30:00'),
(8, 275.40, 'West', '2025-08-19 17:35:00'),
(9, 300.00, 'Central', '2025-09-30 18:40:00'),
(10, 125.60, 'North', '2025-10-25 19:45:00');

SELECT * FROM transactions;

SELECT
    partition,
    count() AS parts,
    sum(rows) AS rows
FROM system.parts
WHERE (database = 'testdata') AND (`table` = 'transactions') AND active
GROUP BY partition
ORDER BY partition ASC;

-------------------------------PARTITION(EXPRESSION)----------------------------------
CREATE TABLE products
(
    product_id UInt32,
    product_name String,
    price Float32,
    sale_date DateTime
)
ENGINE = MergeTree()
PARTITION BY if(price > 1000, 'expensive', 'cheap')
ORDER BY (sale_date, product_id);

INSERT INTO products (product_id, product_name, price, sale_date)
VALUES
(1, 'Product A', 10.99, '2025-01-15 10:00:00'),
(2, 'Product B', 150.50, '2025-02-22 11:30:00'),
(3, 'Product C', 999.99, '2025-03-10 14:15:00'),
(4, 'Product D', 2499.99, '2025-01-25 16:45:00'),
(5, 'Product E', 5000.00, '2025-04-05 09:00:00'),
(6, 'Product F', 85.80, '2025-05-12 12:30:00'),
(7, 'Product G', 620.00, '2025-06-30 13:00:00'),
(8, 'Product H', 39.99, '2025-07-18 15:20:00'),
(9, 'Product I', 15.49, '2025-08-13 18:10:00'),
(10, 'Product J', 10000.00, '2025-09-25 19:55:00');

SELECT * FROM products;

SELECT
    partition,
    count() AS parts,
    sum(rows) AS rows
FROM system.parts
WHERE (database = 'testdata') AND (`table` = 'products') AND active
GROUP BY partition
ORDER BY partition ASC;
