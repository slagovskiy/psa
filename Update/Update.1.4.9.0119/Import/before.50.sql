CREATE PROCEDURE [dbo].[spVerification_GetList]
AS
BEGIN
	SELECT [id_verification]
		  ,[del]
		  ,[guid]
		  ,[verification_date]
		  ,[verification_user]
	FROM [dbo].[verification]
	ORDER BY [verification_date] DESC
END