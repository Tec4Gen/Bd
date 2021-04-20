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
END
ELSE
	COMMIT
GO

CREATE TRIGGER perfomanceUpdate
ON Perfomance
INSTEAD OF UPDATE
AS
DECLARE @Evalution INT
DECLARE @IdStudent INT
DECLARE @IdDiscipline INT
DECLARE @DateOfDelivery DATETIME2


SET @IdStudent = (SELECT Id FROM inserted)
SET @IdDiscipline = (SELECT IdDiscipline FROM Inserted)
SET @DateOfDelivery = (SELECT DateOfDelivery FROM inserted)
SET @Evalution = (SELECT @Evalution FROM Inserted)

BEGIN TRAN
UPDATE dbo.Perfomance SET IdStudent= @IdStudent, IdDiscipline = IdDiscipline, Evalution = @Evalution, DateOfDelivery = @DateOfDelivery

IF(EXISTS (SELECT * FROM dbo.Graduates) OR EXISTS(SELECT * FROM dbo.Deducted))
BEGIN
	RAISERROR('Невозможно добавить студента в выпускники у него не сданы все предметы', 16, 10)
END 
ELSE 
	COMMIT
GO

CREATE TRIGGER groupTrigger
ON [Group]
AFTER UPDATE
AS
	DECLARE @OldNumber INT
	DECLARE @IdSpecialty INT
	DECLARE @Number INT
	DECLARE @NumberOfstudent INT
	DECLARE @Course INT



	SET @OldNumber = (SELECT [Group] FROM [Group] WHERE Id = @Id)
	SET @IdSpecialty = (SELECT  Inserted.IdSpecialty FROM Inserted)
	SET @Number = (SELECT Inserted.Number FROM inserted)
	SET @NumberOfstudent = (SELECT Inserted.NumberOfstudent FROM Inserted)
	SET @Course = (SELECT Inserted.Course FROM Inserted)

	SET 
	UPDATE dbo.[Group] SET @Number= Number, IdSpecialty = @IdSpecialty, NumberOfstudent = @NumberOfstudent, Course = @Course

	UPDATE dbo.Student SET [Group] = @Number WHERE @OldNumber
WHERE 

GO

CREATE TRIGGER studentTrigger
ON [Group]
AFTER INSERT
AS
	DECLARE @OldNumber INT
	DECLARE @Number INT

	SET @OldNumber = (SELECT Inserted.Number FROM Inserted)
	
	INSERT [Group] (Number, NumberOfstudent, Course,IdSpecialty)
	SELECT Number, NumberOfstudent, Course,IdSpecialty
	FROM inserted

	UPDATE Student SET Number = @Number
	WHERE [Group] = @OldNumber


WHERE 

GO



CREATE TRIGGER specialtyTrigger
ON Specialty
AFTER DELETE
AS
	DECLARE @IdSpec INT

	SET @IdSpec = (SELECT Inserted.Id FROM Inserted)
	
	DELETE FROM Specialty WHERE Id = @IdSpec

	DELETE FROM Student WHERE IdSpecialty = @IdSpec
WHERE 
GO










INSERT INTO Student(FirstName, MiddleName,LastName,Course,[Group], DateOfReceipt, IdSpecialty)
VALUES ('Laurie', 'Laurie','Laurie',968862639,2140311795, '1986-03-26',727)

SELECT NumberOfstudent
FROM dbo.[Group]
WHERE Number = 2140311795

DELETE FROM Student WHERE Id = 100010

SELECT * FROM Student WHERE FirstName = 'Laurie' AND LastName = 'Laurie'
