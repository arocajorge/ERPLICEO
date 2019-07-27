create view [dbo].[vwcp_orden_pago_total]
as
SELECT     IdEmpresa, IdOrdenPago, SUM(Valor_a_pagar) AS Total_OP
FROM         cp_orden_pago_det
GROUP BY IdEmpresa, IdOrdenPago