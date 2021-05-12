
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/24/2018 17:06:56
-- Generated from EDMX file: C:\Users\yi.wei\Desktop\MVC\RollCall\RollCall.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RollCallName];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Staff]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Staff];
GO
IF OBJECT_ID(N'[dbo].[LienNumber]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LienNumber];
GO
IF OBJECT_ID(N'[dbo].[Attendance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attendance];
GO
IF OBJECT_ID(N'[dbo].[WorkOvertime]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkOvertime];
GO
IF OBJECT_ID(N'[dbo].[PhoneUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhoneUsers];
GO
IF OBJECT_ID(N'[dbo].[SpecialCase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecialCase];
GO
IF OBJECT_ID(N'[dbo].[StaffXXX]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffXXX];
GO
IF OBJECT_ID(N'[dbo].[Sys_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sys_Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Staff'
CREATE TABLE [dbo].[Staff] (
    [ID] varchar(20)  NOT NULL,
    [NAME] varchar(20)  NOT NULL,
    [BU] varchar(20)  NOT NULL,
    [BUID] varchar(20)  NOT NULL,
    [POSITION] varchar(4)  NOT NULL,
    [LINENAME] varchar(10)  NULL,
    [CLASS] varchar(4)  NOT NULL,
    [date1] datetime  NULL,
    [date2] datetime  NULL
);
GO

-- Creating table 'LienNumber'
CREATE TABLE [dbo].[LienNumber] (
    [type1] varchar(20)  NULL,
    [linename] varchar(20)  NULL,
    [OrderbBy] int  NULL,
    [id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Attendance'
CREATE TABLE [dbo].[Attendance] (
    [ID] varchar(20)  NULL,
    [NAME] varchar(20)  NOT NULL,
    [BU] varchar(20)  NOT NULL,
    [LINENAME] varchar(4)  NOT NULL,
    [CLASS] varchar(4)  NOT NULL,
    [state1] varchar(20)  NOT NULL,
    [reason1] varchar(20)  NULL,
    [note1] varchar(200)  NULL,
    [state2] varchar(20)  NULL,
    [reason2] varchar(20)  NULL,
    [note2] varchar(200)  NULL,
    [date1] datetime  NOT NULL,
    [date2] datetime  NULL,
    [time1] float  NULL,
    [keyid] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'WorkOvertime'
CREATE TABLE [dbo].[WorkOvertime] (
    [ID] varchar(20)  NULL,
    [NAME] varchar(20)  NOT NULL,
    [BU] varchar(20)  NOT NULL,
    [LINENAME] varchar(20)  NOT NULL,
    [CLASS] varchar(20)  NOT NULL,
    [time1] float  NOT NULL,
    [date1] datetime  NOT NULL,
    [date2] datetime  NOT NULL,
    [keyid] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'PhoneUsers'
CREATE TABLE [dbo].[PhoneUsers] (
    [ID] varchar(20)  NOT NULL,
    [PASSWORD1] varchar(20)  NOT NULL,
    [power1] varchar(4)  NOT NULL
);
GO

-- Creating table 'SpecialCase'
CREATE TABLE [dbo].[SpecialCase] (
    [ID] char(20)  NULL,
    [NAME] char(20)  NULL,
    [BU] char(20)  NULL,
    [BUID] char(20)  NULL,
    [LINENAME] char(20)  NULL,
    [CODE] char(1)  NULL,
    [note] char(50)  NULL,
    [indexx] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'StaffXXX'
CREATE TABLE [dbo].[StaffXXX] (
    [ID] varchar(20)  NULL,
    [NAME] varchar(20)  NOT NULL,
    [BU] varchar(20)  NOT NULL,
    [BUID] varchar(20)  NOT NULL,
    [POSITION] varchar(4)  NOT NULL,
    [LINENAME] varchar(3)  NOT NULL,
    [CLASS] varchar(4)  NOT NULL,
    [date1] datetime  NULL,
    [date2] datetime  NULL
);
GO

-- Creating table 'Sys_Users'
CREATE TABLE [dbo].[Sys_Users] (
    [Emp_no] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NULL,
    [Department] nvarchar(50)  NULL,
    [Curr_time] datetime  NULL,
    [State] int  NULL,
    [Name] nvarchar(50)  NULL,
    [Item1] nvarchar(50)  NULL,
    [Item2] nvarchar(50)  NULL,
    [Item3] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Staff'
ALTER TABLE [dbo].[Staff]
ADD CONSTRAINT [PK_Staff]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [id] in table 'LienNumber'
ALTER TABLE [dbo].[LienNumber]
ADD CONSTRAINT [PK_LienNumber]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [keyid] in table 'Attendance'
ALTER TABLE [dbo].[Attendance]
ADD CONSTRAINT [PK_Attendance]
    PRIMARY KEY CLUSTERED ([keyid] ASC);
GO

-- Creating primary key on [keyid] in table 'WorkOvertime'
ALTER TABLE [dbo].[WorkOvertime]
ADD CONSTRAINT [PK_WorkOvertime]
    PRIMARY KEY CLUSTERED ([keyid] ASC);
GO

-- Creating primary key on [ID] in table 'PhoneUsers'
ALTER TABLE [dbo].[PhoneUsers]
ADD CONSTRAINT [PK_PhoneUsers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [indexx] in table 'SpecialCase'
ALTER TABLE [dbo].[SpecialCase]
ADD CONSTRAINT [PK_SpecialCase]
    PRIMARY KEY CLUSTERED ([indexx] ASC);
GO

-- Creating primary key on [NAME], [BU], [BUID], [POSITION], [LINENAME], [CLASS] in table 'StaffXXX'
ALTER TABLE [dbo].[StaffXXX]
ADD CONSTRAINT [PK_StaffXXX]
    PRIMARY KEY CLUSTERED ([NAME], [BU], [BUID], [POSITION], [LINENAME], [CLASS] ASC);
GO

-- Creating primary key on [Emp_no] in table 'Sys_Users'
ALTER TABLE [dbo].[Sys_Users]
ADD CONSTRAINT [PK_Sys_Users]
    PRIMARY KEY CLUSTERED ([Emp_no] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------