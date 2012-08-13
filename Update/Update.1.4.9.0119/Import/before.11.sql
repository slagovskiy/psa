CREATE VIEW [dbo].[vwOrderListOperator]
AS
SELECT DISTINCT 
                      TOP (100) PERCENT dbo.[order].id_order, dbo.[order].number, RTRIM(dbo.[order].status) AS status, dbo.[order].input_date, dbo.[order].expected_date, 
                      dbo.orderbody.id_good, dbo.[order].crop, dbo.[order].type, dbo.[order].id_user_operator
FROM         dbo.[order] INNER JOIN
                      dbo.orderbody ON dbo.[order].id_order = dbo.orderbody.id_order
WHERE     (dbo.[order].del IS NULL) AND (RTRIM(dbo.[order].status) = N'000100' OR
                      RTRIM(dbo.[order].status) = N'000110' OR
                      RTRIM(dbo.[order].status) = N'000111' OR
                      RTRIM(dbo.[order].status) = N'000211') OR
                      (dbo.[order].del < 1) AND (RTRIM(dbo.[order].status) = N'000100' OR
                      RTRIM(dbo.[order].status) = N'000110' OR
                      RTRIM(dbo.[order].status) = N'000111' OR
                      RTRIM(dbo.[order].status) = N'000211')
ORDER BY status
