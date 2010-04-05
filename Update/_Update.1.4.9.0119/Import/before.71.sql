DECLARE @c int;
SET @c = (SELECT COUNT(*) FROM [dbo].[order_status] WHERE [order_status] = '300000')
IF (@c = 0)
	BEGIN
		INSERT INTO [dbo].[order_status] ([order_status], [status_desc]) VALUES ('300000','Утеряно')
	END