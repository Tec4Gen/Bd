USE master;
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'University')
BEGIN
	DROP DATABASE University;
END

CREATE DATABASE University
GO

USE University
GO
--///////////////////////////////////////////////
--                  University Table
--///////////////////////////////////////////////
CREATE TABLE dbo.University
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Title NVARCHAR(100) NOT NULL
	)
GO

--///////////////////////////////////////////////
--                  Faculty Table
--///////////////////////////////////////////////

CREATE TABLE dbo.Faculty
	(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(100) NOT NULL,
	IdUniversity INT NOT NULL FOREIGN KEY REFERENCES University(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
GO

--///////////////////////////////////////////////
--                  Depertament Table
--///////////////////////////////////////////////
CREATE TABLE dbo.Departament
	(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(100) NOT NULL,
	IdFaculty INT NOT NULL FOREIGN KEY REFERENCES Faculty(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
GO
--///////////////////////////////////////////////
--                  Specialty Table
--///////////////////////////////////////////////
CREATE TABLE dbo.Specialty
	(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(100) NOT NULL,
	IdDepartament INT NOT NULL FOREIGN KEY REFERENCES Departament(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
GO

--///////////////////////////////////////////////
--                  Student Table
--///////////////////////////////////////////////

CREATE TABLE dbo.Student
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FirstName NVARCHAR(50) NULL,
	LastName NVARCHAR(50) NULL,
	MiddleName NVARCHAR(50) NULL,
	Course INT NOT NULL,
	DateOfReceipt DATETIME NOT NULL,
	IdSpecialty INT NOT NULL FOREIGN KEY REFERENCES dbo.Specialty (Id) ON DELETE CASCADE ON UPDATE CASCADE
	)
GO

--///////////////////////////////////////////////
--                  Group Table
--///////////////////////////////////////////////
CREATE TABLE dbo.[Group]
	(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Number INT NOT NULL,
	NumberOfstudent INT NOT NULL,
	Course INT NOT NULL,
	IdSpecialty INT NOT NULL FOREIGN KEY REFERENCES Specialty(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
GO
--///////////////////////////////////////////////
--                  Employee Table
--///////////////////////////////////////////////

CREATE TABLE dbo.Employee
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FirstName NVARCHAR(50) NULL,
	LastName NVARCHAR(50) NULL,
	MiddleName NVARCHAR(50) NULL,
	IdDepertament INT NOT NULL FOREIGN KEY REFERENCES Departament(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	)
GO

--///////////////////////////////////////////////
--                  Discipline Table
--///////////////////////////////////////////////

CREATE TABLE dbo.Discipline
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Title NVARCHAR(100) NOT NULL,
	)
GO

--///////////////////////////////////////////////
--                  Perfomance Table
--///////////////////////////////////////////////

CREATE TABLE dbo.Perfomance
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	IdStudent INT NOT NULL FOREIGN KEY REFERENCES Student(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	IdDiscipline INT NOT NULL FOREIGN KEY REFERENCES Discipline(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	Evalution INT NOT NULL,
	DateOfDelivery DATETIME NOT NULL
	)
GO

--///////////////////////////////////////////////
--                  EducationPlane Table
--///////////////////////////////////////////////

CREATE TABLE dbo.EducationPlane
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	IdSpecialty INT NOT NULL FOREIGN KEY REFERENCES Specialty(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	IdDiscipline INT NOT NULL FOREIGN KEY REFERENCES Discipline(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	TotalTime INT NOT NULL,
	DateInstall DATETIME NOT NULL
	)
GO


--///////////////////////////////////////////////
--                  Deducted Table
--///////////////////////////////////////////////

CREATE TABLE dbo.Deducted
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	IdStudent INT NOT NULL FOREIGN KEY REFERENCES Student(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	DateDeducted DATETIME NOT NULL
	)
GO
--///////////////////////////////////////////////
--                  Graduates Table
--///////////////////////////////////////////////

CREATE TABLE dbo.Graduates
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	IdStudent INT NOT NULL FOREIGN KEY REFERENCES Student(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	DateGraduates DATETIME NOT NULL
	)
GO
--///////////////////////////////////////////////
--                  Transfer Table
--///////////////////////////////////////////////

CREATE TABLE dbo.[Transfer]
	(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	IdStudent INT FOREIGN KEY REFERENCES Student(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	IdOldUniversity INT NOT NULL,
	IdNewUniversity INT NULL,
	IdOldFaculty INT NOT NULL,
	IdOldGroup INT NOT NULL,
	IdNewGroup INT NULL,
	DateTransfer DATETIME NOT NULL
	)
GO