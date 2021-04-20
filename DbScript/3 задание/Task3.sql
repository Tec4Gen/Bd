USE University
GO

--JOIN 
SELECT Title, FirstName, LastName, MiddleName
FROM Employee 
	JOIN Departament 
	ON Employee.IdDepertament = Departament.Id
GO

SELECT Title, FirstName, LastName, MiddleName
FROM Employee 
	LEFT JOIN Departament 
	ON Employee.IdDepertament = Departament.Id
GO

SELECT Title, FirstName, LastName, MiddleName
FROM Employee 
	RIGHT JOIN Departament 
	ON Employee.IdDepertament = Departament.Id
GO

SELECT Title, FirstName, LastName, MiddleName
FROM Employee 
	CROSS JOIN Departament 
GO

SELECT Title, FirstName, LastName, MiddleName
FROM Employee FULL JOIN Departament 
	ON Employee.IdDepertament = Departament.Id
GO


--ANY ALL
SELECT FirstName, LastName,MiddleName, IdSpecialty as Specialty
FROM Student
WHERE Id = ANY (SELECT IdStudent FROM Perfomance WHERE  Evalution > 4 )
GO

SELECT Id,FirstName, LastName,MiddleName, IdSpecialty as Specialty
FROM Student
WHERE Id < ALL (SELECT IdStudent FROM Graduates WHERE DateGraduates = '2021-01-11')
GO

-- BETWEEN LIKE
SELECT  FirstName, LastName, MiddleName
FROM Student
WHERE DateOfReceipt BETWEEN '2018-09-01' AND '2021-09-01'
GO

SELECT  FirstName, LastName, MiddleName
FROM Student
WHERE LastName LIKE 'Вор_%'
GO

--IN EXISTS
SELECT FirstName, LastName, MiddleName, Course
FROM Student 
WHERE Course IN (3,4)
GO

SELECT FirstName, LastName, MiddleName
FROM Student 
WHERE EXISTS
	(
		SELECT FirstName, LastName, MiddleName, Course
		FROM  Student 
		WHERE IdSpecialty = 1
	)
GO

--CASE 
SELECT Number,
CASE IdSpecialty
  WHEN 1 THEN 'Mathematical support and administration of information systems'
  WHEN 2 THEN 'Applied Mathematics and Computer Science'
  WHEN 3 THEN 'Operations Research and Systems Analysis'
  WHEN 4 THEN 'Computer security'
  ELSE 'Нет названия'
END
FROM [Group]
GO

--CAST CONVERT 
SELECT LastName,FirstName,MiddleName, 'Группа' + ': ' + CAST([Group] AS nvarchar)
FROM Student
GO

SELECT IdStudent, CONVERT(varchar, DateGraduates, 111)
FROM Graduates

--IS NULL
SELECT FirstName, LastName, MiddleName  
FROM Student  
WHERE NOT LastName IS NULL
GO  
-- NULLIF
SELECT FirstName, LastName, MiddleName,   
   NULLIF(141,[Group]) AS 'Null if Equal'  
FROM Student  
GO  

--CHOOSE
SELECT CHOOSE (3, Id,IdSpecialty, [Group])
FROM Student;

--COALESCE 
SELECT COALESCE(IIF(Id < 15, 1, null), IIF(Id < 2, 150, null), IIF(Id < 2, 10, null)) as 'Какой то столбец'
FROM Student;
GO

SELECT COALESCE(CHOOSE(2, null, null), CHOOSE(1, 150, null),  CHOOSE(1, 150, null)) as 'Какой то столбец'
FROM Student;
GO

--IF ELSE
IF EXISTS(SELECT FirstName FROM Student WHERE [Group] = 141)
BEGIN
	SELECT FirstName, LastName, MiddleName
	FROM Student
	WHERE [Group] = 141
END
ELSE 
BEGIN
	PRINT('Not Found')
END

--REPLACE SUBSTRING STUFF STR UNICODE LOWER UPPER
SELECT REPLACE(Title, 'а', '333333') FROM Discipline;

SELECT SUBSTRING(Title, 2, 5) FROM Discipline;

SELECT STUFF (Title, 5, 5, 'ABASD') AS 'STUFF Function'  FROM Discipline;

SELECT STR([Group]) FROM Student

SELECT UNICODE('a') As [Unicode]

SELECT LOWER(Title) FROM Discipline;

SELECT UPPER(Title) FROM Discipline;


--DATE
SELECT DATEADD(YEAR, 7, DateDeducted)
FROM Deducted

SELECT DATEADD(DAY, 7, DateDeducted)
FROM Deducted

SELECT DATEADD(MONTH, 7, DateDeducted)
FROM Deducted


SELECT DATEDIFF(DAY, DateDeducted, '2023-01-01') ResultDay
FROM Deducted 

SELECT DATEDIFF(YEAR, DateDeducted, '2023-01-01') ResultYear 
FROM Deducted 

SELECT DATEDIFF(MONTH, DateDeducted, '2023-01-01') ResultMouth
FROM Deducted 


SELECT DATEPART(YEAR, DateGraduates)
FROM Graduates

SELECT DATEPART(MONTH, DateGraduates)
FROM Graduates

SELECT DATEPART(DAY, DateGraduates)
FROM Graduates

SELECT GETDATE() 
	
SELECT SYSDATETIMEOFFSET()   

SELECT SYSUTCDATETIME()   
	
SELECT GETUTCDATE()  

--GROUP BY
SELECT [Group], COUNT(*) As CountStudent
FROM Student
GROUP BY [Group]
GO

SELECT [Group], COUNT(*) As CountStudent
FROM Student
GROUP BY [Group]
HAVING COUNT(*) > 5
ORDER BY CountStudent DESC
