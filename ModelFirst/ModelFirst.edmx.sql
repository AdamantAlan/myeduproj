
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/30/2021 16:57:59
-- Generated from EDMX file: W:\projects\ModelFirst\ModelFirst.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ForModel];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PersonalSet'
CREATE TABLE [dbo].[PersonalSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [PersonalInfo_Id] int  NOT NULL
);
GO

-- Creating table 'PersonalInfoSet'
CREATE TABLE [dbo].[PersonalInfoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Location] nvarchar(max)  NOT NULL,
    [Role] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PersonalSet'
ALTER TABLE [dbo].[PersonalSet]
ADD CONSTRAINT [PK_PersonalSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonalInfoSet'
ALTER TABLE [dbo].[PersonalInfoSet]
ADD CONSTRAINT [PK_PersonalInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PersonalInfo_Id] in table 'PersonalSet'
ALTER TABLE [dbo].[PersonalSet]
ADD CONSTRAINT [FK_PersonalPersonalInfo]
    FOREIGN KEY ([PersonalInfo_Id])
    REFERENCES [dbo].[PersonalInfoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonalPersonalInfo'
CREATE INDEX [IX_FK_PersonalPersonalInfo]
ON [dbo].[PersonalSet]
    ([PersonalInfo_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------