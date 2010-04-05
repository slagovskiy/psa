BEGIN TRANSACTION
;
DECLARE @C1 int;
DECLARE @C2 int;
DECLARE @C3 int;

DECLARE @I1 int;
DECLARE @I2 int;
DECLARE @I3 int;

SET @C1 = (SELECT COUNT(*) FROM [category] WHERE RTRIM(UPPER([name])) LIKE 'ÐÎÇÍÈÖÀ' AND [del] = 0);
SET @C2 = (SELECT COUNT(*) FROM [category] WHERE RTRIM(UPPER([name])) LIKE 'ÑÎÒÐÓÄÍÈÊ' AND [del] = 0);
SET @C3 = (SELECT COUNT(*) FROM [category] WHERE RTRIM(UPPER([name])) LIKE 'ÎÏÒÎÂÈÊ' AND [del] = 0);
IF((@C1 = 1) AND (@C2 = 1) AND (@C3 = 1))
BEGIN
	SET @I1 = (SELECT [id_category] FROM [category] WHERE RTRIM(UPPER([name])) LIKE 'ÐÎÇÍÈÖÀ' AND [del] = 0);
	SET @I2 = (SELECT [id_category] FROM [category] WHERE RTRIM(UPPER([name])) LIKE 'ÑÎÒÐÓÄÍÈÊ' AND [del] = 0);
	SET @I3 = (SELECT [id_category] FROM [category] WHERE RTRIM(UPPER([name])) LIKE 'ÎÏÒÎÂÈÊ' AND [del] = 0);

	IF ((@I1 > 0) AND (@I2 > 0) AND (@I3 > 0))
	BEGIN
		DELETE FROM [dbo].[category]
		WHERE (([id_category] <> @I1) AND ([id_category] <> @I2) AND ([id_category] <> @I3));

		UPDATE [dbo].[category]
		SET [id_category] = 1
		WHERE [id_category] = @I1;

		UPDATE [dbo].[category]
		SET [id_category] = 2
		WHERE [id_category] = @I2;

		UPDATE [dbo].[category]
		SET [id_category] = 3
		WHERE [id_category] = @I3

		UPDATE [dbo].[client]
		SET [id_category] = 3
		WHERE (([id_category] <> @I1) AND ([id_category] <> @I2));

		UPDATE [dbo].[client]
		SET [id_category] = 1
		WHERE [id_category] = @I1;

		UPDATE [dbo].[client]
		SET [id_category] = 2
		WHERE [id_category] = @I2;
	END
END
;
COMMIT
;
