CREATE PROCEDURE [dbo].[spUser_GetNameById]
(
	@id int
)
AS
BEGIN
	SELECT [name]
		FROM [dbo].[user]	
		WHERE [id_user] = @id
END
