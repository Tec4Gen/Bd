USE University
GO

--INSTED OF By Student
ALTER TRIGGER studentInsert
ON Student
INSTEAD OF INSERT
AS
INSERT INTO Student(FirstName, MiddleName,LastName,Course,[Group], DateOfReceipt,IdSpecialty)
SELECT FirstName, MiddleName,LastName,Course,[Group], DateOfReceipt,IdSpecialty
FROM INSERTED 

DECLARE @Number INT

DECLARE @Count INT

BEGIN TRAN
SET @Number = (SELECT [Group] FROM INSERTED)

UPDATE [Group] SET NumberOfstudent += 1
	FROM [Group]
	WHERE (Number = @Number)

SET @Count= (SELECT NumberOfstudent FROM [Group] WHERE Number = @Number)

IF(@Count > 30)
BEGIN 
	RAISERROR('Невозможно содержать в группе больше 30 человек', 16, 10)
	ROLLBACK
END 
ELSE
	COMMIT
GO

CREATE TRIGGER graduatesDelete
ON Graduates
AFTER DELETE
AS

DECLARE @IdStudent INT

SET @IdStudent = (SELECT Id FROM DELETED)

BEGIN TRAN
DELETE FROM Student WHERE Id = @IdStudent

INSERT Graduates (IdStudent, DateGraduates)
VALUES (@IdStudent, GETDATE())

IF(EXISTS (SELECT *  FROM Perfomance WHERE IdStudent = @IdStudent AND Evalution > 2))
BEGIN
	RAISERROR('Невозможно добавить студента в выпускники у него не сданы все предметы', 16, 10)
	ROLLBACK
END
ELSE
	COMMIT
GO

CREATE TRIGGER transferUpdate
ON [Transfer]
INSTEAD OF UPDATE
AS
DECLARE @Number INT
DECLARE @IdStudent INT

SET @IdStudent = (SELECT Id FROM DELETED)

SET @Number = (SELECT [Group] FROM DELETED)

DELETE FROM Student WHERE Id = @IdStudent

UPDATE [Group] SET NumberOfstudent -= 1
	FROM [Group]
	WHERE (Number = @Number)
GO










INSERT INTO Student(FirstName, MiddleName,LastName,Course,[Group], DateOfReceipt, IdSpecialty)
VALUES ('Laurie', 'Laurie','Laurie',968862639,2140311795, '1986-03-26',727)

SELECT NumberOfstudent
FROM dbo.[Group]
WHERE Number = 2140311795

DELETE FROM Student WHERE Id = 100010

SELECT * FROM Student WHERE FirstName = 'Laurie' AND LastName = 'Laurie'