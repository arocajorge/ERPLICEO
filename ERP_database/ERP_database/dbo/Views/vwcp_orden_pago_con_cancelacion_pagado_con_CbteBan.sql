create view [dbo].[vwcp_orden_pago_con_cancelacion_pagado_con_CbteBan]
as
SELECT        cp_orden_pago_cancelaciones.IdEmpresa_cxp, cp_orden_pago_cancelaciones.IdTipoCbte_cxp, cp_orden_pago_cancelaciones.IdCbteCble_cxp, 
                         SUM(cp_orden_pago_cancelaciones.MontoAplicado) AS MontoAplicado
FROM            cp_orden_pago_cancelaciones INNER JOIN
                         ba_Cbte_Ban ON cp_orden_pago_cancelaciones.IdEmpresa_pago = ba_Cbte_Ban.IdEmpresa AND 
                         cp_orden_pago_cancelaciones.IdTipoCbte_pago = ba_Cbte_Ban.IdTipocbte AND cp_orden_pago_cancelaciones.IdCbteCble_pago = ba_Cbte_Ban.IdCbteCble
GROUP BY cp_orden_pago_cancelaciones.IdEmpresa_cxp, cp_orden_pago_cancelaciones.IdTipoCbte_cxp, cp_orden_pago_cancelaciones.IdCbteCble_cxp