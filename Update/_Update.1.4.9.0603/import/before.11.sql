DECLARE @cnt int;
SET @cnt = (SELECT COUNT(*) FROM [dbo].[order_status] WHERE [order_status] = '400000');
IF (@cnt = 0)
BEGIN 
	INSERT INTO [dbo].[order_status] ([order_status], [status_desc]) VALUES ('400000', 'Списано');
END