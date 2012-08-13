CREATE PROCEDURE [dbo].[spKiosk_GetByCode]
	@code int
AS
BEGIN
	SELECT [id_kiosk]
		  ,[guid]
		  ,[del]
		  ,[name]
		  ,[path]
		  ,[code]
	FROM [dbo].[kiosk]
	WHERE [code] = @code
END