create view [dbo].[vwcp_orden_giro_total_pagodo]
as
SELECT     IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp, SUM(MontoAplicado) AS TotalPagado
FROM         cp_orden_pago_cancelaciones
GROUP BY IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp