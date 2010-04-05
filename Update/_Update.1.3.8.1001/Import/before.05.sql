ALTER TABLE dbo.inventorybody
	DROP CONSTRAINT DF_inventorybody_exported
;

ALTER TABLE dbo.inventorybody
	DROP COLUMN exported
;
