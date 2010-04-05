CREATE TABLE [dbo].[inventory](
	[id_inventory] [int] IDENTITY(1,1) NOT NULL,
	[del] [bit] NULL CONSTRAINT [DF_inventory_del]  DEFAULT ((0)),
	[guid] [nchar](40) NULL CONSTRAINT [DF_inventory_guid]  DEFAULT (newid()),
	[inventory_date] [datetime] NULL CONSTRAINT [DF_inventory_inventory_date]  DEFAULT (getdate()),
	[inventory_user] [int] NULL,
	[exported] [bit] NULL CONSTRAINT [DF_inventory_expotred]  DEFAULT ((0)),
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[id_inventory] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
