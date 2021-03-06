USE [master]
GO
/****** Object:  Database [University]    Script Date: 5/17/2021 11:57:30 PM ******/
CREATE DATABASE [University]
 CONTAINMENT = NONE

ALTER DATABASE [University] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [University].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [University] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [University] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [University] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [University] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [University] SET ARITHABORT OFF 
GO
ALTER DATABASE [University] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [University] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [University] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [University] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [University] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [University] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [University] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [University] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [University] SET  ENABLE_BROKER 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [University] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [University] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [University] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [University] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [University] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [University] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [University] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [University] SET  MULTI_USER 
GO
ALTER DATABASE [University] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [University] SET DB_CHAINING OFF 
GO
ALTER DATABASE [University] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [University] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [University] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [University] SET QUERY_STORE = OFF
GO
USE [University]
GO
/****** Object:  Table [dbo].[Perfomance]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfomance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdStudent] [int] NOT NULL,
	[IdDiscipline] [int] NOT NULL,
	[Evalution] [int] NOT NULL,
	[DateOfDelivery] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Course] [int] NOT NULL,
	[Group] [int] NOT NULL,
	[DateOfReceipt] [datetime] NOT NULL,
	[IdSpecialty] [int] NOT NULL,
	[IdUser] [int] NOT NULL,
 CONSTRAINT [PK__Student__3214EC07F514EB60] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[EpmDepView]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EpmDepView]
WITH SCHEMABINDING
AS
SELECT per.Id, std.[Group], per.Evalution, std.FirstName, std.LastName, std.MiddleName
FROM dbo.Student AS std INNER JOIN 
	dbo.Perfomance AS per ON std.Id = per.IdStudent
WHERE (per.Evalution > 3 AND  std.[Group] > 0)
WITH CHECK OPTION
GO
/****** Object:  Table [dbo].[Deducted]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deducted](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdStudent] [int] NOT NULL,
	[DateDeducted] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departament]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departament](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[IdFaculty] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discipline]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discipline](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationPlan]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationPlan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSpecialty] [int] NOT NULL,
	[IdDiscipline] [int] NOT NULL,
	[TotalTime] [int] NOT NULL,
	[DateInstall] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NOT NULL,
	[IdDepertament] [int] NOT NULL,
	[IdUser] [int] NOT NULL,
 CONSTRAINT [PK__Employee__3214EC075282A636] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[IdUniversity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Graduates]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Graduates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdStudent] [int] NOT NULL,
	[DateGraduates] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[NumberOfstudent] [int] NOT NULL,
	[Course] [int] NOT NULL,
	[IdSpecialty] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialty]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialty](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[IdDepartament] [int] NOT NULL,
	[OpeningDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transfer]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdStudent] [int] NULL,
	[IdOldUniversity] [int] NOT NULL,
	[IdNewUniversity] [int] NULL,
	[IdOldFaculty] [int] NOT NULL,
	[IdOldGroup] [int] NOT NULL,
	[IdNewGroup] [int] NULL,
	[DateTransfer] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[University]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/17/2021 11:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IdRole] [int] NOT NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Departament] ON 

INSERT [dbo].[Departament] ([Id], [Title], [IdFaculty]) VALUES (1, N'Сomputer science and programming', 1)
INSERT [dbo].[Departament] ([Id], [Title], [IdFaculty]) VALUES (2, N'System analysis and automatic control', 1)
INSERT [dbo].[Departament] ([Id], [Title], [IdFaculty]) VALUES (3, N'Department of Theoretical Foundations of Computer Security and Cryptography', 1)
SET IDENTITY_INSERT [dbo].[Departament] OFF
GO
SET IDENTITY_INSERT [dbo].[Discipline] ON 

INSERT [dbo].[Discipline] ([Id], [Title]) VALUES (1, N'Математический анализ')
INSERT [dbo].[Discipline] ([Id], [Title]) VALUES (2, N'Дискретная математика')
INSERT [dbo].[Discipline] ([Id], [Title]) VALUES (3, N'Алгебра и геометрия')
INSERT [dbo].[Discipline] ([Id], [Title]) VALUES (4, N'Теория вероятности')
INSERT [dbo].[Discipline] ([Id], [Title]) VALUES (5, N'Базы даных')
SET IDENTITY_INSERT [dbo].[Discipline] OFF
GO
SET IDENTITY_INSERT [dbo].[EducationPlan] ON 

INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (1, 1, 1, 56, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (2, 1, 2, 56, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (3, 1, 3, 46, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (4, 1, 4, 36, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (5, 1, 5, 66, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (6, 1, 1, 40, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (7, 1, 2, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (8, 1, 3, 50, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (9, 1, 4, 40, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (10, 1, 5, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (11, 2, 1, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (12, 2, 2, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (13, 2, 3, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (14, 2, 4, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (15, 2, 5, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (16, 3, 1, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (17, 3, 2, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (18, 3, 3, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (19, 3, 4, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (20, 3, 5, 60, CAST(N'2021-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (21, 4, 1, 120, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (22, 4, 2, 120, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (23, 4, 3, 120, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (24, 4, 4, 120, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[EducationPlan] ([Id], [IdSpecialty], [IdDiscipline], [TotalTime], [DateInstall]) VALUES (25, 4, 5, 120, CAST(N'2018-09-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[EducationPlan] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [MiddleName], [IdDepertament], [IdUser]) VALUES (1, N'Парфенова', N'Василиса', N'Витальевна', 1, 1)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Faculty] ON 

INSERT [dbo].[Faculty] ([Id], [Title], [IdUniversity]) VALUES (1, N'CSIT', 1)
SET IDENTITY_INSERT [dbo].[Faculty] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([Id], [Number], [NumberOfstudent], [Course], [IdSpecialty]) VALUES (1, 141, 9, 1, 1)
INSERT [dbo].[Group] ([Id], [Number], [NumberOfstudent], [Course], [IdSpecialty]) VALUES (2, 331, 13, 3, 4)
INSERT [dbo].[Group] ([Id], [Number], [NumberOfstudent], [Course], [IdSpecialty]) VALUES (3, 441, 5, 4, 1)
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[Perfomance] ON 

INSERT [dbo].[Perfomance] ([Id], [IdStudent], [IdDiscipline], [Evalution], [DateOfDelivery]) VALUES (37, 1, 1, 4, CAST(N'2021-01-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Perfomance] ([Id], [IdStudent], [IdDiscipline], [Evalution], [DateOfDelivery]) VALUES (38, 1, 2, 3, CAST(N'2021-01-14T00:00:00.000' AS DateTime))
INSERT [dbo].[Perfomance] ([Id], [IdStudent], [IdDiscipline], [Evalution], [DateOfDelivery]) VALUES (39, 1, 3, 4, CAST(N'2021-01-18T00:00:00.000' AS DateTime))
INSERT [dbo].[Perfomance] ([Id], [IdStudent], [IdDiscipline], [Evalution], [DateOfDelivery]) VALUES (40, 1, 4, 4, CAST(N'2021-01-25T00:00:00.000' AS DateTime))
INSERT [dbo].[Perfomance] ([Id], [IdStudent], [IdDiscipline], [Evalution], [DateOfDelivery]) VALUES (41, 2, 1, 4, CAST(N'2021-01-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Perfomance] ([Id], [IdStudent], [IdDiscipline], [Evalution], [DateOfDelivery]) VALUES (42, 2, 2, 3, CAST(N'2021-01-14T00:00:00.000' AS DateTime))
INSERT [dbo].[Perfomance] ([Id], [IdStudent], [IdDiscipline], [Evalution], [DateOfDelivery]) VALUES (43, 2, 3, 4, CAST(N'2021-01-18T00:00:00.000' AS DateTime))
INSERT [dbo].[Perfomance] ([Id], [IdStudent], [IdDiscipline], [Evalution], [DateOfDelivery]) VALUES (44, 2, 4, 4, CAST(N'2021-01-25T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Perfomance] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Title]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([Id], [Title]) VALUES (2, N'Student')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Specialty] ON 

INSERT [dbo].[Specialty] ([Id], [Title], [IdDepartament], [OpeningDate]) VALUES (1, N'Mathematical support and administration of information systems', 1, CAST(N'2002-02-03T00:00:00.000' AS DateTime))
INSERT [dbo].[Specialty] ([Id], [Title], [IdDepartament], [OpeningDate]) VALUES (2, N'Applied Mathematics and Computer Science', 2, CAST(N'2001-05-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Specialty] ([Id], [Title], [IdDepartament], [OpeningDate]) VALUES (3, N'Operations Research and Systems Analysis', 2, CAST(N'2001-02-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Specialty] ([Id], [Title], [IdDepartament], [OpeningDate]) VALUES (4, N'Computer security', 3, CAST(N'2001-12-06T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Specialty] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [FirstName], [MiddleName], [LastName], [Course], [Group], [DateOfReceipt], [IdSpecialty], [IdUser]) VALUES (1, N'Толкачева', N'Ксения', N'Петровна', 1, 141, CAST(N'2021-09-01T00:00:00.000' AS DateTime), 1, 2)
INSERT [dbo].[Student] ([Id], [FirstName], [MiddleName], [LastName], [Course], [Group], [DateOfReceipt], [IdSpecialty], [IdUser]) VALUES (2, N'Аксенова', N'Арина', N'Даниэльевна', 1, 141, CAST(N'2021-09-01T00:00:00.000' AS DateTime), 1, 3)
INSERT [dbo].[Student] ([Id], [FirstName], [MiddleName], [LastName], [Course], [Group], [DateOfReceipt], [IdSpecialty], [IdUser]) VALUES (3, N'Ivan123', N'12', N'12', 1, 141, CAST(N'2021-09-01T00:00:00.000' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[University] ON 

INSERT [dbo].[University] ([Id], [Title]) VALUES (1, N'Saratov State University')
SET IDENTITY_INSERT [dbo].[University] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Login], [Password], [IdRole]) VALUES (1, N'Admin', N'ADmin', 1)
INSERT [dbo].[User] ([Id], [Login], [Password], [IdRole]) VALUES (2, N'Log1', N'Log1', 2)
INSERT [dbo].[User] ([Id], [Login], [Password], [IdRole]) VALUES (3, N'Log2', N'Log2', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
/****** Object:  Index [IX_Employee]    Script Date: 5/17/2021 11:57:31 PM ******/
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [IX_Employee] UNIQUE NONCLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Student]    Script Date: 5/17/2021 11:57:31 PM ******/
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [IX_Student] UNIQUE NONCLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Deducted]  WITH CHECK ADD  CONSTRAINT [FK__Deducted__IdStud__5441852A] FOREIGN KEY([IdStudent])
REFERENCES [dbo].[Student] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Deducted] CHECK CONSTRAINT [FK__Deducted__IdStud__5441852A]
GO
ALTER TABLE [dbo].[Departament]  WITH CHECK ADD FOREIGN KEY([IdFaculty])
REFERENCES [dbo].[Faculty] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EducationPlan]  WITH CHECK ADD FOREIGN KEY([IdDiscipline])
REFERENCES [dbo].[Discipline] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EducationPlan]  WITH CHECK ADD FOREIGN KEY([IdSpecialty])
REFERENCES [dbo].[Specialty] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__IdDepe__47DBAE45] FOREIGN KEY([IdDepertament])
REFERENCES [dbo].[Departament] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__IdDepe__47DBAE45]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_User]
GO
ALTER TABLE [dbo].[Faculty]  WITH CHECK ADD FOREIGN KEY([IdUniversity])
REFERENCES [dbo].[University] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Graduates]  WITH CHECK ADD  CONSTRAINT [FK__Graduates__IdStu__571DF1D5] FOREIGN KEY([IdStudent])
REFERENCES [dbo].[Student] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Graduates] CHECK CONSTRAINT [FK__Graduates__IdStu__571DF1D5]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD FOREIGN KEY([IdSpecialty])
REFERENCES [dbo].[Specialty] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Perfomance]  WITH CHECK ADD FOREIGN KEY([IdDiscipline])
REFERENCES [dbo].[Discipline] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Perfomance]  WITH CHECK ADD  CONSTRAINT [FK__Perfomanc__IdStu__4CA06362] FOREIGN KEY([IdStudent])
REFERENCES [dbo].[Student] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Perfomance] CHECK CONSTRAINT [FK__Perfomanc__IdStu__4CA06362]
GO
ALTER TABLE [dbo].[Specialty]  WITH CHECK ADD FOREIGN KEY([IdDepartament])
REFERENCES [dbo].[Departament] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK__Student__IdSpeci__4222D4EF] FOREIGN KEY([IdSpecialty])
REFERENCES [dbo].[Specialty] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK__Student__IdSpeci__4222D4EF]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK__Transfer__IdStud__59FA5E80] FOREIGN KEY([IdStudent])
REFERENCES [dbo].[Student] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK__Transfer__IdStud__59FA5E80]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
/****** Object:  StoredProcedure [dbo].[GetAllStudnet]    Script Date: 5/17/2021 11:57:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[GetAllStudnet]
AS 
BEGIN

SELECT Id, FirstName,LastName,MiddleName, Course, [Group], IdSpecialty
FROM Student

END 
GO
/****** Object:  StoredProcedure [dbo].[GetStudentBydateReceipt]    Script Date: 5/17/2021 11:57:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetStudentBydateReceipt]
@DateReceipt DATETIME2
AS
BEGIN
	SELECT Id,FirstName, MiddleName, LastName, Course,[Group],DateOfReceipt,IdSpecialty
	FROM Student
	WHERE DateOfReceipt = @DateReceipt
END 
GO
USE [master]
GO
ALTER DATABASE [University] SET  READ_WRITE 
GO
