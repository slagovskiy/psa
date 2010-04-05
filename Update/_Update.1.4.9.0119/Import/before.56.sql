CREATE PROCEDURE [dbo].[spKiosk_Get]
AS
BEGIN
	SELECT [id_kiosk]
		  ,[guid]
		  ,[del]
		  ,[name]
		  ,[path]
		  ,[code]
	FROM [dbo].[kiosk]
	WHERE [del] = 0
	ORDER BY [code]
END