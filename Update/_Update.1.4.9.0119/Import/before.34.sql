CREATE TABLE [dbo].[verification](
	[id_verification] [int] IDENTITY(1,1) NOT NULL,
	[del] [bit] NULL CONSTRAINT [DF_verification_del]  DEFAULT ((0)),
	[guid] [nchar](40) NULL CONSTRAINT [DF_verification_guid]  DEFAULT (newid()),
	[verification_date] [datetime] NULL CONSTRAINT [DF_verification_verification_date]  DEFAULT (getdate()),
	[verification_user] [int] NULL,
	[exported] [bit] NULL CONSTRAINT [DF_verification_exported]  DEFAULT ((0)),
 CONSTRAINT [PK_verification] PRIMARY KEY CLUSTERED 
(
	[id_verification] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
