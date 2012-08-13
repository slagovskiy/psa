CREATE VIEW [dbo].[vwExportDiscard]
AS
SELECT     id_discard, del, guid, datediscard, id_material, quantity, comment, id_user, user_name, orderno, exported, id_mashine
FROM         dbo.discard
WHERE     (exported <> 1) OR
                      (exported IS NULL)
