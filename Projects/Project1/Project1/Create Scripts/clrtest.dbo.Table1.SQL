/****** Object:  Table [dbo].[Table1]    Script Date: 03/25/2006 17:28:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Table1]') AND type in (N'U'))
DROP TABLE [dbo].[Table1]
GO
/****** Object:  Table [dbo].[Table1]    Script Date: 03/25/2006 17:28:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Table1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Table1](
	[blah] [ntext] NULL,
	[b] [nchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
