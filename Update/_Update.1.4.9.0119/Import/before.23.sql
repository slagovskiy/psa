CREATE TABLE [dbo].[orderevent](
	[id_orderevent] [int] IDENTITY(1,1) NOT NULL,
	[del] [bit] NULL CONSTRAINT [DF_orderevent_del]  DEFAULT ((0)),
	[guid] [nchar](40) NULL CONSTRAINT [DF_orderevent_guid]  DEFAULT (newid()),
	[id_order] [int] NULL,
	[event_date] [datetime] NULL CONSTRAINT [DF_orderevent_event_date]  DEFAULT (getdate()),
	[event_user] [nchar](255) NULL,
	[event_status] [nchar](10) NULL,
	[event_point] [nchar](10) NULL,
	[event_text] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]