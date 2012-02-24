/**
 *	Drop all the tables
 */
DROP TABLE [VIDEO];
DROP TABLE [BOOK];
DROP TABLE [MEDIA];
DROP TABLE [PRODUCERDIRECTOR];
DROP TABLE [PERSON];
DROP TABLE [CODE];
DROP TABLE [CODETYPE];
DROP TABLE [AUTHOR];
DROP TABLE [USER];

/**
 *	Create Media Table
 */
CREATE TABLE [Media](
	[MediaID] [int] IDENTITY(1,1) NOT NULL,
	[ISBN] [nvarchar](13) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Publsiher] [nvarchar](40) NULL,
	[Pubdatetime] [datetime] NULL,
	[GenreID] [int] NULL,
	[FormatID] [int] NULL,
	[Aquisitiondatetime] [datetime] NULL,
	[IsBorrowable] [bit] NULL,
	[IsBorrowed] [bit] NULL,
	[BorrowerID] [int] NULL,
	[Price] [money] NULL,
	[NofStars] [decimal](18, 0) NULL,
	[Comment] [nvarchar](1000) NULL,
	[MediaDescription] [nvarchar](1000) NULL
);
GO

ALTER TABLE [Media] ADD CONSTRAINT [PK_MediaID] PRIMARY KEY ([MediaID]);
GO

/**
 *	Create Book Table
 */
CREATE TABLE [Book](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[LibraryLocation] [nvarchar](100) NULL
);
GO

ALTER TABLE [Book] ADD CONSTRAINT [PK_BookID] PRIMARY KEY ([BookID]);
GO

/**
 * 	Create Video Table
 */
CREATE TABLE [Video](
	[VideoID] [int] IDENTITY(1,1) NOT NULL,
	[MediaID] [int] NOT NULL,
	[datetimeRelased] [datetime] NULL,
	[BoardRatingID] [int] NULL
);
GO

ALTER TABLE [Video] ADD CONSTRAINT [PK_VideoID] PRIMARY KEY ([VideoID]);
GO

/**
 *	Create ProducerDirector Table
 */
CREATE TABLE [ProducerDirector](
	[ProducerDirectorID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[VideoID] [int] NOT NULL,
	[IsProducer] [bit] NOT NULL
);
GO

ALTER TABLE [ProducerDirector] ADD CONSTRAINT [PK_ProducerDirectorID] PRIMARY KEY ([ProducerDirectorID]);
GO

/**
 *	Create Code Table
 */
CREATE TABLE [Code](
	[CodeID] [int] IDENTITY(1,1) NOT NULL,
	[CodeDescription] [nvarchar](40) NOT NULL,
	[CodeTypeID] [int] NOT NULL
);
GO

ALTER TABLE [Code] ADD CONSTRAINT [PK_CodeID] PRIMARY KEY ([CodeID]);
GO

/**
 *	Create CodeType Table
 */
CREATE TABLE [CodeType](
	[CodeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CodeType] [nvarchar](40) NOT NULL
);
GO

ALTER TABLE [CodeType] ADD CONSTRAINT [PK_CodeTypeID] PRIMARY KEY ([CodeTypeID]);
GO

/**
 *	Create Person Table
 */
CREATE TABLE [Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](40) NOT NULL,
	[FirstName] [nvarchar](40) NULL,
	[Address] [nvarchar](100) NULL,
	[City] [nvarchar](40) NULL,
	[StateProvence] [nvarchar](40) NULL,
	[PostalCode] [nvarchar](40) NULL,
	[Country] [nvarchar](40) NULL
);
GO

ALTER TABLE [Person] ADD CONSTRAINT [PK_PersonID] PRIMARY KEY ([PersonID]);
GO

/**
 *	Create User Table
 */
CREATE TABLE [User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](15) NOT NULL,
	[PasswordHash] [binary](16) NOT NULL,
	[LastLogindatetime] [datetime] NULL
);
GO

ALTER TABLE [User] ADD CONSTRAINT [PK_UserID] PRIMARY KEY ([UserID]);
GO

/**
 *	Create Author Table
 */
CREATE TABLE [Author](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[BookId] [int] NOT NULL
);
GO

ALTER TABLE [Author] ADD CONSTRAINT [PK_AuthorID] PRIMARY KEY ([AuthorID]);
GO