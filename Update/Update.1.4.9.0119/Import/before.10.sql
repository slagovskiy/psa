CREATE VIEW [dbo].[vwOrderListDesigner]
AS
SELECT DISTINCT 
                      TOP (100) PERCENT dbo.[order].id_order, dbo.[order].number, RTRIM(dbo.[order].status) AS status, dbo.[order].input_date, dbo.[order].expected_date, 
                      dbo.orderbody.id_good, dbo.[order].id_user_designer
FROM         dbo.[order] INNER JOIN
                      dbo.orderbody ON dbo.[order].id_order = dbo.orderbody.id_order
WHERE     (dbo.[order].del IS NULL) AND (RTRIM(dbo.[order].status) = N'000200' OR
                      RTRIM(dbo.[order].status) = N'000210' OR
                      RTRIM(dbo.[order].status) = N'000212' OR
                      RTRIM(dbo.[order].status) = N'000211' OR
                      RTRIM(dbo.[order].status) = '000111') OR
                      (dbo.[order].del < 1) AND (RTRIM(dbo.[order].status) = N'000200' OR
                      RTRIM(dbo.[order].status) = N'000210' OR
                      RTRIM(dbo.[order].status) = N'000212' OR
                      RTRIM(dbo.[order].status) = N'000211' OR
                      RTRIM(dbo.[order].status) = '000111')
