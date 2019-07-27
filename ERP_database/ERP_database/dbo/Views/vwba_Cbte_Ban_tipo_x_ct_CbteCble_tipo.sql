CREATE view vwba_Cbte_Ban_tipo_x_ct_CbteCble_tipo
as
SELECT        B.IdEmpresa, B.CodTipoCbteBan,B.nom_TipoCbteBan, A.IdTipoCbteCble, A.IdTipoCbteCble_Anu, A.IdCtaCble, A.Tipo_DebCred
FROM            ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo AS A RIGHT OUTER JOIN
                             (
							 SELECT        tb_empresa.IdEmpresa, ba_Cbte_Ban_tipo.CodTipoCbteBan,ba_Cbte_Ban_tipo.Descripcion nom_TipoCbteBan
                               FROM            ba_Cbte_Ban_tipo CROSS JOIN
                                                         tb_empresa
														 
														 ) AS B ON A.IdEmpresa = B.IdEmpresa AND A.CodTipoCbteBan = B.CodTipoCbteBan