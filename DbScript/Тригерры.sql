USE University
GO
--INSTED OF DELETED By Student
CREATE TRIGGER studentDeleteInOf
ON Student
INSTEAD OF DELETE
AS

DECLARE @IdStudent INT
DECLARE @Deleted BIT

SET @IdStudent = (SELECT Id FROM DELETED)
SET @Deleted = (SELECT IsDeleted FROm DELETED)

BEGIN TRAN
UPDATE Student SET IsDeleted = 1 WHERE @IdStudent = Id

INSERT dbo.[Log] ([Message], [IdEntity], [Action])
VALUES ('Студент поверхностно удален :)', @IdStudent, 'Deleted')

IF(@Deleted = 1)
BEGIN
	RAISERROR('Поверхностое удаление записи уже было произведено', 16, 10)
	ROLLBACK
END
ELSE
	COMMIT
GO

SELECT * FROM Student WHERE Id =3
DELETE Student WHERE Id =3
SELECT * FROM Student WHERE Id = 3


SELECT * FROM [Log]

--INSTED OF By Student
ALTER TRIGGER groupUpdateInOf
ON [Group]
INSTEAD OF UPDATE
AS
 
DECLARE @userRole INT = (IS_ROLEMEMBER('dbo'))

DECLARE @Id INT = (SELECT Id FROM INSERTED)
DECLARE @Number INT = (SELECT Number FROM INSERTED)
DECLARE @NumberOfStudent INT = (SELECT NumberOfstudent FROM INSERTED)
DECLARE @Course INT = (SELECT Course FROM INSERTED)
DECLARE @IdSpecialty INT = (SELECT IdSpecialty FROM INSERTED)

IF(@userRole = 1)
BEGIN 	


	BEGIN TRAN 

	INSERT dbo.[Log] ([Message], [IdEntity], [Action])
	VALUES ('Информация о группе обновлена', @Id, 'Update')

	UPDATE [Group] SET Number = @Number, NumberOfstudent = @NumberOfStudent, Course = @Course, IdSpecialty = @IdSpecialty
	WHERE Id = @Id

	IF(@NumberOfStudent > 30)
	BEGIN
		RAISERROR('Нельзя иметь в группе болье 30 человек', 16, 10)	
		ROLLBACK
	END
	ELSE 
		COMMIT
	
END
ELSE 
	RAISERROR('нет прав доступа', 16, 10)
GO

UPDATE [Group] SET Number = 2140311795, NumberOfstudent = 29, Course = 1114727966, IdSpecialty = 818
WHERE Id = 1

SELECT * FROM [Log]


CREATE TRIGGER GraduatesUpdateInOf
ON Graduates
INSTEAD OF INSERT
AS
 
DECLARE @userRole INT = (IS_ROLEMEMBER('dbo'))
IF(@userRole = 1)
BEGIN
	DECLARE @IdStudent INT
	DECLARE @CountStudent INT
	SET @IdStudent = (SELECT IdStudent FROM inserted)


	IF(NOT EXISTS (SELECT * FROM dbo.Perfomance WHERE @IdStudent = Id  
										AND Evalution < 3 ) OR EXISTS(SELECT * FROM dbo.Deducted WHERE @IdStudent = Id))
	BEGIN
		BEGIN TRAN
		INSERT dbo.[Log] ([Message], [IdEntity], [Action])
		VALUES ('Студент был добавлен в список выпустившихся', @IdStudent, 'Inserted')

		INSERT dbo.Graduates (IdStudent, DateGraduates)
		SELECT IdStudent, GETDATE() FROM INSERTED

		SET @CountStudent = (SELECT COUNT(Id) FROM Graduates WHERE IdStudent = @IdStudent)
		IF(@CountStudent > 1)
		BEGIN
			RAISERROR('Студент уже существует в списке отчисленных', 16, 10)	
			ROLLBACK
		END 
		ELSE
			COMMIT
	END 
	ELSE 
		RAISERROR('Невозможно добавить студента в выпускники 
					у него не сданы все предметы или он находится в списках отчисленных', 16, 10)
END 
ELSE
	RAISERROR('нет прав доступа', 16, 10)

GO

INSERT Graduates (IdStudent, DateGraduates)
SELECT Id, GETDATE()
FROM Student
WHERE Id = 4

SELECT * FROM Graduates WHERE IdStudent = 4

INSERT Graduates (IdStudent, DateGraduates)
SELECT Id, GETDATE()
FROM Student
WHERE Id = 4

SELECT * FROM Graduates WHERE IdStudent = 4

DELETE dbo.Graduates WHERE IdStudent = 4


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







CREATE TRIGGER studentInsertAft
ON Student
AFTER INSERT
AS
DECLARE @Number INT

DECLARE @Count INT

BEGIN TRAN
SET @Number = (SELECT [Group] FROM INSERTED)

INSERT INTO Student(FirstName, MiddleName,LastName,Course,[Group], DateOfReceipt,IdSpecialty)
SELECT FirstName, MiddleName,LastName,Course,[Group], DateOfReceipt,IdSpecialty
FROM INSERTED 

UPDATE [Group] SET NumberOfstudent += 1
	FROM [Group]
	WHERE (Number = @Number)

SET @Count= (SELECT NumberOfstudent FROM [Group] WHERE Number = @Number)

IF(@Count > 30)
BEGIN 
	RAISERROR('Запись не была добавлена, нельзя содержать в группе больше 30 человек', 16, 10)
	ROLLBACK
END 
ELSE
	COMMIT
GO

SELECT NumberOfstudent
FROM dbo.[Group]
WHERE Number = 2140311795

INSERT INTO Student(FirstName, MiddleName,LastName,Course,[Group], DateOfReceipt, IdSpecialty)
VALUES ('Laurie', 'Laurie','Laurie',968862639,2140311795, '1986-03-26',727)

SELECT NumberOfstudent
FROM dbo.[Group]
WHERE Number = 2140311795

DELETE FROM Student WHERE Id = 100011

SELECT * FROM Student WHERE FirstName = 'Laurie' AND LastName = 'Laurie'