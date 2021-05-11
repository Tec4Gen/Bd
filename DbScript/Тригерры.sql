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


ALTER TRIGGER studentTriggerAft
ON Student
AFTER UPDATE
AS
	DECLARE @Id INT
	DECLARE @FirstName NVARCHAR(50)
	DECLARE @MiddleName NVARCHAR(50)
	DECLARE @LastName NVARCHAR(50)
	DECLARE @Course INT
	DECLARE @Group INT
	DECLARE @OldGroup INT
	DECLARE @DateOfReceipt DATETIME2
	DECLARE @IdSpecialty INT

	SET @Id = (SELECT Id FROM Inserted)
	SET @IdSpecialty = (SELECT  Inserted.IdSpecialty FROM Inserted)
	SET @FirstName = (SELECT Inserted.FirstName FROM inserted)
	SET @MiddleName = (SELECT Inserted.MiddleName FROM Inserted)
	SET @LastName = (SELECT Inserted.LastName FROM Inserted)
	SET @Course = (SELECT Inserted.Course FROM Inserted)
	SET @Group = (SELECT Inserted.[Group] FROM Inserted)
	SET @DateOfReceipt = (SELECT Inserted.DateOfReceipt FROM Inserted)

	SET @OldGroup =  (SELECT [Group] FROM Deleted)

	UPDATE dbo.Student SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Course = @Course,
	[Group] = @Group, DateOfReceipt = @DateOfReceipt,IdSpecialty = @IdSpecialty
	WHERE @Id = Id

	IF(@Group != @OldGroup) 
		UPDATE dbo.[Group] SET NumberOfstudent += 1 WHERE Number = @Group

GO



SELECT * FROM Student JOIN [Group] ON Number=[group]

SELECT * FROM Student WHERE [Group] = 2026371279

SELECT * FROM [Group] WHERE Number = 2026371279
SELECT * FROM [Group] WHERE Number = 652411300

UPDATE dbo.Student SET [Group] = 652411300 WHERE Id = 23541

SELECT * FROM [Group] WHERE Number = 2026371279
SELECT * FROM [Group] WHERE Number = 652411300

1025515425
2026371279
652411300
978987186


ALTER TRIGGER specialtyTrigger
ON Specialty
AFTER DELETE
AS
	DECLARE @IdSpec INT

	SET @IdSpec = (SELECT DELETED.Id FROM DELETED)
	
	DELETE FROM Specialty WHERE Id = @IdSpec

	DELETE FROM [Group] WHERE IdSpecialty = @IdSpec

GO
SELECT * FROM Specialty WHERE Id = 1

SELECT * FROM [Group] WHERE IdSpecialty = 1

DELETE Specialty WHERE Id = 1

SELECT * FROM Specialty WHERE Id = 1

SELECT * FROM [Group] WHERE IdSpecialty = 1
