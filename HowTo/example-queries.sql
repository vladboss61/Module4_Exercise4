--use AdventureWorksLT2019
SELECT * FROM [Customer] WHERE ID = 9999999

USE [Sample]

-- SELECT
SELECT c.Id + c.Id as New_Id, c.* FROM [Customer] AS c

SELECT TOP 10 * FROM [Customer] AS c

SELECT * FROM [Customer] AS c ORDER BY c.[FirstName]
SELECT * FROM [Customer] AS c ORDER BY c.[FirstName] DESC
SELECT * FROM [Customer] AS c ORDER BY c.[FirstName], c.[Country]

SELECT * FROM [Customer] AS c WHERE c.[Phone] = '030-0074321'
SELECT * FROM [Customer] AS c WHERE (c.[Phone] = '030-0074321' OR [Phone] = '(5) 555-4729')
	AND [FirstName] = 'Maria1'


SELECT * FROM [Customer] AS c WHERE c.[Phone] = '030-0074321' OR [Phone] = '(5) 555-4729'
SELECT * FROM [Customer] AS c WHERE c.[Phone] IN ('030-0074321', '(5) 555-4729')

SELECT * FROM [Customer] AS c WHERE c.[Id] IN 
	(SELECT Id FROM [Order] as o WHERE o.CustomerId = c.Id)

SELECT * FROM [Customer] AS c WHERE c.[Phone] LIKE '030-0074321'
SELECT * FROM [Customer] AS c WHERE c.[Phone] LIKE '03%'
SELECT * FROM [Customer] AS c WHERE c.[Phone] LIKE '%43%'
SELECT * FROM [Customer] AS c WHERE c.[Phone] LIKE '43%'
SELECT * FROM [Customer], [Order] -- == Cross Join

use AdventureWorks2019
select * from Production.Product where SellStartDate
	between CAST('2008-04-30 00:00:00.000' AS DATETIME) and '2012-05-30 00:00:00.000'

SELECT * FROM [Order] AS o WHERE o.OrderDate
	BETWEEN  CAST('2012-08-07 00:00:00.000' AS DATETIME) AND '2012-12-25 00:00:00.000'


--Aggregate Functions Example
SELECT COUNT(*) AS COUNT_1 FROM [Customer] -- include rows with a null id
SELECT COUNT(Id) FROM [Customer] -- not include rows with a null id
SELECT MAX(Id) FROM [Customer]
SELECT SUM(Id) FROM [Customer]
SELECT MIN(Id) FROM [Customer]
SELECT AVG(Id) FROM [Customer]
--Aggregate Functions Example


SELECT * FROM [Customer] AS c GROUP BY c.[Phone] -- not WORK
SELECT c.[Phone] FROM [Customer] AS c GROUP BY c.[Phone]

SELECT COUNT(*) AS Phone_c, c.[Phone] FROM [Customer] AS c
	GROUP BY c.[Phone]

SELECT COUNT(*) AS Phone_c, c.[Phone] FROM [Customer] AS c
	GROUP BY c.[Phone]
	ORDER BY Phone_c DESC

SELECT COUNT(*), c.[Phone] FROM [Customer] AS c
	GROUP BY c.[Phone]
	HAVING c.[Phone] LIKE '(171)%'

SELECT COUNT(*), c.[Phone] FROM [Customer] AS c
	GROUP BY c.[Phone]
	HAVING COUNT(*) = 5

SELECT * FROM [Customer] AS c WHERE c.[Phone] = '030-0074321'

SELECT * FROM Customer AS c
SELECT * FROM [Order] AS o

SELECT COUNT(c.Id) FROM Customer AS c 
	INNER JOIN [Order] as o ON c.Id = o.CustomerId

SELECT COUNT(c.Id) FROM Customer AS c 
	LEFT JOIN [Order] as o ON c.Id = o.CustomerId

SELECT c.*, '--' as divider, o.* FROM Customer AS c 
	LEFT JOIN [Order] as o ON c.Id = o.CustomerId WHERE o.Id IS NULL

SELECT o.*, '--' as divider, c.* FROM [Order] AS o 
	RIGHT JOIN [Customer] as c ON c.Id = o.CustomerId 

SELECT c.*, '--' as divider, o.* FROM Customer AS c 
	FULL JOIN [Order] as o ON c.Id = o.CustomerId

SELECT c.*, '--' as divider, o.* FROM Customer AS c 
	CROSS JOIN [Order] as o

-- ==  SELECT * FROM [Customer], [Order] 
-- SELECT

--UPDATE
SELECT * FROM [Customer] 

UPDATE [Customer]
SET LastName = 'test'
-- UPDATE

--Variable Example
DECLARE @TestVariable AS INT
SET @TestVariable = (SELECT Id FROM [Customer] WHERE ID = 2)
PRINT 'TestVariable'
PRINT @TestVariable
--Variable Example

-- Transaction Example

DECLARE @TestVariableForTr AS INT
SET @TestVariableForTr = 5
PRINT 'TestVariableForTr:'
PRINT @TestVariableForTr 

BEGIN TRANSACTION
	INSERT INTO [Customer] ([Id],[FirstName],[LastName],[City],[Country],[Phone])
		VALUES (199999998,'Maria1', 'Anders','Berlin','Germany','030-0074321')
	
	SELECT * FROM [Customer] WHERE ID = 199999998 OR ID = 1999999981

	INSERT INTO [Customer] ([Id],[FirstName],[LastName],[City],[Country],[Phone])
		VALUES (1999999981,'Maria2', 'Anders','Berlin','Germany','030-0074321')
	
	SELECT * FROM [Customer] WHERE ID = 199999998 OR ID = 1999999981
IF @TestVariableForTr = 5
BEGIN
	ROLLBACK;
END
ELSE 
BEGIN
	COMMIT
END
SELECT * FROM [Customer] WHERE ID = 199999998 OR ID = 1999999981
-- Transaction Example

SELECT 'Test: ' + o.OrderNumber, 'Test1' FROM [Order] as o