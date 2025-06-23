IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TaskTrackPro')
  CREATE DATABASE TaskTrackPro;
GO
USE TaskTrackPro;
GO

-- Table Alerts
DROP TABLE IF EXISTS Alerts;
GO
CREATE TABLE Alerts (
	Id int NOT NULL,
	Email nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Message nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__Alerts__3214EC07FA1D36D1 PRIMARY KEY (Id)
);


-- Table Projects 
DROP TABLE IF EXISTS Projects;
GO
CREATE TABLE Projects (
	Name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar(400) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	StarTDate date NOT NULL,
	EndDate date NOT NULL,
	Duration int NOT NULL,
	AdminEmail nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	LiderEmail nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__Projects__737584F7CF54737B PRIMARY KEY (Name)
);


-- Table Resources
DROP TABLE IF EXISTS Resources;
GO
CREATE TABLE Resources (
	Name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	TaskType nvarchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Description nvarchar(400) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	InCommon nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__Resource__737584F71B5E46F1 PRIMARY KEY (Name)
);


-- Table Tasks
DROP TABLE IF EXISTS Tasks;
GO
CREATE TABLE Tasks (
	Name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Duration int NOT NULL,
	Description nvarchar(400) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	StarTDate date NOT NULL,
	EndDate date NOT NULL,
	LateStarTDate date NOT NULL,
	LateEndDate date NOT NULL,
	Slak int NOT NULL,
	Status nvarchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__Tasks__737584F78E8BAB2E PRIMARY KEY (Name)
);


-- Table Users
DROP TABLE IF EXISTS Users;
GO
CREATE TABLE Users (
	Email nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Name nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Surname nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Password nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Birthdate date NOT NULL,
	AdminSistem bit NOT NULL,
	[Login] bit NOT NULL,
	CONSTRAINT PK__Users__A9D105355288E570 PRIMARY KEY (Email)
);


-- Table AlertsProjects
DROP TABLE IF EXISTS AlertsProjects;
GO
CREATE TABLE AlertsProjects (
	AlertName int NOT NULL,
	ProjectName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__AlertsPr__D6A05AAE97BEB359 PRIMARY KEY (ProjectName,AlertName),
	CONSTRAINT FK__AlertsPro__Alert__6A30C649 FOREIGN KEY (AlertName) REFERENCES Alerts(Id),
	CONSTRAINT FK__AlertsPro__TaskN__693CA210 FOREIGN KEY (ProjectName) REFERENCES Tasks(Name)
);


-- Table ResourceUsages 
DROP TABLE IF EXISTS ResourceUsages;
GO
CREATE TABLE ResourceUsages (
	ResourceName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	TaskName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProjectName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	StarTDate date NOT NULL,
	EndDate date NOT NULL,
	CONSTRAINT PK__Resource__1D8C28A781CA2121 PRIMARY KEY (ResourceName,TaskName,ProjectName,StarTDate),
	CONSTRAINT FK__ResourceU__Proje__5DCAEF64 FOREIGN KEY (ProjectName) REFERENCES Projects(Name),
	CONSTRAINT FK__ResourceU__Resou__5BE2A6F2 FOREIGN KEY (ResourceName) REFERENCES Resources(Name),
	CONSTRAINT FK__ResourceU__TaskN__5CD6CB2B FOREIGN KEY (TaskName) REFERENCES Tasks(Name)
);


-- Table ResourceUsagesComp 
DROP TABLE IF EXISTS ResourceUsagesComp;
GO
CREATE TABLE ResourceUsagesComp (
	ResourceName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	TaskName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProjectName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	StarTDate date NOT NULL,
	EndDate date NOT NULL,
	CONSTRAINT PK__Resource__E3B729972F8477B9 PRIMARY KEY (ProjectName,ResourceName,TaskName),
	CONSTRAINT FK__ResourceU__Proje__70DDC3D8 FOREIGN KEY (ProjectName) REFERENCES Projects(Name),
	CONSTRAINT FK__ResourceU__Resou__6EF57B66 FOREIGN KEY (ResourceName) REFERENCES Resources(Name),
	CONSTRAINT FK__ResourceU__TaskN__6FE99F9F FOREIGN KEY (TaskName) REFERENCES Tasks(Name)
);


-- Table TaskDependencies 
DROP TABLE IF EXISTS TaskDependencies;
GO
CREATE TABLE TaskDependencies (
	TaskName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DependsOnTaskName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__TaskDepe__C46C71FCF293FEBE PRIMARY KEY (TaskName,DependsOnTaskName),
	CONSTRAINT FK__TaskDepen__Depen__5535A963 FOREIGN KEY (DependsOnTaskName) REFERENCES Tasks(Name),
	CONSTRAINT FK__TaskDepen__TaskN__5441852A FOREIGN KEY (TaskName) REFERENCES Tasks(Name)
);


-- Table TaskResources 
DROP TABLE IF EXISTS TaskResources;
GO
CREATE TABLE TaskResources (
	TaskName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ResourceName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__TaskReso__C8120C5A9AEAF0DB PRIMARY KEY (TaskName,ResourceName),
	CONSTRAINT FK__TaskResou__Resou__59063A47 FOREIGN KEY (ResourceName) REFERENCES Resources(Name),
	CONSTRAINT FK__TaskResou__TaskN__5812160E FOREIGN KEY (TaskName) REFERENCES Tasks(Name)
);


-- Table TasksProjects 
DROP TABLE IF EXISTS TasksProjects;
GO
CREATE TABLE TasksProjects (
	ProjectName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	TaskName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__TasksPro__D5CEBF09730F910B PRIMARY KEY (TaskName,ProjectName),
	CONSTRAINT FK__TasksProj__Proje__66603565 FOREIGN KEY (ProjectName) REFERENCES Projects(Name),
	CONSTRAINT FK__TasksProj__TaskN__656C112C FOREIGN KEY (TaskName) REFERENCES Tasks(Name)
);


-- Table UserProjects 
DROP TABLE IF EXISTS UserProjects;
GO
CREATE TABLE UserProjects (
	Email nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProjectName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__UserProj__621AE2B436853300 PRIMARY KEY (Email,ProjectName),
	CONSTRAINT FK__UserProje__Email__47DBAE45 FOREIGN KEY (Email) REFERENCES Users(Email),
	CONSTRAINT FK__UserProje__Proje__48CFD27E FOREIGN KEY (ProjectName) REFERENCES Projects(Name)
);


-- Table UserTasks 
DROP TABLE IF EXISTS UserTasks;
GO
CREATE TABLE UserTasks (
	Email nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	TaskName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProjectName varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__UserTask__383150BD2724A258 PRIMARY KEY (Email,TaskName),
	CONSTRAINT FK__UserTasks__Email__440B1D61 FOREIGN KEY (Email) REFERENCES Users(Email),
	CONSTRAINT FK__UserTasks__TaskN__44FF419A FOREIGN KEY (TaskName) REFERENCES Tasks(Name)
);


-- Table UserTypes 
DROP TABLE IF EXISTS UserTypes;
GO
CREATE TABLE UserTypes (
	Email nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Type] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK__UserType__164A8F7D5410CB82 PRIMARY KEY (Email,[Type]),
	CONSTRAINT FK__UserTypes__Email__412EB0B6 FOREIGN KEY (Email) REFERENCES Users(Email)
);

--Insert Alerts
INSERT INTO Alerts (Id, Email, Message)
SELECT 1, 'admin@sistema.com', 'mssg3'
WHERE NOT EXISTS (
    SELECT 1
      FROM Alerts
     WHERE Id = 1
);

--Insert Users

INSERT INTO Users (Email, Name, Surname, Password, Birthdate, AdminSistem, [Login])
SELECT 'admin@sistema.com', 'Administrador', 'Sistema', '$Admin123', '2004-10-10', 1, 1
WHERE NOT EXISTS (
    SELECT 1
      FROM Users
     WHERE Email = 'admin@sistema.com'
);
GO