create view [dbo].[vwRo_MotivoPrestamo]
as
SELECT     IdCatalogo AS Id, CodCatalogo AS Codigo, ca_descripcion, ca_estado, ca_orden, IdTipoCatalogo
FROM         ro_catalogo
WHERE     (IdTipoCatalogo = 15)