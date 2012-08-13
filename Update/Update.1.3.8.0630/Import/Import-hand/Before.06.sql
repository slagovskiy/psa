BEGIN TRANSACTION
;
ALTER TABLE dbo.dcard ADD
	typebonus nchar(1) NULL
;
ALTER TABLE dbo.dcard ADD CONSTRAINT
	DF_dcard_typebonus DEFAULT N'A' FOR typebonus
;
COMMIT
;