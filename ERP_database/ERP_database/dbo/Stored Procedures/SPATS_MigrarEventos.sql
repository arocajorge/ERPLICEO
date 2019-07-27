CREATE PROCEDURE [dbo].[SPATS_MigrarEventos]
AS

update EntidadRegulatoria.ATS_ventas_eventos set DenoCli = A.DenoCli
FROM EntidadRegulatoria.ATS_ventas A
WHERE EntidadRegulatoria.ATS_ventas_eventos.IdEmpresa = A.IdEmpresa
AND EntidadRegulatoria.ATS_ventas_eventos.IdPeriodo = A.IdPeriodo
AND EntidadRegulatoria.ATS_ventas_eventos.idCliente = A.idCliente


INSERT INTO EntidadRegulatoria.ATS_ventas
select * from EntidadRegulatoria.ATS_ventas_eventos