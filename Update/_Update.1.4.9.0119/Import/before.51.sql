CREATE PROCEDURE [dbo].[spVerificationBody_GetByVerificationId]
	@id int
AS
BEGIN
	SELECT [id_verificationbody]
		  ,[del]
		  ,[guid]
		  ,[id_verification]
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
	FROM [dbo].[verificationbody]
	WHERE [id_verification] = @id
	ORDER BY [order_number]
END