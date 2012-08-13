CREATE PROCEDURE spOrderExportTabel
(
	@di1 as datetime,
	@di2 as datetime,
	@de1 as datetime,
	@de2 as datetime,
	@p as char(10)
)
AS
BEGIN
	DECLARE @d1 as datetime;
	DECLARE @d2 as datetime;
	DECLARE @pn as char(10);

	SET @pn = @p + '%';

	IF (@di1 < @de1)
		SET @d1 = @di1;
	ELSE
		SET @d1 = @de1;

	IF (@di2 > @de2)
		SET @d2 = @di2;
	ELSE
		SET @d2 = @de2;

	DECLARE @event_ok AS TABLE (id_order int, event_date datetime);
	INSERT INTO @event_ok 
		  SELECT id_order, MAX(event_date) AS event_date
		  FROM dbo.orderevent
		  WHERE (
				(event_text LIKE 'Дизайнер завершил работу над заказом%') OR
				(event_text LIKE 'Оператор завершил работу над заказом%')
				) AND (
				RTRIM(event_point) = @p
				) 
		  GROUP BY id_order
		  HAVING (MAX(event_date) >= @d1) AND (MAX(event_date) <= @d2);

	DECLARE @event_import AS TABLE (id_order int, event_date datetime);
	INSERT INTO @event_import 
		  SELECT id_order, MAX(event_date) AS event_date
		  FROM dbo.orderevent
		  WHERE (
				(event_text LIKE 'Заказ был импортирован%')
				) AND (
				RTRIM(event_point) = @p
				) 
		  GROUP BY id_order
		  HAVING (MAX(event_date) >= @d1) AND (MAX(event_date) <= @d2);

	DECLARE @event_export AS TABLE (id_order int, event_date datetime);
	INSERT INTO @event_export 
		  SELECT id_order, MAX(event_date) AS event_date
		  FROM dbo.orderevent
		  WHERE (
				(event_text LIKE 'Зака% был экспортирован%')
				) AND (
				RTRIM(event_point) = @p
				) 
		  GROUP BY id_order
		  HAVING (MAX(event_date) >= @d1) AND (MAX(event_date) <= @d2);


	SELECT 
		dbo.[order].id_order, 
		dbo.[order].number, 
		dbo.client.name, 
		dbo.order_status.status_desc, 
		[@event_import].[event_date] AS ImportDate, 
		[@event_ok].[event_date] AS WorkDate, 
		[@event_export].[event_date] AS ExportDate, 
		dbo.[order].name_accept 
	FROM 
		dbo.[order] 
		INNER JOIN dbo.client ON dbo.[order].id_client = dbo.client.id_client 
		INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status 
		LEFT OUTER JOIN @event_ok ON dbo.[order].id_order = [@event_ok].id_order 
		LEFT OUTER JOIN @event_export ON dbo.[order].id_order = [@event_export].id_order 
		LEFT OUTER JOIN @event_import ON dbo.[order].id_order = [@event_import].id_order 
	WHERE 
		(NOT (dbo.[order].number LIKE @pn OR 
			  dbo.[order].number LIKE N'40%' OR 
			  dbo.[order].number LIKE N'41%' OR 
			  dbo.[order].number LIKE N'42%' OR 
			  dbo.[order].number LIKE N'49%'))
		AND ([@event_export].[event_date] >= @de1 AND
			 [@event_export].[event_date] <= @de2
			)
		AND ([@event_import].[event_date] >= @di1 AND
			 [@event_import].[event_date] <= @di2
			)
	ORDER BY
		[@event_import].[event_date]
END
