CREATE TABLE [dbo].[kiosk](
	[id_kiosk] [int] IDENTITY(1,1) NOT NULL,
	[guid] [nchar](40) NULL CONSTRAINT [DF_kiosk_guid]  DEFAULT (newid()),
	[del] [bit] NULL CONSTRAINT [DF_kiosk_del]  DEFAULT ((0)),
	[name] [nchar](1024) NULL,
	[path] [nchar](1024) NULL,
	[code] [int] NULL,
 CONSTRAINT [PK_kiosk] PRIMARY KEY CLUSTERED 
(
	[id_kiosk] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]