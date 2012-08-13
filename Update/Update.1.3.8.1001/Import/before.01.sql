ALTER TABLE dbo.inventory
	DROP CONSTRAINT DF_inventory_expotred
;

ALTER TABLE dbo.inventory
	DROP COLUMN exported
;