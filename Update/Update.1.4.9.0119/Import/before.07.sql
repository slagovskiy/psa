CREATE VIEW [dbo].[vwPriceFull]
AS
SELECT     dbo.price.id_price, dbo.price.id_good, dbo.price.id_category, dbo.price.guid, dbo.price.amount, dbo.price.amount2, dbo.price.threshold, 
                      dbo.good.guid AS good_guid, dbo.good.name AS good_name, dbo.good.description, dbo.good.prefix, dbo.good.folder, dbo.good.type, 
                      dbo.good.checked, dbo.category.guid AS category_guid, dbo.category.name AS category_name, dbo.price.amount3, dbo.price.threshold2, 
                      dbo.price.ondate
FROM         dbo.price RIGHT OUTER JOIN
                      dbo.category ON dbo.price.id_category = dbo.category.id_category RIGHT OUTER JOIN
                      dbo.good ON dbo.price.id_good = dbo.good.id_good
WHERE     (dbo.price.del <> 1) AND (dbo.good.del <> 1) AND (dbo.category.del <> 1) OR
                      (ISNULL(dbo.category.del, 1) = 1) AND (ISNULL(dbo.price.del, 0) = 0) AND (ISNULL(dbo.good.del, 0) = 0)
