CREATE NONCLUSTERED INDEX IX_orderbody_4 ON dbo.orderbody
	(
	id_orderbody,
	id_good,
	id_mashine,
	id_material,
	id_order,
	tech_defect,
	datework,
	id_user_work
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
