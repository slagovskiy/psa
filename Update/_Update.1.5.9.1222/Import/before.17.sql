CREATE NONCLUSTERED INDEX IX_orderbody_3 ON dbo.orderbody
	(
	id_good,
	datework,
	id_user_work,
	name_work
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
