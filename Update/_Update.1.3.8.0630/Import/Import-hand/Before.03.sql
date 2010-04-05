DECLARE @C int
SET @C = (SELECT COUNT(*) FROM [dbo].[order_status] WHERE [order_status] = '200000')
IF (@C = 0)
BEGIN
INSERT INTO [dbo].[order_status]
           ([order_status]
           ,[status_desc])
     VALUES
           ('200000'
           ,'Выдано (не оплачено)')
END
;