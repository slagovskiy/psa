BEGIN TRANSACTION
;
ALTER TABLE dbo.category DROP CONSTRAINT DF_category_guid
;
ALTER TABLE dbo.category DROP CONSTRAINT DF_category_del
;
ALTER TABLE dbo.category DROP CONSTRAINT DF_category_input
;
CREATE TABLE dbo.Tmp_category
	(
	id_category int NOT NULL,
	guid nchar(40) NULL,
	del smallint NULL,
	name nchar(255) NULL,
	input int NULL
	)  ON [PRIMARY]
;
ALTER TABLE dbo.Tmp_category ADD CONSTRAINT DF_category_guid DEFAULT (newid()) FOR guid
;
ALTER TABLE dbo.Tmp_category ADD CONSTRAINT DF_category_del DEFAULT ((0)) FOR del
;
ALTER TABLE dbo.Tmp_category ADD CONSTRAINT DF_category_input DEFAULT ((0)) FOR input
;
DROP TABLE dbo.category
;
EXECUTE sp_rename N'dbo.Tmp_category', N'category', 'OBJECT' 
;
COMMIT
;
