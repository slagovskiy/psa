CREATE PROCEDURE [dbo].[spVerification_GetById]
	@id int
AS
BEGIN
	SELECT [id_verification]
		  ,[del]
		  ,[guid]
		  ,[verification_date]
		  ,[verification_user]
	FROM [dbo].[verification]
	WHERE [id_verification] = @id
END