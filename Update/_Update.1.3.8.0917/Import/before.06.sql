CREATE PROCEDURE [dbo].[spInventoryBody_UpdateAction]
(
   @order_action_t nchar(255),
   @order_action nchar(255),
   @order_user int,
   @number nchar(15),
   @id int
)
AS
BEGIN
	UPDATE [dbo].[inventorybody]
	   SET [order_action_t] = @order_action_t
		  ,[order_action] = @order_action
		  ,[order_user] = @order_user
	WHERE ([order_number] = @number) AND ([id_inventory] = @id)
END
