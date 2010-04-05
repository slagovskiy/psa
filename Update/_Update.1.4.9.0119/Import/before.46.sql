CREATE PROCEDURE [dbo].[spInventoryBody_GetByInventoryId]
	@id int
AS
BEGIN
	SELECT [id_inventorybody]
		  ,[del]
		  ,[guid]
		  ,[id_inventory]
		  ,[order_number]
		  ,[order_found]
		  ,[order_status_t]
		  ,[order_status]
          ,[order_status_fact_t]
          ,[order_status_fact]
		  ,[order_in]
		  ,[order_out]
		  ,[order_action_t]
		  ,[order_action]
		  ,[order_user]
		  ,[exported]
	FROM [dbo].[inventorybody]
	WHERE [id_inventory] = @id
	ORDER BY [order_number]
END