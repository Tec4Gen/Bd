
CREATE PROC [dbo].[ResetGorup]
	@OldGroup INT
AS
	DECLARE @Id INT
	DECLARE @Group INT

	DECLARE @cursor CURSOR
	SET @cursor = CURSOR SCROLL FOR
		SELECT Id, [Group] FROM Student

	OPEN @cursor
	FETCH NEXT FROM @cursor INTO @Id, @Group

	IF(EXISTS(SELECT * FROM [Group] WHERE Number = @OldGroup))
	BEGIN 
		WHILE @@FETCH_STATUS = 0
			BEGIN
				UPDATE Student
				SET [Group] = 0
				WHERE [group] = @OldGroup
		
				FETCH NEXT FROM @cursor INTO @Id, @Group
				
			END
			
		CLOSE @cursor
		DEALLOCATE @cursor
	END
GO
EXEC [ResetGorup] @OldGroup = 123

ALTER PROC UpdateStudent
	@Id INT,
	@FirstName NVARCHAR(50),
	@MiddleName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Course INT,
	@Group INT,
	@OldGroup INT,
	@DateOfReceipt DATETIME2,
	@IdSpecialty INT
AS
SET @OldGroup= (SELECT [Group] FROM Student WHERE Id = @Id)

	UPDATE dbo.Student SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Course = @Course,
	[Group] = @Group, DateOfReceipt = @DateOfReceipt,IdSpecialty = @IdSpecialty
	WHERE @Id = Id

	IF(@Group != @OldGroup) 
		UPDATE dbo.[Group] SET NumberOfstudent += 1 WHERE Number = @Group
		UPDATE dbo.[Group] SET NumberOfstudent -= 1 WHERE Number = @OldGroup
GO

EXEC UpdateStudent @Id = 6, @FirstName = '123213', @LastName = '123213', @MiddleName = '123213', @Course = 1, @Group = 123, @DateOfReceipt ='01.01.2019', @IdSpecialty = 1,@OldGroup = 1378500372

SELECT * FROM Student WHERE [Group] = 123
SELECT * FROM [Group] WHERE Number = 1378500372
SELECT * FROM [Group] WHERE Number = 123

UPDATE dbo.Student SET [Group] = 1378500372 WHERE Id = 123

SELECT * FROM Student WHERE [Group] = 1378500372
SELECT * FROM [Group] WHERE Number = 1378500372
SELECT * FROM [Group] WHERE Number = 123


CREATE PROC GetAllStudnet
AS 
BEGIN

SELECT Id, FirstName,LastName,MiddleName, Course, [Group], IdSpecialty
FROM Student

END 
GO

EXEC GetAllStudnet

ALTER PROC InsertStudent
	@Id INT OUTPUT,
	@FirstName NVARCHAR(50),
	@MiddleName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Course INT,
	@Group INT,
	@DateOfReceipt DATETIME2,
	@IdSpecialty INT
AS
BEGIN TRAN 
	INSERT INTO dbo.Student (FirstName, MiddleName, LastName, Course,[Group],DateOfReceipt,IdSpecialty )
	VALUES (@FirstName, @MiddleName, @LastName,@Course, @Group, @DateOfReceipt, @IdSpecialty)

	UPDATE dbo.[Group] SET NumberOfstudent += 1 WHERE Number = @Group

	DECLARE @Countstudnet INT = (SELECT NumberOfstudent FROM [Group] WHERE Number = @Group)
	IF(@Countstudnet > 30)
	BEGIN 
		ROLLBACK

	END
	ELSE
		SET @Id = SCOPE_IDENTITY()
		COMMIT
GO

EXEC InsertStudent @Id = 0,@FirstName = '123213', @LastName = '123213', @MiddleName = '123213', @Course = 1, @Group = 123, @DateOfReceipt ='01.01.2019', @IdSpecialty = 1

CREATE PROC GetStudentBydateReceipt
@DateReceipt DATETIME2
AS
BEGIN
	SELECT Id,FirstName, MiddleName, LastName, Course,[Group],DateOfReceipt,IdSpecialty
	FROM Student
	WHERE DateOfReceipt = @DateReceipt
END 

EXEC GetStudentBydateReceipt @DateReceipt= '2019-01-01'
