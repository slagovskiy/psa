CREATE VIEW [dbo].[vwAdmin_Defect]
AS
SELECT     dbo.orderbody.id_orderbody, dbo.orderbody.id_order, dbo.good.name, dbo.orderbody.datework, dbo.orderbody.name_work, 
                      dbo.orderbody.defect_quantity, dbo.orderbody.user_defect, dbo.orderbody.tech_defect, dbo.orderbody.defect_ok, dbo.good.id_good, 
                      dbo.orderbody.id_user_defect, dbo.orderbody.comment, dbo.[order].number
FROM         dbo.orderbody LEFT OUTER JOIN
                      dbo.[order] ON dbo.orderbody.id_order = dbo.[order].id_order LEFT OUTER JOIN
                      dbo.good ON dbo.orderbody.id_good = dbo.good.id_good
WHERE     (dbo.orderbody.defect_quantity > 0)

