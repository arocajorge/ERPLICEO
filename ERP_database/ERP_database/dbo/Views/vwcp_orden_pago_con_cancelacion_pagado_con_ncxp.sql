create view [dbo].[vwcp_orden_pago_con_cancelacion_pagado_con_ncxp]
as
SELECT        cp_orden_pago_cancelaciones.IdEmpresa_cxp, cp_orden_pago_cancelaciones.IdTipoCbte_cxp, cp_orden_pago_cancelaciones.IdCbteCble_cxp, 
                         SUM(cp_orden_pago_cancelaciones.MontoAplicado) AS MontoAplicado
FROM            cp_orden_pago_cancelaciones INNER JOIN
                         cp_nota_DebCre ON cp_orden_pago_cancelaciones.IdEmpresa_pago = cp_nota_DebCre.IdEmpresa AND 
                         cp_orden_pago_cancelaciones.IdCbteCble_pago = cp_nota_DebCre.IdCbteCble_Nota AND 
                         cp_orden_pago_cancelaciones.IdTipoCbte_pago = cp_nota_DebCre.IdTipoCbte_Nota
GROUP BY cp_orden_pago_cancelaciones.IdEmpresa_cxp, cp_orden_pago_cancelaciones.IdTipoCbte_cxp, cp_orden_pago_cancelaciones.IdCbteCble_cxp