
create view [dbo].[vwRo_Catalogo_Bancos]
as

SELECT     dbo.ro_catalogo.CodCatalogo, dbo.ro_catalogo.ca_descripcion, dbo.ro_catalogoTipo.IdTipoCatalogo, dbo.ro_catalogoTipo.tc_Descripcion
FROM         dbo.ro_catalogo INNER JOIN
                      dbo.ro_catalogoTipo ON dbo.ro_catalogo.IdTipoCatalogo = dbo.ro_catalogoTipo.IdTipoCatalogo
 where dbo.ro_catalogoTipo.IdTipoCatalogo=11