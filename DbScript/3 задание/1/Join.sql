USE University
GO

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

