CREATE TABLE [dbo].[mcounters](
	[id_mcounter] [int] IDENTITY(1,1) NOT NULL,
	[guid] [nchar](40) NULL CONSTRAINT [DF_mcounters_guid]  DEFAULT (newid()),
	[mcounter] [bigint] NULL,
	[date_mcounter] [datetime] NULL CONSTRAINT [DF_mcounters_name_mcounter]  DEFAULT (getdate()),
	[id_user] [int] NULL,
	[name_user] [nchar](255) NULL,
	[exported] [bit] NULL CONSTRAINT [DF_mcounters_exported]  DEFAULT ((0))
) ON [PRIMARY]
