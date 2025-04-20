GRANT CREATE USER ON *.* TO admin;

CREATE USER student IDENTIFIED WITH plaintext_password BY 'ewret3456dfg44';
CREATE ROLE analyst;

GRANT SELECT ON testdata.* TO analyst;
GRANT UPDATE ON testdata.* TO analyst;
GRANT DELETE ON testdata.* TO analyst;
GRANT INSERT ON testdata.* TO analyst;
GRANT CREATE ON testdata.* TO analyst;

GRANT analyst TO student;
SHOW GRANTS FOR analyst;