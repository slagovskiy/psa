CREATE PROCEDURE [dbo].[spInventory_GetList]
AS
BEGIN
	SELECT [id_inventory]
		  ,[del]
		  ,[guid]
		  ,[inventory_date]
		  ,[inventory_user]
	FROM [dbo].[inventory]
	ORDER BY [inventory_date] DESC
END