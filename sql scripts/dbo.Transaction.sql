USE [test_db]
GO

/****** Object:  Table [dbo].[Transaction]    Script Date: 26/06/2021 6:31:26 am ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transaction](
	[trans_id] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [varchar](51) NOT NULL,
	[Amount] [float] NOT NULL,
	[CurrencyCode] [varchar](4) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Status] [char](1) NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[trans_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

