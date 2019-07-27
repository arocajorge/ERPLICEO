create view [dbo].[vwcp_Total_cancelado_x_cbte_cxp]
as
SELECT     IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp, SUM(MontoAplicado) AS Total_Cancelado
FROM         cp_orden_pago_cancelaciones
GROUP BY IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp