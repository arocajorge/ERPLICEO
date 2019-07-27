create view [dbo].[vwro_TipoContrato]
as
SELECT     IdCatalogo, CodCatalogo AS codContrato, ca_descripcion AS Contrato, ca_estado AS Estado, ca_orden AS Orden, IdTipoCatalogo
FROM         dbo.ro_catalogo
WHERE     (IdTipoCatalogo = 2)