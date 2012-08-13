DECLARE @c int;
SET @c = (SELECT COUNT(*) FROM [dbo].[order_status] WHERE [order_status] = '000111')
IF (@c = 0)
	BEGIN
		INSERT INTO [dbo].[order_status] ([order_status], [status_desc]) VALUES ('000111','Готово после печати')
	END