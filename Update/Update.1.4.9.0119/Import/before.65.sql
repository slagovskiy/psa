CREATE PROCEDURE [dbo].[spAdvPrice]
(
	@id_good nchar(30) = 0, 
	@id_category int = 0, 
	@threshold int = 0,
	@ondate datetime = getdate
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @real_threshold int
	DECLARE @real_threshold2 int
	DECLARE @amount1 decimal(9,2)
	DECLARE @amount2 decimal(9,2)
	DECLARE @amount3 decimal(9,2)
	SET @real_threshold = (SELECT [threshold] FROM [vwPriceFull] WHERE [id_good] = @id_good AND [id_category] = @id_category AND [ondate] = (SELECT MAX(ondate) FROM dbo.vwPriceFull AS vwPriceFull WHERE (id_good = @id_good) AND (id_category = @id_category) AND (ondate <= @ondate)))
	SET @real_threshold2 = (SELECT [threshold2] FROM [vwPriceFull] WHERE [id_good] = @id_good AND [id_category] = @id_category AND [ondate] = (SELECT MAX(ondate) FROM dbo.vwPriceFull AS vwPriceFull WHERE (id_good = @id_good) AND (id_category = @id_category) AND (ondate <= @ondate)))
	SET @amount1 = (SELECT [amount] FROM [vwPriceFull] WHERE [id_good] = @id_good AND [id_category] = @id_category AND [ondate] = (SELECT MAX(ondate) FROM dbo.vwPriceFull AS vwPriceFull WHERE (id_good = @id_good) AND (id_category = @id_category) AND (ondate <= @ondate)))
	SET @amount2 = (SELECT [amount2] FROM [vwPriceFull] WHERE [id_good] = @id_good AND [id_category] = @id_category AND [ondate] = (SELECT MAX(ondate) FROM dbo.vwPriceFull AS vwPriceFull WHERE (id_good = @id_good) AND (id_category = @id_category) AND (ondate <= @ondate)))
	SET @amount3 = (SELECT [amount3] FROM [vwPriceFull] WHERE [id_good] = @id_good AND [id_category] = @id_category AND [ondate] = (SELECT MAX(ondate) FROM dbo.vwPriceFull AS vwPriceFull WHERE (id_good = @id_good) AND (id_category = @id_category) AND (ondate <= @ondate)))
	IF (@real_threshold2 = 0)
		SET @real_threshold2 = @real_threshold
	IF (@amount3 = 0)
		SET @amount3 = @amount2
	IF (@amount2 = 0)
	BEGIN
		SET @amount2 = @amount1
		SET @amount3 = @amount1
	END

	IF (@threshold > @real_threshold2)
		SELECT @amount3 AS amount
	ELSE
		BEGIN
			IF (@threshold > @real_threshold)
				SELECT @amount2 AS amount
			ELSE
				SELECT @amount1 AS amount
		END

END
