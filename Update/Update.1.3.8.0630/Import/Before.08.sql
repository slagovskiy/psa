BEGIN TRANSACTION
;
ALTER TABLE dbo.good ADD
	bonustype nchar(250) NULL
;
ALTER TABLE dbo.good ADD CONSTRAINT
	DF_good_bonustype DEFAULT N'A' FOR bonustype
;
COMMIT
;