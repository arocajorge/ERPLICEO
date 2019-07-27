create view [dbo].[vw_orden_pago_cancelaciones_TotalAplicado]
as
SELECT        IdEmpresa_op, IdOrdenPago_op, Secuencia_op, SUM(MontoAplicado) AS MontoAplicado
FROM            cp_orden_pago_cancelaciones
GROUP BY IdEmpresa_op, IdOrdenPago_op, Secuencia_op