ALTER TABLE dbo.inventory
	DROP CONSTRAINT DF_inventory_exported
;

ALTER TABLE dbo.inventory
	DROP COLUMN exported
;