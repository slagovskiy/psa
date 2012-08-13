CREATE VIEW [dbo].[vwOrderEventsExport]
AS
SELECT     id_order, MAX(event_date) AS event_date
FROM         dbo.orderevent
WHERE     (event_text LIKE 'Закал был экспортирован%')
GROUP BY id_order