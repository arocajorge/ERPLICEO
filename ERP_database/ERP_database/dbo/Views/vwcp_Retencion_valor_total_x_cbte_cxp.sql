create view [dbo].[vwcp_Retencion_valor_total_x_cbte_cxp]
as
SELECT        dbo.cp_retencion_det.IdEmpresa, dbo.cp_retencion_det.IdRetencion, SUM(dbo.cp_retencion_det.re_valor_retencion) AS Total_Retencion, dbo.cp_retencion.serie1 + '-'+ cp_retencion.serie2 as serie , 
                         dbo.cp_retencion.NumRetencion, dbo.cp_retencion.NAutorizacion, dbo.cp_retencion.fecha, dbo.cp_retencion.IdEmpresa_Ogiro, dbo.cp_retencion.IdCbteCble_Ogiro, 
                         dbo.cp_retencion.IdTipoCbte_Ogiro
FROM            dbo.cp_retencion_det INNER JOIN
                         dbo.cp_retencion ON dbo.cp_retencion_det.IdEmpresa = dbo.cp_retencion.IdEmpresa AND dbo.cp_retencion_det.IdRetencion = dbo.cp_retencion.IdRetencion
GROUP BY dbo.cp_retencion_det.IdEmpresa, dbo.cp_retencion_det.IdRetencion, dbo.cp_retencion.serie1 + '-'+ cp_retencion.serie2, dbo.cp_retencion.NumRetencion, dbo.cp_retencion.NAutorizacion, 
                         dbo.cp_retencion.fecha, dbo.cp_retencion.IdEmpresa_Ogiro, dbo.cp_retencion.IdCbteCble_Ogiro, dbo.cp_retencion.IdTipoCbte_Ogiro