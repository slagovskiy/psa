CREATE PROCEDURE [dbo].[spKiosk_Delete]
(
	@del bit,
	@id int
)
AS
BEGIN
	UPDATE [dbo].[kiosk]
	   SET [del] = @del
	WHERE [id_kiosk] = @id
END