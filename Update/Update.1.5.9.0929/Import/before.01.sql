CREATE VIEW [dbo].[vwOrderEventsWorkOk]
AS
SELECT     id_order, MAX(event_date) AS event_date
FROM         dbo.orderevent
WHERE     (event_text LIKE '�������� �������� ������ ��� �������%') OR
                      (event_text LIKE '�������� �������� ������ ��� �������%')
GROUP BY id_order