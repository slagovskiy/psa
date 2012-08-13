CREATE VIEW [dbo].[vwDefectFull]
AS
SELECT     dbo.orderbody.id_orderbody, dbo.orderbody.datework, dbo.[order].number, dbo.mashine.mashine, dbo.material.material AS paper, 
                      dbo.good.name AS good, dbo.orderbody.defect_quantity, dbo.orderbody.tech_defect AS tech_defect_code, 
                      CASE WHEN dbo.orderbody.tech_defect = 1 THEN CHAR(193) + CHAR(240) + CHAR(224) + CHAR(234) 
                      WHEN dbo.orderbody.tech_defect = 2 THEN CHAR(210) + CHAR(229) + CHAR(245) + CHAR(32) + CHAR(193) + CHAR(240) + CHAR(224) + CHAR(234) 
                      WHEN dbo.orderbody.tech_defect = 3 THEN 'Setup' WHEN dbo.orderbody.tech_defect = 4 THEN CHAR(206) + CHAR(225) + CHAR(240) + CHAR(229) 
                      + CHAR(231) + CHAR(234) + CHAR(224) WHEN dbo.orderbody.tech_defect = 9 THEN CHAR(205) + CHAR(229) + CHAR(32) + CHAR(241) + CHAR(242) 
                      + CHAR(224) + CHAR(237) + CHAR(228) + CHAR(224) + CHAR(240) + CHAR(242) + CHAR(237) + CHAR(224) + CHAR(255) + CHAR(32) + CHAR(238) 
                      + CHAR(225) + CHAR(240) + CHAR(229) + CHAR(231) + CHAR(234) + CHAR(224) 
                      WHEN dbo.orderbody.tech_defect = 5 THEN 'Index print' WHEN dbo.orderbody.tech_defect = 6 THEN 'Metallik' WHEN dbo.orderbody.tech_defect = 7 THEN
                       CHAR(202) + CHAR(238) + CHAR(240) + CHAR(240) + CHAR(229) + CHAR(234) + CHAR(242) + CHAR(232) + CHAR(240) + CHAR(238) + CHAR(226) 
                      + CHAR(234) + CHAR(224) WHEN dbo.orderbody.tech_defect = 8 THEN 'S Print' END AS tech_defect, dbo.orderbody.id_user_work, 
                      dbo.orderbody.id_user_add
FROM         dbo.good RIGHT OUTER JOIN
                      dbo.orderbody ON dbo.good.id_good = dbo.orderbody.id_good LEFT OUTER JOIN
                      dbo.[order] ON dbo.orderbody.id_order = dbo.[order].id_order LEFT OUTER JOIN
                      dbo.material ON dbo.orderbody.id_material = dbo.material.id_material LEFT OUTER JOIN
                      dbo.mashine ON dbo.orderbody.id_mashine = dbo.mashine.id_mashine
WHERE     (ISNULL(dbo.orderbody.del, 0) = 0) OR
                      (dbo.orderbody.del <> 1)