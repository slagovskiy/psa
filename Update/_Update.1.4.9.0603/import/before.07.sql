CREATE VIEW [dbo].[vwDiscardList]
AS
SELECT     dbo.discard.id_discard, dbo.discard.datediscard, dbo.discard.id_material, dbo.material.material, dbo.discard.quantity, dbo.discard.comment, 
                      dbo.discard.user_name, dbo.discard.orderno, dbo.discard.id_user, dbo.discard.del, dbo.mashine.mashine
FROM         dbo.discard LEFT OUTER JOIN
                      dbo.mashine ON dbo.discard.id_mashine = dbo.mashine.id_mashine LEFT OUTER JOIN
                      dbo.material ON dbo.discard.id_material = dbo.material.id_material
WHERE     (dbo.discard.del <> 1) OR
                      (ISNULL(dbo.discard.del, 0) = 0)
