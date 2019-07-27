CREATE VIEW [web].[VWCXP_005_cancelaciones]
AS
SELECT dbo.cp_orden_pago_cancelaciones.IdEmpresa, dbo.cp_orden_pago_cancelaciones.Idcancelacion, dbo.cp_orden_pago_cancelaciones.Secuencia, dbo.cp_orden_pago_cancelaciones.IdEmpresa_cxp, 
                  dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp, dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp, CASE WHEN cp_orden_giro.co_factura IS NULL THEN 'ND ' + CAST(CAST(cp_nota_DebCre.cod_nota AS INT) AS VARCHAR(20)) 
                  ELSE 'FACT ' + CAST(CAST(cp_orden_giro.co_factura AS INT) AS VARCHAR(20)) END AS Referencia, CASE WHEN cp_orden_giro.co_factura IS NULL 
                  THEN cp_nota_DebCre.cn_observacion ELSE cp_orden_giro.co_observacion END AS Observacion, dbo.cp_orden_pago_cancelaciones.MontoAplicado, dbo.cp_ConciliacionAnticipo.IdEmpresa AS IdEmpresa_conciliacion, dbo.cp_ConciliacionAnticipo.IdConciliacion
FROM     dbo.cp_orden_pago_cancelaciones INNER JOIN
                  dbo.cp_ConciliacionAnticipo ON dbo.cp_orden_pago_cancelaciones.IdEmpresa = dbo.cp_ConciliacionAnticipo.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.Idcancelacion = dbo.cp_ConciliacionAnticipo.IdCancelacion LEFT OUTER JOIN
                  dbo.cp_nota_DebCre ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_cxp = dbo.cp_nota_DebCre.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp = dbo.cp_nota_DebCre.IdCbteCble_Nota AND 
                  dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp = dbo.cp_nota_DebCre.IdTipoCbte_Nota LEFT OUTER JOIN
                  dbo.cp_orden_giro ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_cxp = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp = dbo.cp_orden_giro.IdTipoCbte_Ogiro AND 
                  dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp = dbo.cp_orden_giro.IdCbteCble_Ogiro
