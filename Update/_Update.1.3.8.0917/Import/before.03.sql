CREATE PROCEDURE [dbo].[spInventory_GetById]
	@id int
AS
BEGIN
	SELECT [id_inventory]
		  ,[del]
		  ,[guid]
		  ,[inventory_date]
		  ,[inventory_user]
	FROM [dbo].[inventory]
	WHERE [id_inventory] = @id
END