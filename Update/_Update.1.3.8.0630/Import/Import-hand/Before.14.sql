BEGIN TRANSACTION
;
CREATE TABLE dbo.dcarduse
	(
	code nchar(15) NULL,
	lastdate datetime NULL,
	cnt int NULL
	)  ON [PRIMARY]
;
COMMIT
;