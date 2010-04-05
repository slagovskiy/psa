CREATE NONCLUSTERED INDEX IX_price ON dbo.price
	(
	id_good,
	id_category,
	threshold,
	ondate
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
