CREATE VIEW [dbo].[vwba_TransaccionesAConciliar]
AS
SELECT ISNULL(ROW_NUMBER() OVER (ORDER BY A.IdEmpresa), 0) AS IdRow, A.*
FROM     (SELECT ba_Conciliacion.IdEmpresa, ba_Conciliacion.IdConciliacion, ba_Banco_Cuenta.IdBanco, ba_Banco_Cuenta.IdCtaCble, ba_Banco_Cuenta.ba_descripcion, ct_cbtecble_det.dc_Observacion, ct_cbtecble.cb_Fecha, 
                                    ct_cbtecble_tipo.tc_TipoCbte AS nom_IdTipoCbte, ct_cbtecble_det.dc_Valor, ba_Conciliacion.co_Fecha AS fechaConciliacion, ba_Conciliacion.IdEstado_Concil_Cat, ba_Conciliacion_det_IngEgr.checked, 
                                    ct_cbtecble_det.IdTipoCbte, ct_cbtecble_det.IdCbteCble, ct_cbtecble_det.secuencia, ba_Cbte_Ban.cb_Cheque, ba_Conciliacion.co_SaldoBanco_anterior
                  FROM      ba_Banco_Cuenta INNER JOIN
                                    ba_Conciliacion ON ba_Banco_Cuenta.IdEmpresa = ba_Conciliacion.IdEmpresa AND ba_Banco_Cuenta.IdBanco = ba_Conciliacion.IdBanco INNER JOIN
                                    ba_Conciliacion_det_IngEgr ON ba_Conciliacion.IdEmpresa = ba_Conciliacion_det_IngEgr.IdEmpresa AND ba_Conciliacion.IdConciliacion = ba_Conciliacion_det_IngEgr.IdConciliacion INNER JOIN
                                    ct_cbtecble_det INNER JOIN
                                    ct_cbtecble ON ct_cbtecble_det.IdEmpresa = ct_cbtecble.IdEmpresa AND ct_cbtecble_det.IdTipoCbte = ct_cbtecble.IdTipoCbte AND ct_cbtecble_det.IdCbteCble = ct_cbtecble.IdCbteCble ON 
                                    ba_Conciliacion_det_IngEgr.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ba_Conciliacion_det_IngEgr.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND ba_Conciliacion_det_IngEgr.IdCbteCble = ct_cbtecble_det.IdCbteCble AND 
                                    ba_Conciliacion_det_IngEgr.SecuenciaCbteCble = ct_cbtecble_det.secuencia INNER JOIN
                                    ct_cbtecble_tipo ON ct_cbtecble.IdEmpresa = ct_cbtecble_tipo.IdEmpresa and ct_cbtecble.IdTipoCbte = ct_cbtecble_tipo.IdTipoCbte LEFT OUTER JOIN
                                    ba_Cbte_Ban ON ct_cbtecble.IdEmpresa = ba_Cbte_Ban.IdEmpresa AND ct_cbtecble.IdTipoCbte = ba_Cbte_Ban.IdTipocbte AND ct_cbtecble.IdCbteCble = ba_Cbte_Ban.IdCbteCble
                  WHERE   (NOT EXISTS
                                        (SELECT R.IdEmpresa
                                         FROM      ct_cbtecble_Reversado AS R
                                         WHERE   R.IdEmpresa = ct_cbtecble_det.IdEmpresa AND R.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND r.IdCbteCble = ct_cbtecble_det.IdCbteCble) AND ba_Conciliacion_det_IngEgr.checked = 1)
                  UNION
                  SELECT ct_cbtecble.IdEmpresa, 0 AS IdConciliacion, ba_Banco_Cuenta.IdBanco, ba_Banco_Cuenta.IdCtaCble, ba_Banco_Cuenta.ba_descripcion, ct_cbtecble_det.dc_Observacion, ct_cbtecble.cb_Fecha, ct_cbtecble_tipo.tc_TipoCbte, 
                                    ct_cbtecble_det.dc_Valor, NULL AS FechaConciliacion, 'X_CONCILIAR' AS IdEstado_Concil_Cat, 0 AS checked, ct_cbtecble_det.IdTipoCbte, ct_cbtecble_det.IdCbteCble, ct_cbtecble_det.secuencia, ba_Cbte_Ban.cb_Cheque, 
                                    0
                  FROM     ct_cbtecble_det INNER JOIN
                                    ct_cbtecble ON ct_cbtecble_det.IdEmpresa = ct_cbtecble.IdEmpresa AND ct_cbtecble_det.IdTipoCbte = ct_cbtecble.IdTipoCbte AND ct_cbtecble_det.IdCbteCble = ct_cbtecble.IdCbteCble INNER JOIN
                                    ct_cbtecble_tipo ON ct_cbtecble.IdEmpresa = ct_cbtecble_tipo.IdEmpresa and ct_cbtecble.IdTipoCbte = ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                                    ba_Banco_Cuenta ON ct_cbtecble_det.IdEmpresa = ba_Banco_Cuenta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ba_Banco_Cuenta.IdCtaCble LEFT OUTER JOIN
                                    ba_Cbte_Ban ON ct_cbtecble.IdEmpresa = ba_Cbte_Ban.IdEmpresa AND ct_cbtecble.IdTipoCbte = ba_Cbte_Ban.IdTipocbte AND ct_cbtecble.IdCbteCble = ba_Cbte_Ban.IdCbteCble
                  WHERE  (NOT EXISTS
                                        (SELECT A.IdEmpresa
                                         FROM      ba_Conciliacion_det_IngEgr AS A, ba_Conciliacion B, ct_cbtecble_Reversado AS R
                                         WHERE   A.IdEmpresa = B.IdEmpresa AND A.IdConciliacion = B.IdConciliacion AND (A.IdEmpresa = ct_cbtecble.IdEmpresa) AND (A.IdTipocbte = ct_cbtecble.IdTipoCbte) AND (A.IdCbteCble = ct_cbtecble.IdCbteCble) AND 
                                                           B.Estado = 'A' AND R.IdEmpresa = ct_cbtecble_det.IdEmpresa AND R.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND r.IdCbteCble = ct_cbtecble_det.IdCbteCble)) AND NOT EXISTS
                                        (SELECT A.IdEmpresa
                                         FROM      ba_Conciliacion_det_IngEgr A
                                         WHERE   A.IdEmpresa = ct_cbtecble_det.IdEmpresa AND A.IdTipocbte = ct_cbtecble_det.IdTipoCbte AND A.IdCbteCble = ct_cbtecble_det.IdCbteCble AND a.SecuenciaCbteCble = ct_cbtecble_det.secuencia AND a.checked = 1) 
                                    AND NOT EXISTS
                                        (SELECT A.IdEmpresa
                                         FROM      ct_cbtecble_Reversado A
                                         WHERE   A.IdEmpresa_Anu = ct_cbtecble_det.IdEmpresa AND A.IdTipoCbte_Anu = ct_cbtecble_det.IdTipoCbte AND A.IdCbteCble_Anu = ct_cbtecble_det.IdCbteCble) AND isnull(ct_cbtecble_det.dc_para_conciliar, 0) = 1 AND 
                                    ct_cbtecble.cb_Estado = 'A') A