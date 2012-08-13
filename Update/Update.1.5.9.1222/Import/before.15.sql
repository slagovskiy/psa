CREATE NONCLUSTERED INDEX IX_orderbody_1 ON dbo.orderbody
	(
	id_good,
	id_mashine,
	id_material,
	id_order
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
