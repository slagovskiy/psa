CREATE NONCLUSTERED INDEX IX_order_5 ON dbo.[order]
	(
	id_client
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]