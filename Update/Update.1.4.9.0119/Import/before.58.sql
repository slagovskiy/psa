CREATE PROCEDURE [dbo].[spKiosk_GetById]
	@id int
AS
BEGIN
	SELECT [id_kiosk]
		  ,[guid]
		  ,[del]
		  ,[name]
		  ,[path]
		  ,[code]
	FROM [dbo].[kiosk]
	WHERE [id_kiosk] = @id
END