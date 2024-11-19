# SQL Basics workshop

## Table of Contents
1. [Types of databases](#types-of-databases)
1. [RDBMS Concept](#rdbms-concept)
1. [SQL Constraints](#sql-constraints)
1. [Types of SQL Commands](#types-of-sql-commands)
1. [SQL Operators](#sql-operators)
1. [Special functions](#special-functions)
1. [Aggregate Functions](#aggregate-functions)
1. [Alias](#alias)
1. [Subquery](#subquery)
1. [JOINs](#joins)
1. [ACID Principles](#acid-principles)
1. [Transactions](#transactions)

## Types of databases
To provide you with a broader perspective on the various types of databases, we will give a general overview. However, for this particular discussion, we will focus specifically on **SQL** databases.

**SQL (Structured Query Language) Databases**
- Relational databases that store data in tables consisting of rows and columns.
- Highly structured with predefined schemas.
- Ideal for transactional data, complex queries, and ACID compliance (Atomicity, Consistency, Isolation, Durability).
- Examples: MySQL, PostgreSQL, SQL Server

**NoSQL Databases**
- Not only SQL: A broader category of databases that don't adhere to the relational model.
- Flexible schema for handling large volumes of unstructured or semi-structured data.
- Scalable and often used for big data applications.
- *Key-Value Stores*:
    - Simplest NoSQL model.
    - Store data as key-value pairs.
    - Ideal for: caching, session management, and simple lookups.
    - Examples: Redis, Amazon DynamoDB
- *Column-Family Databases*:
    - Store data in columns rather than rows.
    - Highly scalable for large datasets with many columns.
    - Ideal for: time-series data, wide-column data.
    - Examples: Apache Cassandra, HBase
- *Document Databases*:
    - Store data in flexible, JSON-like documents.
    - Ideal for: flexible schemas, content management, and document-oriented applications.
    - Examples: MongoDB, Couchbase
- *Graph Databases*:
    - Model data as nodes (entities) and relationships (edges).
    - Ideal for: social networks, recommendation systems, and network analysis.
    - Examples: Neo4j, Amazon Neptune

**Analytical (OLAP) Databases**
- Optimized for reading and analyzing large amounts of historical data.
- Used for data warehousing, business intelligence, and reporting.
- Often multidimensional, allowing for complex aggregations and calculations.
- Examples: Star Schema, Snowflake

## RDBMS Concept
RDBMS stands for Relational Database Management System. It's a type of database management system that stores and organizes data in a structured format using tables, records, and fields. These elements are linked together using specific relationships.
**RDBMSs primarily use SQL as their interface for interacting with data. SQL, designed for relational databases, enables seamless management of tables, records, and relationships.**

**Key Characteristics of RDBMS:**
- Tables: Data is organized into tables, where each table represents a specific entity (like customers, products, or orders).
- Rows: Each row in a table represents a record or instance of the entity.
- Columns: Columns define the attributes or properties of the entity.
- Relationships: Tables are linked together using relationships, most commonly through primary and foreign keys.

**Benefits of RDBMS:**
- Data Integrity: RDBMS ensures data consistency and accuracy through features like primary keys, foreign keys, and constraints.
- Data Security: RDBMS provides mechanisms for controlling access to data, ensuring that only authorized users can view or modify it.
- Data Independence: Data is stored separately from the applications that use it, making it easier to maintain and update.
- Scalability: RDBMS can handle large amounts of data and can be scaled to accommodate growing data volumes.

## SQL Constraints
SQL constraints are rules that you can define on a table or column to restrict the kind of data that can be stored.
- They help to maintain data accuracy and consistency.
- SQL constraints help enforce these ACID properties (see later) by defining rules about the data that can be stored in a database.

**Types of SQL constraints:**
**NOT NUL**L: Ensures that a column cannot have a null value.
```sql
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(50) NOT NULL,
    ContactNumber VARCHAR(20)
);
```
**UNIQUE**: Ensures that all values in a column are different.
```sql
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(50) NOT NULL,
    ProductCode VARCHAR(20) UNIQUE
);
```
**PRIMARY KEY**: Uniquely identifies each row in a table. It is a combination of NOT NULL and UNIQUE constraints.   
```sql
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE
);
```
**FOREIGN KEY**: Ensures referential integrity between two tables. It creates a link between a column in one table and a primary key in another table.
```sql
ALTER TABLE Orders
ADD FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID);
```
**CHECK**: Defines a specific condition that all rows in a table must satisfy.
```sql
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Age INT CHECK (Age >= 18)
);
```

## Types of SQL Commands
SQL commands can be categorized into four main types based on their function:

**Data Definition Language (DDL)**
- **DDL** commands are used to define the structure of the database.
- They are used to create, modify, and delete database objects like tables, indexes, and views.
- Examples:
    - **CREATE**: Creates new database objects (e.g., CREATE TABLE, CREATE INDEX).
    - **ALTER**: Modifies existing database objects (e.g., ALTER TABLE, ALTER INDEX).
    - **DROP**: Deletes existing database objects (e.g., DROP TABLE, DROP INDEX).
    - **TRUNCATE**: Removes all rows from a table but keeps the table structure.

**Data Manipulation Language (DML)**
- **DML** commands are used to manipulate data within the database. 
- They are used to insert, update, and delete data.
- Examples:
    - **INSERT**: Adds new rows to a table.
    - **UPDATE**: Modifies existing data in a table.
    - **DELETE**: Removes rows from a table.

**Data Control Language (DCL)**
- **DCL** commands are used to control access to the database.
- They are used to grant or revoke privileges to users.
- Examples:
    - **GRANT**: Grants privileges to a user or role.
    - **REVOKE**: Revokes privileges from a user or role.

**Data Query Language (DQL)**
- **DQL** commands are used to retrieve data from the database.
- Example:
    - **SELECT**: Retrieves data from one or more tables.

```sql
SELECT projection 
FROM source clause
WHERE selection
GROUP BY column
HAVING exp
ORDER BY columns ASC/DESC [NULLS FIRST | NULLS LAST]
```

## SQL Operators
SQL operators are symbols used to perform operations on data in a database. They can be categorized into several types:

**Relational Operators**
- Equal to: **=**
- Less than: **<**
- Greater than: **>**
- Less than or equal to: **<=** 
- Greater than or equal to: **>=**
- Not equal to: **<>**

**Logical Operators**
- **AND**: Logical AND. Both conditions must be true.
- **OR**: Logical OR. At least one condition must be true.
- **NOT**: Logical NOT. Reverses the result of a condition.

**Set Operators**
- **BETWEEN**: Checks if a value is within a range.
```sql
SELECT * FROM products WHERE price BETWEEN 10 AND 20;
```
- **IN**: Checks if a value is in a list of values or a subquery.
```sql
SELECT * FROM customers WHERE country IN ('USA', 'UK', 'Canada');
```
- **ALL**: Compares a value to all values in a list or subquery.
```sql
SELECT * FROM products
WHERE price < ALL (SELECT price FROM products WHERE category = 'Electronics');
```
- **ANY**: Compares a value to at least one value in a list or subquery.
```sql
SELECT * FROM customers
WHERE customer_id = ANY (
  SELECT customer_id FROM orders
  WHERE order_date > '2023-01-01'
);
```
- **IS NULL**: Checks if a value is NULL.
```sql
SELECT * FROM customers WHERE email IS NULL;
```
- **IS NOT NULL**: Checks if a value is not NULL.
```sql
SELECT * FROM customers WHERE email IS NOT NULL;
```
- **EXISTS**: Checks if a subquery returns any rows.
```sql
SELECT * FROM customers WHERE EXISTS (SELECT 1 FROM orders WHERE customers.customer_id = orders.customer_id);
```
- Pattern Matching Operators (**LIKE**)
%: Matches any sequence of characters.
_: Matches any single character.
```sql
SELECT * FROM customers WHERE first_name LIKE 'J%';
SELECT * FROM customers WHERE last_name LIKE '_mith';
```
## Special functions

**String Functions**
String functions are used to manipulate text data in a database. Some common string functions include:
- **LOWER(string)**: Converts all characters in a string to lowercase.
```sql
SELECT LOWER('Hello World'); -- would return hello world.
```
- **UPPER(string)**: Converts all characters in a string to uppercase.
```sql
SELECT UPPER('hello world'); -- would return HELLO WORLD.
```
- **CONCAT(string1, string2, ...)**: Combines two or more strings into a single string.
```sql
SELECT CONCAT('first', ' ', 'last'); -- would return first last.
```
- **LENGTH(string)**: Returns the number of characters in a string.
```sql
SELECT LENGTH('hello'); -- would return 5.
```

**Math Functions**
Math functions are used to perform mathematical operations on numeric data. A common math function is:
- **ROUND**(number, decimals): Rounds a number to a specified number of decimal places.
```sql
SELECT ROUND(3.14159, 2); --would return 3.14.
```

**Date and Time Functions**
Date and time functions are used to manipulate and extract information from date and time values.
- **EXTRACT**(part FROM date): Extracts a specific part of a date.
- Common parts to extract: YEAR, MONTH, DAY, HOUR, MINUTE, SECOND.
```sql
SELECT EXTRACT(YEAR FROM '2023-11-23'); -- would return 2023.
```
## Aggregate Functions
Aggregate functions in SQL are used to perform calculations on a set of rows and return a single value. These functions are especially useful when you need to summarize or group data.

**Common aggregate functions include:**
- COUNT(*): Counts the number of rows in a set.
- SUM(column_name): Calculates the sum of all values in a numeric column.
- AVG(column_name): Calculates the average of all values in a numeric column.
- MIN(column_name): Finds the minimum value in a numeric column.
- MAX(column_name): Finds the maximum value in a numeric column.
```sql
SELECT COUNT(id) 
FROM car
WHERE model = 'Fiat';
```

## Alias
An alias is a temporary name given to a table or column in a SQL query. It's like a nickname that you give something for a specific purpose.

**When to use aliases:**
- To make queries more readable: When table or column names are long or complex, aliases can make your SQL statements easier to understand.
- To avoid repeating long names: If you need to reference a table or column multiple times in a query, using an alias can save you from typing the full name repeatedly.
- To create temporary names: You might use an alias to create a temporary name for a calculated field or a subquery.

```sql
SELECT price * 1.2 as RAISED_PRICE, model
FROM car
WHERE model = 'Opel';
```

## Subquery
A subquery is a SELECT statement nested inside another SQL statement. It's essentially a query within a query. Subqueries are used to derive data that is then used in the main query.

**When to use Subqueries:**
- Filtering data: Using a subquery in a WHERE clause to filter rows based on the results of another query.
- Comparing values: Using a subquery to compare values from different tables.
- Getting aggregated values: Using a subquery to calculate aggregated values (like SUM, AVG, COUNT) and then use those values in the main query.
- Example: Let's say we have two tables: customers and orders. We want to find the customers who have placed more than 5 orders.
```sql
SQL
SELECT *
FROM customers
WHERE customer_id IN (
    SELECT customer_id
    FROM orders
    GROUP BY customer_id
    HAVING COUNT(*) > 5
);
```

## JOINs
SQL joins are used to combine rows from two or more tables based on a related column between them.<br>
There are different types of joins, such as inner joins, outer joins (left, right, full), and self joins, each with its specific purpose.

**INNER JOIN**
- Returns rows that have matching values in both tables.
- Example: This query will return only customers who have placed orders.
```sql
SELECT customers.customer_name, orders.order_id
FROM customers
INNER JOIN orders
ON customers.customer_id = orders.customer_id;   
```

**LEFT OUTER JOIN**
- Returns all rows from the left table, and the matched rows from the right table.
- Example: This query will return all customers, even if they haven't placed orders. If there's no matching order, the order_id will be NULL.
```sql
SELECT customers.customer_name, orders.order_id
FROM customers
LEFT JOIN orders
ON customers.customer_id = orders.customer_id;   
```

**RIGHT OUTER JOIN**
- Returns all rows from the right table, and the matched rows from the left table.
- Example: This query will return all orders, even if there's no corresponding customer.
```sql
SELECT customers.customer_name, orders.order_id
FROM customers
RIGHT JOIN orders
ON customers.customer_id = orders.customer_id;   
```

**FULL OUTER JOIN**
- Returns all rows when there is a match in either left or right table.
- Example: This query will return all customers and all orders, whether or not there's a match.
```sql
SELECT customers.customer_name, orders.order_id
FROM customers
FULL OUTER JOIN orders
ON customers.customer_id = orders.customer_id;   
```

**FULL OUTER JOIN excluding INNER JOIN**
- To achieve this, you can use a UNION between a LEFT JOIN and a RIGHT JOIN, excluding rows where both customer_id and order_id are not NULL:
- Example: The query is designed to find customers without orders and orders without customers, which can help identify anomalies or inconsistencies in the data.
```sql
SELECT customers.customer_name, orders.order_id
FROM customers
LEFT JOIN orders
ON customers.customer_id = orders.customer_id   
WHERE orders.order_id IS NULL
UNION
SELECT customers.customer_name, orders.order_id
FROM customers
RIGHT JOIN orders
ON customers.customer_id = orders.customer_id   
WHERE customers.customer_id IS NULL;
```

## ACID Principles
Imagine you're transferring money from one bank account to another. This simple transaction involves several steps: checking your balance, deducting the amount from one account, and adding it to the other. Now, imagine what could go wrong:
- Your computer crashes halfway through the transaction.
- There's a power outage.
- A software bug causes an error.
In these situations, you wouldn't want to lose your money, right? That's where the ACID principles come in.

**ACID stands for:**
- **Atomicity**: This means that a transaction is either completed entirely or not at all. It's like flipping a coin: there's no halfway point. If the money transfer can't be completed successfully, the system is rolled back to its previous state.
- **Consistency**: After a transaction is completed, the database must be in a consistent state. This means that all the rules and constraints of the database are still valid. For example, if you transfer money from one account to another, the total amount of money in the system should remain the same.
- **Isolation**: Each transaction is isolated from other transactions. This means that one transaction cannot see the intermediate results of another transaction. This prevents conflicts and ensures data integrity.
- **Durability**: Once a transaction is committed, it's permanent. Even if there's a system failure, the changes made by the transaction will be preserved.

**Why are ACID principles important?**
- Data integrity: They ensure that data is accurate and consistent.
- Reliability: They make database systems more reliable and trustworthy.
- Concurrency: They allow multiple transactions to run concurrently without interfering with each other.

In simple terms, ACID principles are like a set of rules that database systems follow to ensure that data is handled correctly and safely. They are essential for any system that needs to maintain data integrity and reliability.

## Transactions

**What is ADO.NET?**
- ADO.NET is a .NET Framework data access technology that provides a set of classes for connecting to and managing data from various data sources, primarily relational databases. It acts as a bridge between your .NET applications and databases, allowing you to perform CRUD operations (Create, Read, Update, Delete).
E.g.: **Npgsql** is an open-source library that provides an ADO.NET implementation for PostgreSQL databases.

**Transactions in ADO.NET**
- A transaction is a logical unit of work that consists of one or more SQL statements. In database terms, a transaction is a sequence of operations that are treated as a **single unit**. The ACID properties (Atomicity, Consistency, Isolation, Durability) are fundamental to database transactions.
