CREATE NONCLUSTERED INDEX IX_orderevent ON dbo.orderevent
	(
	id_order
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
