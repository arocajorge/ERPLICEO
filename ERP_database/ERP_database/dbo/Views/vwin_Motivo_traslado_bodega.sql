CREATE VIEW [dbo].[vwin_Motivo_traslado_bodega]
as
SELECT     IdCatalogo AS IdMotivo_Traslado, IdCatalogo_tipo, Nombre, Estado
FROM         dbo.in_Catalogo
WHERE     (IdCatalogo_tipo = 3)