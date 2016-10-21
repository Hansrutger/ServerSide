
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/21/2016 11:18:06
-- Generated from EDMX file: C:\Users\Charles\Source\Repos\ServerSide\RealBusinessPage\RealBusinessPage\LibModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBLibrary];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BOOK_AUTHOR_AUTHOR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_AUTHOR] DROP CONSTRAINT [FK_BOOK_AUTHOR_AUTHOR];
GO
IF OBJECT_ID(N'[dbo].[FK_BOOK_AUTHOR_BOOK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK_AUTHOR] DROP CONSTRAINT [FK_BOOK_AUTHOR_BOOK];
GO
IF OBJECT_ID(N'[dbo].[FK_BOOK_CLASSIFICATION]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOK] DROP CONSTRAINT [FK_BOOK_CLASSIFICATION];
GO
IF OBJECT_ID(N'[dbo].[FK_BORROW_BORROWER]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BORROW] DROP CONSTRAINT [FK_BORROW_BORROWER];
GO
IF OBJECT_ID(N'[dbo].[FK_BORROW_COPY]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BORROW] DROP CONSTRAINT [FK_BORROW_COPY];
GO
IF OBJECT_ID(N'[dbo].[FK_BORROWER_CATEGORY]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BORROWER] DROP CONSTRAINT [FK_BORROWER_CATEGORY];
GO
IF OBJECT_ID(N'[dbo].[FK_COPY_BOOK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[COPY] DROP CONSTRAINT [FK_COPY_BOOK];
GO
IF OBJECT_ID(N'[dbo].[FK_COPY_STATUS]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[COPY] DROP CONSTRAINT [FK_COPY_STATUS];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AUTHOR]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AUTHOR];
GO
IF OBJECT_ID(N'[dbo].[BOOK]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOK];
GO
IF OBJECT_ID(N'[dbo].[BOOK_AUTHOR]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOK_AUTHOR];
GO
IF OBJECT_ID(N'[dbo].[BORROW]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BORROW];
GO
IF OBJECT_ID(N'[dbo].[BORROWER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BORROWER];
GO
IF OBJECT_ID(N'[dbo].[CATEGORY]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CATEGORY];
GO
IF OBJECT_ID(N'[dbo].[CLASSIFICATION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CLASSIFICATION];
GO
IF OBJECT_ID(N'[dbo].[COPY]', 'U') IS NOT NULL
    DROP TABLE [dbo].[COPY];
GO
IF OBJECT_ID(N'[dbo].[STATUS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[STATUS];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AUTHORs'
CREATE TABLE [dbo].[AUTHORs] (
    [Aid] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [BirthYear] nvarchar(10)  NULL
);
GO

-- Creating table 'BOOKs'
CREATE TABLE [dbo].[BOOKs] (
    [ISBN] nvarchar(15)  NOT NULL,
    [Title] nvarchar(255)  NULL,
    [SignId] int  NULL,
    [PublicationYear] nvarchar(10)  NULL,
    [publicationinfo] nvarchar(255)  NULL,
    [pages] smallint  NULL
);
GO

-- Creating table 'BORROWs'
CREATE TABLE [dbo].[BORROWs] (
    [Barcode] nvarchar(20)  NOT NULL,
    [PersonId] nvarchar(13)  NOT NULL,
    [BorrowDate] datetime  NOT NULL,
    [ToBeReturnedDate] datetime  NULL,
    [ReturnDate] datetime  NULL
);
GO

-- Creating table 'BORROWERs'
CREATE TABLE [dbo].[BORROWERs] (
    [PersonId] nvarchar(13)  NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Address] nvarchar(50)  NULL,
    [Telno] nvarchar(50)  NULL,
    [CategoryId] int  NULL
);
GO

-- Creating table 'CATEGORies'
CREATE TABLE [dbo].[CATEGORies] (
    [CatergoryId] int  NOT NULL,
    [Category1] nvarchar(50)  NULL,
    [Period] smallint  NULL,
    [Penaltyperday] int  NULL
);
GO

-- Creating table 'CLASSIFICATIONs'
CREATE TABLE [dbo].[CLASSIFICATIONs] (
    [SignId] int  NOT NULL,
    [Signum] nvarchar(50)  NULL,
    [Description] nvarchar(255)  NULL
);
GO

-- Creating table 'COPies'
CREATE TABLE [dbo].[COPies] (
    [Barcode] nvarchar(20)  NOT NULL,
    [Location] nvarchar(50)  NULL,
    [StatusId] int  NULL,
    [ISBN] nvarchar(15)  NULL,
    [library] nvarchar(50)  NULL
);
GO

-- Creating table 'STATUS'
CREATE TABLE [dbo].[STATUS] (
    [statusid] int  NOT NULL,
    [status1] nvarchar(50)  NULL
);
GO

-- Creating table 'ACCOUNTs'
CREATE TABLE [dbo].[ACCOUNTs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Level] smallint  NOT NULL,
    [BORROWER_PersonId] nvarchar(13)  NOT NULL
);
GO

-- Creating table 'BOOK_AUTHOR'
CREATE TABLE [dbo].[BOOK_AUTHOR] (
    [AUTHORs_Aid] int  NOT NULL,
    [BOOKs_ISBN] nvarchar(15)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Aid] in table 'AUTHORs'
ALTER TABLE [dbo].[AUTHORs]
ADD CONSTRAINT [PK_AUTHORs]
    PRIMARY KEY CLUSTERED ([Aid] ASC);
GO

-- Creating primary key on [ISBN] in table 'BOOKs'
ALTER TABLE [dbo].[BOOKs]
ADD CONSTRAINT [PK_BOOKs]
    PRIMARY KEY CLUSTERED ([ISBN] ASC);
GO

-- Creating primary key on [Barcode], [PersonId], [BorrowDate] in table 'BORROWs'
ALTER TABLE [dbo].[BORROWs]
ADD CONSTRAINT [PK_BORROWs]
    PRIMARY KEY CLUSTERED ([Barcode], [PersonId], [BorrowDate] ASC);
GO

-- Creating primary key on [PersonId] in table 'BORROWERs'
ALTER TABLE [dbo].[BORROWERs]
ADD CONSTRAINT [PK_BORROWERs]
    PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO

-- Creating primary key on [CatergoryId] in table 'CATEGORies'
ALTER TABLE [dbo].[CATEGORies]
ADD CONSTRAINT [PK_CATEGORies]
    PRIMARY KEY CLUSTERED ([CatergoryId] ASC);
GO

-- Creating primary key on [SignId] in table 'CLASSIFICATIONs'
ALTER TABLE [dbo].[CLASSIFICATIONs]
ADD CONSTRAINT [PK_CLASSIFICATIONs]
    PRIMARY KEY CLUSTERED ([SignId] ASC);
GO

-- Creating primary key on [Barcode] in table 'COPies'
ALTER TABLE [dbo].[COPies]
ADD CONSTRAINT [PK_COPies]
    PRIMARY KEY CLUSTERED ([Barcode] ASC);
GO

-- Creating primary key on [statusid] in table 'STATUS'
ALTER TABLE [dbo].[STATUS]
ADD CONSTRAINT [PK_STATUS]
    PRIMARY KEY CLUSTERED ([statusid] ASC);
GO

-- Creating primary key on [Id] in table 'ACCOUNTs'
ALTER TABLE [dbo].[ACCOUNTs]
ADD CONSTRAINT [PK_ACCOUNTs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AUTHORs_Aid], [BOOKs_ISBN] in table 'BOOK_AUTHOR'
ALTER TABLE [dbo].[BOOK_AUTHOR]
ADD CONSTRAINT [PK_BOOK_AUTHOR]
    PRIMARY KEY CLUSTERED ([AUTHORs_Aid], [BOOKs_ISBN] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SignId] in table 'BOOKs'
ALTER TABLE [dbo].[BOOKs]
ADD CONSTRAINT [FK_BOOK_CLASSIFICATION]
    FOREIGN KEY ([SignId])
    REFERENCES [dbo].[CLASSIFICATIONs]
        ([SignId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BOOK_CLASSIFICATION'
CREATE INDEX [IX_FK_BOOK_CLASSIFICATION]
ON [dbo].[BOOKs]
    ([SignId]);
GO

-- Creating foreign key on [ISBN] in table 'COPies'
ALTER TABLE [dbo].[COPies]
ADD CONSTRAINT [FK_COPY_BOOK]
    FOREIGN KEY ([ISBN])
    REFERENCES [dbo].[BOOKs]
        ([ISBN])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_COPY_BOOK'
CREATE INDEX [IX_FK_COPY_BOOK]
ON [dbo].[COPies]
    ([ISBN]);
GO

-- Creating foreign key on [PersonId] in table 'BORROWs'
ALTER TABLE [dbo].[BORROWs]
ADD CONSTRAINT [FK_BORROW_BORROWER]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[BORROWERs]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BORROW_BORROWER'
CREATE INDEX [IX_FK_BORROW_BORROWER]
ON [dbo].[BORROWs]
    ([PersonId]);
GO

-- Creating foreign key on [Barcode] in table 'BORROWs'
ALTER TABLE [dbo].[BORROWs]
ADD CONSTRAINT [FK_BORROW_COPY]
    FOREIGN KEY ([Barcode])
    REFERENCES [dbo].[COPies]
        ([Barcode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CategoryId] in table 'BORROWERs'
ALTER TABLE [dbo].[BORROWERs]
ADD CONSTRAINT [FK_BORROWER_CATEGORY]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[CATEGORies]
        ([CatergoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BORROWER_CATEGORY'
CREATE INDEX [IX_FK_BORROWER_CATEGORY]
ON [dbo].[BORROWERs]
    ([CategoryId]);
GO

-- Creating foreign key on [StatusId] in table 'COPies'
ALTER TABLE [dbo].[COPies]
ADD CONSTRAINT [FK_COPY_STATUS]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[STATUS]
        ([statusid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_COPY_STATUS'
CREATE INDEX [IX_FK_COPY_STATUS]
ON [dbo].[COPies]
    ([StatusId]);
GO

-- Creating foreign key on [AUTHORs_Aid] in table 'BOOK_AUTHOR'
ALTER TABLE [dbo].[BOOK_AUTHOR]
ADD CONSTRAINT [FK_BOOK_AUTHOR_AUTHOR]
    FOREIGN KEY ([AUTHORs_Aid])
    REFERENCES [dbo].[AUTHORs]
        ([Aid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BOOKs_ISBN] in table 'BOOK_AUTHOR'
ALTER TABLE [dbo].[BOOK_AUTHOR]
ADD CONSTRAINT [FK_BOOK_AUTHOR_BOOK]
    FOREIGN KEY ([BOOKs_ISBN])
    REFERENCES [dbo].[BOOKs]
        ([ISBN])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BOOK_AUTHOR_BOOK'
CREATE INDEX [IX_FK_BOOK_AUTHOR_BOOK]
ON [dbo].[BOOK_AUTHOR]
    ([BOOKs_ISBN]);
GO

-- Creating foreign key on [BORROWER_PersonId] in table 'ACCOUNTs'
ALTER TABLE [dbo].[ACCOUNTs]
ADD CONSTRAINT [FK_ACCOUNTBORROWER]
    FOREIGN KEY ([BORROWER_PersonId])
    REFERENCES [dbo].[BORROWERs]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACCOUNTBORROWER'
CREATE INDEX [IX_FK_ACCOUNTBORROWER]
ON [dbo].[ACCOUNTs]
    ([BORROWER_PersonId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------