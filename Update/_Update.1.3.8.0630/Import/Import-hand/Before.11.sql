CREATE VIEW [dbo].[vwDCardFull]
AS
SELECT     id_dcard, guid, code, name, disc, discserv, date, [not], phone, email, bonus, typebonus
FROM         dbo.dcard
WHERE     (del <> 1) OR
                      (ISNULL(del, 0) = 0)
;