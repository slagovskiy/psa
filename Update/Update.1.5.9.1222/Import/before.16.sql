CREATE NONCLUSTERED INDEX IX_orderbody_2 ON dbo.orderbody
	(
	tech_defect
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
