

CREATE view [dbo].[vwba_Conciliacion_det_IngEgr]  
as  
SELECT     A.IdEmpresa, A.IdCbteCble, A.IdTipoCbte, J.CodTipoCbte, J.tc_TipoCbte, A2.CodTipoCbteBan, A2.Descripcion, A.IdPeriodo, A_1.IdBanco, A.cb_Fecha,   
                      A.cb_Observacion, A_1.cb_Cheque, GETDATE() cb_FechaCheque, I.ba_descripcion, H.IdCtaCble, H.pc_Cuenta, C.dc_Valor, A_1.Estado, C.secuencia AS SecuenciaCbteCble,   
                      A2.CodTipoCbteBan + ' - ' + A2.Descripcion AS ReferenciaCbte, co.IdConciliacion ,co.Secuencia as SecuenciaConciliacion,co.tipo_IngEgr  
FROM  ba_Banco_Cuenta AS I   
   INNER JOIN ct_cbtecble_det AS C ON I.IdCtaCble = C.IdCtaCble AND I.IdEmpresa = C.IdEmpresa   
   INNER JOIN ct_plancta AS H ON C.IdEmpresa = H.IdEmpresa AND C.IdCtaCble = H.IdCtaCble   
   INNER JOIN ct_cbtecble AS A ON C.IdEmpresa = A.IdEmpresa AND C.IdTipoCbte = A.IdTipoCbte AND C.IdCbteCble = A.IdCbteCble   
   INNER JOIN ct_cbtecble_tipo AS J ON A.IdTipoCbte = J.IdTipoCbte  
   INNER JOIN ba_Conciliacion_det_IngEgr co on co.IdEmpresa=C.IdEmpresa and co.IdCbteCble=C.IdCbteCble and co.IdTipocbte=C.IdTipoCbte and co.SecuenciaCbteCble=C.secuencia  
   left join ba_Cbte_Ban as A_1 on A_1.IdTipocbte=C.IdTipoCbte and A_1.IdCbteCble=C.IdCbteCble and A_1.IdEmpresa=C.IdEmpresa   
   left join ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo AS B ON B.IdTipoCbteCble=A_1.IdTipocbte and B.IdEmpresa=A_1.IdEmpresa   
   left join ba_Cbte_Ban_tipo as A2 on A2.CodTipoCbteBan = B.CodTipoCbteBan