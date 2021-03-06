USE [OMAM]
GO
/****** Object:  Table [dbo].[OffShoreFundPrices]    Script Date: 02/22/2008 20:24:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OffShoreFundPrices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OMAMFundId] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_OffShoreFundPrices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
