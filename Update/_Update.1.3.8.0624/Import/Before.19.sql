BEGIN TRANSACTION
;
CREATE TABLE dbo.defect
	(
	defect_code int NULL,
	defect_name nchar(250) NULL
	)  ON [PRIMARY]
;
COMMIT
;