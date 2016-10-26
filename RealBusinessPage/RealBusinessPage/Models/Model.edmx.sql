
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/25/2016 07:43:38
-- Generated from EDMX file: C:\Users\Charles\Source\Repos\ServerSide\RealBusinessPage\RealBusinessPage\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [serverside2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CLASSIFICATIONBOOK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOKSet] DROP CONSTRAINT [FK_CLASSIFICATIONBOOK];
GO
IF OBJECT_ID(N'[dbo].[FK_ISBN]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[COPYSet] DROP CONSTRAINT [FK_ISBN];
GO
IF OBJECT_ID(N'[dbo].[FK_BORROWERBORROW]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BORROWSet] DROP CONSTRAINT [FK_BORROWERBORROW];
GO
IF OBJECT_ID(N'[dbo].[FK_CATEGORYBORROWER]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BORROWERSet] DROP CONSTRAINT [FK_CATEGORYBORROWER];
GO
IF OBJECT_ID(N'[dbo].[FK_COPYBORROW]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BORROWSet] DROP CONSTRAINT [FK_COPYBORROW];
GO
IF OBJECT_ID(N'[dbo].[FK_statusId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[COPYSet] DROP CONSTRAINT [FK_statusId];
GO
IF OBJECT_ID(N'[dbo].[FK_AUTHORSetAUTHORBOOK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AUTHORBOOKSet] DROP CONSTRAINT [FK_AUTHORSetAUTHORBOOK];
GO
IF OBJECT_ID(N'[dbo].[FK_BOOKSetAUTHORBOOK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AUTHORBOOKSet] DROP CONSTRAINT [FK_BOOKSetAUTHORBOOK];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AUTHORSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AUTHORSet];
GO
IF OBJECT_ID(N'[dbo].[BOOKSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOKSet];
GO
IF OBJECT_ID(N'[dbo].[BORROWERSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BORROWERSet];
GO
IF OBJECT_ID(N'[dbo].[BORROWSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BORROWSet];
GO
IF OBJECT_ID(N'[dbo].[CATEGORYSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CATEGORYSet];
GO
IF OBJECT_ID(N'[dbo].[CLASSIFICATIONSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CLASSIFICATIONSet];
GO
IF OBJECT_ID(N'[dbo].[COPYSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[COPYSet];
GO
IF OBJECT_ID(N'[dbo].[STATUSSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[STATUSSet];
GO
IF OBJECT_ID(N'[dbo].[AUTHORBOOKSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AUTHORBOOKSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AUTHORSet'
CREATE TABLE [dbo].[AUTHORSet] (
    [AId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [BirthYear] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BOOKSet'
CREATE TABLE [dbo].[BOOKSet] (
    [ISBN] int IDENTITY(1,1) NOT NULL,
    [PublicationYear] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Pages] int  NOT NULL,
    [PublicationInfo] nvarchar(max)  NOT NULL,
    [CLASSIFICATIONSignId] int  NULL
);
GO

-- Creating table 'BORROWERSet'
CREATE TABLE [dbo].[BORROWERSet] (
    [PersonId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [TelNo] int  NOT NULL,
    [CATEGORYCategoryId] int  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Level] int  NOT NULL
);
GO

-- Creating table 'BORROWSet'
CREATE TABLE [dbo].[BORROWSet] (
    [BorrowDate] datetime  NOT NULL,
    [ToBeReturnedDate] datetime  NOT NULL,
    [ReturnDate] datetime  NOT NULL,
    [COPYBarcode] int  NOT NULL,
    [BORROWERPersonId] int  NOT NULL
);
GO

-- Creating table 'CATEGORYSet'
CREATE TABLE [dbo].[CATEGORYSet] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Period] int  NOT NULL,
    [PenaltyPerDay] int  NOT NULL
);
GO

-- Creating table 'CLASSIFICATIONSet'
CREATE TABLE [dbo].[CLASSIFICATIONSet] (
    [SignId] int IDENTITY(1,1) NOT NULL,
    [SignNum] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'COPYSet'
CREATE TABLE [dbo].[COPYSet] (
    [Barcode] int IDENTITY(1,1) NOT NULL,
    [Location] nvarchar(max)  NOT NULL,
    [Library] nvarchar(max)  NOT NULL,
    [STATUSStatusId] int  NULL,
    [BOOKISBN] int  NULL
);
GO

-- Creating table 'STATUSSet'
CREATE TABLE [dbo].[STATUSSet] (
    [StatusId] int IDENTITY(1,1) NOT NULL,
    [status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AUTHORBOOKSet'
CREATE TABLE [dbo].[AUTHORBOOKSet] (
    [AUTHORSetAId] int  NOT NULL,
    [BOOKSetISBN] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AId] in table 'AUTHORSet'
ALTER TABLE [dbo].[AUTHORSet]
ADD CONSTRAINT [PK_AUTHORSet]
    PRIMARY KEY CLUSTERED ([AId] ASC);
GO

-- Creating primary key on [ISBN] in table 'BOOKSet'
ALTER TABLE [dbo].[BOOKSet]
ADD CONSTRAINT [PK_BOOKSet]
    PRIMARY KEY CLUSTERED ([ISBN] ASC);
GO

-- Creating primary key on [PersonId] in table 'BORROWERSet'
ALTER TABLE [dbo].[BORROWERSet]
ADD CONSTRAINT [PK_BORROWERSet]
    PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO

-- Creating primary key on [BorrowDate] in table 'BORROWSet'
ALTER TABLE [dbo].[BORROWSet]
ADD CONSTRAINT [PK_BORROWSet]
    PRIMARY KEY CLUSTERED ([BorrowDate] ASC);
GO

-- Creating primary key on [CategoryId] in table 'CATEGORYSet'
ALTER TABLE [dbo].[CATEGORYSet]
ADD CONSTRAINT [PK_CATEGORYSet]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [SignId] in table 'CLASSIFICATIONSet'
ALTER TABLE [dbo].[CLASSIFICATIONSet]
ADD CONSTRAINT [PK_CLASSIFICATIONSet]
    PRIMARY KEY CLUSTERED ([SignId] ASC);
GO

-- Creating primary key on [Barcode] in table 'COPYSet'
ALTER TABLE [dbo].[COPYSet]
ADD CONSTRAINT [PK_COPYSet]
    PRIMARY KEY CLUSTERED ([Barcode] ASC);
GO

-- Creating primary key on [StatusId] in table 'STATUSSet'
ALTER TABLE [dbo].[STATUSSet]
ADD CONSTRAINT [PK_STATUSSet]
    PRIMARY KEY CLUSTERED ([StatusId] ASC);
GO

-- Creating primary key on [AUTHORSetAId], [BOOKSetISBN] in table 'AUTHORBOOKSet'
ALTER TABLE [dbo].[AUTHORBOOKSet]
ADD CONSTRAINT [PK_AUTHORBOOKSet]
    PRIMARY KEY CLUSTERED ([AUTHORSetAId], [BOOKSetISBN] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CLASSIFICATIONSignId] in table 'BOOKSet'
ALTER TABLE [dbo].[BOOKSet]
ADD CONSTRAINT [FK_CLASSIFICATIONBOOK]
    FOREIGN KEY ([CLASSIFICATIONSignId])
    REFERENCES [dbo].[CLASSIFICATIONSet]
        ([SignId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CLASSIFICATIONBOOK'
CREATE INDEX [IX_FK_CLASSIFICATIONBOOK]
ON [dbo].[BOOKSet]
    ([CLASSIFICATIONSignId]);
GO

-- Creating foreign key on [BOOKISBN] in table 'COPYSet'
ALTER TABLE [dbo].[COPYSet]
ADD CONSTRAINT [FK_ISBN]
    FOREIGN KEY ([BOOKISBN])
    REFERENCES [dbo].[BOOKSet]
        ([ISBN])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ISBN'
CREATE INDEX [IX_FK_ISBN]
ON [dbo].[COPYSet]
    ([BOOKISBN]);
GO

-- Creating foreign key on [BORROWERPersonId] in table 'BORROWSet'
ALTER TABLE [dbo].[BORROWSet]
ADD CONSTRAINT [FK_BORROWERBORROW]
    FOREIGN KEY ([BORROWERPersonId])
    REFERENCES [dbo].[BORROWERSet]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BORROWERBORROW'
CREATE INDEX [IX_FK_BORROWERBORROW]
ON [dbo].[BORROWSet]
    ([BORROWERPersonId]);
GO

-- Creating foreign key on [CATEGORYCategoryId] in table 'BORROWERSet'
ALTER TABLE [dbo].[BORROWERSet]
ADD CONSTRAINT [FK_CATEGORYBORROWER]
    FOREIGN KEY ([CATEGORYCategoryId])
    REFERENCES [dbo].[CATEGORYSet]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CATEGORYBORROWER'
CREATE INDEX [IX_FK_CATEGORYBORROWER]
ON [dbo].[BORROWERSet]
    ([CATEGORYCategoryId]);
GO

-- Creating foreign key on [COPYBarcode] in table 'BORROWSet'
ALTER TABLE [dbo].[BORROWSet]
ADD CONSTRAINT [FK_COPYBORROW]
    FOREIGN KEY ([COPYBarcode])
    REFERENCES [dbo].[COPYSet]
        ([Barcode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_COPYBORROW'
CREATE INDEX [IX_FK_COPYBORROW]
ON [dbo].[BORROWSet]
    ([COPYBarcode]);
GO

-- Creating foreign key on [STATUSStatusId] in table 'COPYSet'
ALTER TABLE [dbo].[COPYSet]
ADD CONSTRAINT [FK_statusId]
    FOREIGN KEY ([STATUSStatusId])
    REFERENCES [dbo].[STATUSSet]
        ([StatusId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_statusId'
CREATE INDEX [IX_FK_statusId]
ON [dbo].[COPYSet]
    ([STATUSStatusId]);
GO

-- Creating foreign key on [AUTHORSetAId] in table 'AUTHORBOOKSet'
ALTER TABLE [dbo].[AUTHORBOOKSet]
ADD CONSTRAINT [FK_AUTHORSetAUTHORBOOK]
    FOREIGN KEY ([AUTHORSetAId])
    REFERENCES [dbo].[AUTHORSet]
        ([AId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BOOKSetISBN] in table 'AUTHORBOOKSet'
ALTER TABLE [dbo].[AUTHORBOOKSet]
ADD CONSTRAINT [FK_BOOKSetAUTHORBOOK]
    FOREIGN KEY ([BOOKSetISBN])
    REFERENCES [dbo].[BOOKSet]
        ([ISBN])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BOOKSetAUTHORBOOK'
CREATE INDEX [IX_FK_BOOKSetAUTHORBOOK]
ON [dbo].[AUTHORBOOKSet]
    ([BOOKSetISBN]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------