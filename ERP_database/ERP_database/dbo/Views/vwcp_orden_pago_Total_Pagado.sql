create view [dbo].[vwcp_orden_pago_Total_Pagado]
as
SELECT     IdEmpresa_op, IdOrdenPago_op, SUM(Total_cancelado) AS Total_cancelado
FROM         vwcp_orden_pago_Total_cancelacion
GROUP BY IdEmpresa_op, IdOrdenPago_op