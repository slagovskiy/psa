CREATE VIEW [dbo].[vwOrderEventsWorkOk]
AS
SELECT     id_order, MAX(event_date) AS event_date
FROM         dbo.orderevent
WHERE     (event_text LIKE 'Дизайнер завершил работу над заказом%') OR
                      (event_text LIKE 'Оператор завершил работу над заказом%')
GROUP BY id_order