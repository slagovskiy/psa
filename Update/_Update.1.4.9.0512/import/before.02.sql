CREATE VIEW [dbo].[vwPaymentFull]
AS
SELECT     dbo.payments.id_payment, dbo.payments.guid, dbo.payments.date, dbo.payments.time, dbo.payments.id_user, dbo.payments.name_user, 
                      dbo.payments.number, dbo.payments.payment, dbo.payments.type, dbo.payments.comment, dbo.payments.payment_way, dbo.payments.del, 
                      dbo.category.name
FROM         dbo.client LEFT OUTER JOIN
                      dbo.category ON dbo.client.id_category = dbo.category.id_category LEFT OUTER JOIN
                      dbo.[order] ON dbo.client.id_client = dbo.[order].id_client RIGHT OUTER JOIN
                      dbo.payments ON dbo.[order].number = dbo.payments.number
WHERE     (dbo.payments.del < 1) OR
                      (dbo.payments.del IS NULL)