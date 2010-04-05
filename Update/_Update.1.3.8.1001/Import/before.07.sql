ALTER TABLE dbo.inventorybody ADD
	exported bit NULL
;

ALTER TABLE dbo.inventorybody ADD CONSTRAINT
	DF_inventorybody_exported DEFAULT 0 FOR exported
;
