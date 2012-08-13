BEGIN TRANSACTION
;
ALTER TABLE dbo.dcarduse ADD CONSTRAINT
	DF_dcarduse_lastdate DEFAULT getdate() FOR lastdate
;
ALTER TABLE dbo.dcarduse ADD CONSTRAINT
	DF_dcarduse_cnt DEFAULT 0 FOR cnt
;
COMMIT
;