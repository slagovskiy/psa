BEGIN TRANSACTION
;
ALTER TABLE dbo.dcard ADD
	bonus decimal(18, 2) NULL
;
ALTER TABLE dbo.dcard ADD CONSTRAINT
	DF_dcard_bonus DEFAULT 0 FOR bonus
;
COMMIT
;