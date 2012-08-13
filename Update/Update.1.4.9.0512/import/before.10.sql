CREATE PROCEDURE spOrderGetStatusNameByNumber 
	@n nchar(12)
AS
BEGIN
	DECLARE @cnt int;
	SET @cnt = (
					SELECT COUNT(*)
					FROM dbo.[order] INNER JOIN
						dbo.order_status ON dbo.[order].status = dbo.order_status.order_status
					WHERE (dbo.[order].number = @n)
				)
	IF (@cnt > 0)
		BEGIN
			SELECT dbo.order_status.status_desc
			FROM dbo.[order] INNER JOIN
				dbo.order_status ON dbo.[order].status = dbo.order_status.order_status
			WHERE (dbo.[order].number = @n)
		END
	ELSE
		BEGIN
			SELECT 'не удалось получить статус'
		END
END