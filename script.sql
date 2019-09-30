CREATE DATABASE Test
GO
USE [Test]
GO

/****** Object:  Table [dbo].[User]    Script Date: 30/09/2019 4:26:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
[Id] [int] identity NOT NULL,
[Name] [varchar](max) NULL,
[LastName] [varchar](max) NULL,
[Address] [varchar](max) NULL,
[CreateDate] [datetime] NULL,
[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO