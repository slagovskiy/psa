CREATE VIEW [dbo].[vwOrderEventsImport]
AS
SELECT     id_order, MAX(event_date) AS event_date
FROM         dbo.orderevent
WHERE     (event_text LIKE 'Заказ был импортирован%')
GROUP BY id_order