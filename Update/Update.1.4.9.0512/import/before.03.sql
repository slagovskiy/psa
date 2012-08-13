BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
;
BEGIN TRANSACTION
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_id_order
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_id_mashine
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_id_material
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_id_good
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_guid
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_del
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_quantity
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_actual_quantity
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_sign
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_price
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_dateadd
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_id_user_work
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_name_work
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_defect_quantity
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_id_user_defect
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_user_defect
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_tech_defect
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_exported
;
ALTER TABLE dbo.orderbody
	DROP CONSTRAINT DF_orderbody_defect_ok
;
CREATE TABLE dbo.Tmp_orderbody
	(
	id_orderbody int NOT NULL IDENTITY (1, 1),
	id_order int NULL,
	id_mashine nchar(30) NULL,
	id_material nchar(30) NULL,
	id_good nchar(30) NULL,
	guid nchar(40) NULL,
	del smallint NULL,
	quantity numeric(18, 3) NULL,
	actual_quantity numeric(18, 3) NULL,
	sign nchar(1) NULL,
	price decimal(9, 2) NULL,
	dateadd datetime NULL,
	id_user_add int NULL,
	name_add nchar(255) NULL,
	datework datetime NULL,
	id_user_work int NULL,
	name_work nchar(255) NULL,
	defect_quantity numeric(18, 3) NULL,
	id_user_defect int NULL,
	user_defect nchar(255) NULL,
	tech_defect int NULL,
	exported bit NULL,
	defect_ok bit NULL,
	comment nchar(2048) NULL
	)  ON [PRIMARY]
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_id_order DEFAULT ((0)) FOR id_order
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_id_mashine DEFAULT (N'') FOR id_mashine
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_id_material DEFAULT (N'') FOR id_material
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_id_good DEFAULT (N'') FOR id_good
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_guid DEFAULT (newid()) FOR guid
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_del DEFAULT ((0)) FOR del
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_quantity DEFAULT ((0)) FOR quantity
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_actual_quantity DEFAULT ((0)) FOR actual_quantity
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_sign DEFAULT (N'') FOR sign
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_price DEFAULT ((0)) FOR price
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_dateadd DEFAULT (getdate()) FOR dateadd
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_id_user_work DEFAULT ((0)) FOR id_user_work
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_name_work DEFAULT (N'') FOR name_work
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_defect_quantity DEFAULT ((0)) FOR defect_quantity
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_id_user_defect DEFAULT ((0)) FOR id_user_defect
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_user_defect DEFAULT (N'') FOR user_defect
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_tech_defect DEFAULT ((0)) FOR tech_defect
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_exported DEFAULT ((0)) FOR exported
;
ALTER TABLE dbo.Tmp_orderbody ADD CONSTRAINT
	DF_orderbody_defect_ok DEFAULT ((0)) FOR defect_ok
;
SET IDENTITY_INSERT dbo.Tmp_orderbody ON
;
IF EXISTS(SELECT * FROM dbo.orderbody)
	 EXEC('INSERT INTO dbo.Tmp_orderbody (id_orderbody, id_order, id_mashine, id_material, id_good, guid, del, quantity, actual_quantity, sign, price, dateadd, id_user_add, name_add, datework, id_user_work, name_work, defect_quantity, id_user_defect, user_defect, tech_defect, exported, defect_ok, comment)
		SELECT id_orderbody, id_order, id_mashine, id_material, id_good, guid, del, quantity, actual_quantity, sign, price, dateadd, id_user_add, name_add, datework, id_user_work, name_work, defect_quantity, id_user_defect, user_defect, tech_defect, exported, defect_ok, comment FROM dbo.orderbody WITH (HOLDLOCK TABLOCKX)')
;
SET IDENTITY_INSERT dbo.Tmp_orderbody OFF
;
DROP TABLE dbo.orderbody
;
EXECUTE sp_rename N'dbo.Tmp_orderbody', N'orderbody', 'OBJECT' 
;
ALTER TABLE dbo.orderbody ADD CONSTRAINT
	PK_orderbody PRIMARY KEY CLUSTERED 
	(
	id_orderbody
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

;
COMMIT
;