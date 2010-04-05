CREATE TABLE [dbo].[counters](
	[id_counter] [int] IDENTITY(1,1) NOT NULL,
	[guid] [nchar](40) NULL CONSTRAINT [DF_counters_guid]  DEFAULT (newid()),
	[id_mashine] [nchar](30) NULL,
	[id_user] [int] NULL,
	[name_user] [nchar](250) NULL,
	[c1] [bigint] NULL,
	[c1date] [datetime] NULL,
	[c2] [bigint] NULL,
	[c2date] [datetime] NULL,
	[c0] [int] NULL,
	[c00] [int] NULL,
	[exported] [bit] NULL CONSTRAINT [DF_counters_exported]  DEFAULT ((0))
) ON [PRIMARY]
