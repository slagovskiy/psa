CREATE VIEW [dbo].[vwOrderBodyDesigner]
AS
SELECT     dbo.orderbody.id_orderbody, dbo.orderbody.id_order, dbo.orderbody.id_good, dbo.good.name, dbo.orderbody.guid, dbo.orderbody.quantity, 
                      dbo.orderbody.actual_quantity, dbo.good.type, dbo.orderbody.datework, dbo.orderbody.id_user_work, dbo.orderbody.name_work, 
                      dbo.orderbody.dateadd
FROM         dbo.orderbody LEFT OUTER JOIN
                      dbo.good ON dbo.orderbody.id_good = dbo.good.id_good
WHERE     (dbo.orderbody.del <> 1) AND (dbo.good.type = N'2' OR
                      dbo.good.type = N'2') OR
                      (dbo.good.type = N'2' OR
                      dbo.good.type = N'2') AND (ISNULL(dbo.orderbody.del, 0) = 0)