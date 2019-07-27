CREATE VIEW [dbo].[vwcp_nota_DebCre_total_saldo]
AS
SELECT        dbo.cp_nota_DebCre.IdEmpresa, dbo.cp_nota_DebCre.IdCbteCble_Nota, dbo.cp_nota_DebCre.IdTipoCbte_Nota, dbo.cp_nota_DebCre.DebCre, 
                         dbo.cp_nota_DebCre.IdProveedor, dbo.cp_nota_DebCre.cn_fecha, 
						 dbo.cp_nota_DebCre.cn_serie1+' - '+dbo.cp_nota_DebCre.cn_serie2 as serie
						 , dbo.cp_nota_DebCre.cn_Nota, dbo.cp_nota_DebCre.Fecha_Transac, 
                         dbo.cp_nota_DebCre.Fecha_contable, dbo.cp_nota_DebCre.cn_Fecha_vcto, dbo.cp_nota_DebCre.cn_observacion, dbo.cp_nota_DebCre.cn_total, 
                         dbo.cp_nota_DebCre.cn_total as valorpagar, ISNULL(dbo.vwcp_orden_giro_total_pagodo.TotalPagado, 0) AS TotalPagado, 
                         dbo.cp_nota_DebCre.cn_total - ISNULL(dbo.vwcp_orden_giro_total_pagodo.TotalPagado, 0) AS SaldoOG
FROM            dbo.cp_nota_DebCre LEFT OUTER JOIN
                         dbo.vwcp_orden_giro_total_pagodo ON dbo.cp_nota_DebCre.IdEmpresa = dbo.vwcp_orden_giro_total_pagodo.IdEmpresa_cxp AND 
                         dbo.cp_nota_DebCre.IdCbteCble_Nota = dbo.vwcp_orden_giro_total_pagodo.IdCbteCble_cxp AND 
                         dbo.cp_nota_DebCre.IdTipoCbte_Nota = dbo.vwcp_orden_giro_total_pagodo.IdTipoCbte_cxp