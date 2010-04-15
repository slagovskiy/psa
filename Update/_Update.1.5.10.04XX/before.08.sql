CREATE NONCLUSTERED INDEX IX_order_6 ON dbo.[order]
	(
	id_order,
	status,
	expected_date,
	input_date
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
