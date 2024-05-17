USE [TestMatchProfiler]
GO

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Author]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Author] (
        [idAuthor] INT           IDENTITY (1, 1) NOT NULL,
        [Describe]     VARCHAR (254) NOT NULL,
        CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED ([idAuthor] ASC)
    )
END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[LegalEntity]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[LegalEntity] (
        [idLegalEntity] INT           NOT NULL,
        [Describe]      VARCHAR (254) NULL,
        CONSTRAINT [PK_LegalEntity] PRIMARY KEY CLUSTERED ([idLegalEntity] ASC)
    )
END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[LegalContract]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[LegalContract] (
        [IdProcess]           INT            IDENTITY (1, 1) NOT NULL,
        [Author]              INT            NOT NULL,
        [LegalEntity]         INT            NOT NULL,
        [DescribeLegalEntity] NVARCHAR (MAX) NULL,
        [CreatedProcess]      INT            NULL,
        [UpdatedProcess]      INT            NULL,
        CONSTRAINT [PK_LegalContract] PRIMARY KEY CLUSTERED ([IdProcess] ASC),
        CONSTRAINT [FK_LegalContract_LegalEntity] FOREIGN KEY ([LegalEntity]) REFERENCES [dbo].[LegalEntity] ([idLegalEntity]),
        CONSTRAINT [FK_LegalContract_Author] FOREIGN KEY ([Author]) REFERENCES [dbo].[Author] ([idAuthor])    
    )
END