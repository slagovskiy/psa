CREATE PROCEDURE [dbo].[spVerificationBody_UpdateAction]
(
   @order_action_t nchar(255),
   @order_action nchar(255),
   @order_user int,
   @number nchar(15),
   @id int
)
AS
BEGIN
	UPDATE [dbo].[verificationbody]
	   SET [order_action_t] = @order_action_t
		  ,[order_action] = @order_action
		  ,[order_user] = @order_user
	WHERE ([order_number] = @number) AND ([id_verification] = @id)

	UPDATE [dbo].[verification]
		SET [exported] = 0
	WHERE [id_verification] = (
		SELECT DISTINCT [id_verification]
		FROM [dbo].[verificationbody]
		WHERE ([order_number] = @number) AND ([id_verification] = @id)
	)
END