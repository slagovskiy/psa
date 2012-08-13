CREATE PROCEDURE [dbo].[spKiosk_GetAll]
AS
BEGIN
	SELECT [id_kiosk]
		  ,[guid]
		  ,[del]
		  ,[name]
		  ,[path]
		  ,[code]
	FROM [dbo].[kiosk]
	ORDER BY [code]
END