CREATE PROCEDURE [dbo].[spKiosk_Add]
(
	@name nchar(1024),
	@path nchar(1024),
	@code int,
	@id int output
)
AS
BEGIN
INSERT INTO [dbo].[kiosk]
           ([name]
           ,[path]
           ,[code])
     VALUES
           (@name
           ,@path
           ,@code)

SET @id = scope_identity()

END