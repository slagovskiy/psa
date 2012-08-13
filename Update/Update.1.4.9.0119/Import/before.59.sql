CREATE PROCEDURE [dbo].[spKiosk_Update]
(
	@name nchar(1024),
	@path nchar(1024),
	@code int,
	@id int
)
AS
BEGIN
	UPDATE [dbo].[kiosk]
	   SET [name] = @name
		  ,[path] = @path
		  ,[code] = @code
	WHERE [id_kiosk] = @id
END