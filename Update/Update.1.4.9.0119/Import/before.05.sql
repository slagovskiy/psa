CREATE VIEW [dbo].[vwAdminPriceReport]
AS
SELECT     TOP (100) PERCENT dbo.price.ondate, dbo.good.name, dbo.vwCategoryFull.name AS category, 
                      CASE WHEN dbo.good.type = 1 THEN 'Печать' WHEN dbo.good.type = 2 THEN 'Обработка' END AS types, dbo.price.amount, dbo.price.threshold, 
                      dbo.price.amount2, dbo.price.threshold2, dbo.price.amount3, dbo.good.type
FROM         dbo.good INNER JOIN
                      dbo.price ON dbo.good.id_good = dbo.price.id_good INNER JOIN
                      dbo.vwCategoryFull ON dbo.price.id_category = dbo.vwCategoryFull.id_category
