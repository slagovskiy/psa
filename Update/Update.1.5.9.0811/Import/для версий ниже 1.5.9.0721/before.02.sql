CREATE TABLE [dbo].[kiosk_orders_ok](
	[id_kiosk_order] [int] IDENTITY(1,1) NOT NULL,
	[number] [nchar](20) NULL DEFAULT (N''),
	[status] [int] NULL,
	[dateok] [datetime] NULL DEFAULT (getdate()),
	[exported] [bit] NULL DEFAULT ((0))
) ON [PRIMARY]
