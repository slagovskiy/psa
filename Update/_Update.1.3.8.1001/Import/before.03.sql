ALTER TABLE dbo.inventory ADD
	exported bit NULL
;

ALTER TABLE dbo.inventory ADD CONSTRAINT
	DF_inventory_exported DEFAULT 0 FOR exported
;
