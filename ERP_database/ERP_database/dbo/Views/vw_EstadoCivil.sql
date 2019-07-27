create view [dbo].[vw_EstadoCivil]
as
SELECT     IdCatalogo AS Id, CodCatalogo AS Codigo, ca_descripcion AS descripcion, ca_estado AS estado, ca_orden AS orden, IdTipoCatalogo
FROM         dbo.tb_Catalogo
WHERE     (IdTipoCatalogo = 2)