
CREATE VIEW [dbo].[vwOrderQuickListOperator]
AS
SELECT     dbo.[order].id_order, dbo.client.name AS client, dbo.category.name AS category, dbo.[order].name_accept, dbo.[order].name_designer, dbo.[order].number, 
                      dbo.[order].input_date, dbo.[order].expected_date, dbo.[order].comment, dbo.[order].crop, dbo.[order].type, dbo.[order].id_user_operator, dbo.[order].status
FROM         dbo.[order] LEFT OUTER JOIN
                      dbo.client ON dbo.[order].id_client = dbo.client.id_client LEFT OUTER JOIN
                      dbo.category ON dbo.client.id_category = dbo.category.id_category
;

