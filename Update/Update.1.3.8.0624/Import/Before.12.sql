BEGIN TRANSACTION
;
ALTER TABLE dbo.[order] ADD
	bonus decimal(18, 2) NULL
;
ALTER TABLE dbo.[order] ADD CONSTRAINT
	DF_order_bonus DEFAULT 0 FOR bonus
;
COMMIT
;