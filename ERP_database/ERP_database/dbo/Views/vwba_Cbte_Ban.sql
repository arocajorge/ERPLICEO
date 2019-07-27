CREATE view [dbo].[vwba_Cbte_Ban]
as
SELECT        B.tc_TipoCbte, C.ba_descripcion, '' AS NombreProveedor, Ba.IdEmpresa, Ba.IdCbteCble, Ba.IdTipocbte, Ba.Cod_Cbtecble, Ba.IdPeriodo, Ba.IdBanco, Ba.IdEntidad IdProveedor, Ba.cb_Fecha, Ba.cb_Observacion, 
                         0 cb_secuencia, Ba.cb_Valor, Ba.cb_Cheque, 'N'cb_ChequeImpreso, GETDATE() cb_FechaCheque, Ba.IdUsuario, Ba.IdUsuario_Anu, Ba.FechaAnulacion, Ba.Fecha_Transac, Ba.Fecha_UltMod, Ba.IdUsuarioUltMod, 
                         Ba.Estado, Ba.MotivoAnulacion, ''ip, ''nom_pc, Ba.cb_giradoA, Ba.cb_ciudadChq, null IdCbteCble_Anulacion, null IdTipoCbte_Anulacion, Ba.IdTipoFlujo, Ba.IdTipoNota, 
                         dbo.ba_tipo_nota.Descripcion AS NomTipoNota, 'N'Por_Anticipo, 'N'PosFechado, Ba.IdPersona_Girado_a, Ba.ValorEnLetras, Ba.IdSucursal, ISNULL(Ba.IdEstado_Cbte_Ban_cat, '') AS IdEstado_Cbte_Ban_cat, 
                         dbo.vwba_Estado_cbte_ban.ca_descripcion AS nom_Estado_Cbte_Ban, MAX(CASE WHEN dbo.tb_persona.pe_nombreCompleto IS NULL AND tb_persona_1.pe_nombreCompleto IS NULL 
                         THEN tb_persona_2.pe_nombreCompleto WHEN dbo.tb_persona.pe_nombreCompleto IS NULL AND tb_persona_1.pe_nombreCompleto IS NOT NULL 
                         THEN tb_persona_1.pe_nombreCompleto ELSE dbo.tb_persona.pe_nombreCompleto END) AS Beneficiario, tb_persona_2.IdTipoDocumento, tb_persona_2.pe_cedulaRuc, 
                         dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan, 
						 ''--ISNULL(dbo.vwba_Banco_Estado_Cheques.Estado_Conciliacion, 'NO CONCILIADO') 
						 AS Estado_Conciliacion, 
                         CASE WHEN Ba.IdEstado_Preaviso_ch_cat = '' THEN 'ES_CH_XPREAVISO_CH' ELSE isnull(Ba.IdEstado_Preaviso_ch_cat, 'ES_CH_XPREAVISO_CH') END AS IdEstado_Preaviso_ch_cat, 
                         Ba.IdEstado_cheque_cat
FROM            dbo.caj_Caja_Movimiento INNER JOIN
                         dbo.tb_persona ON dbo.caj_Caja_Movimiento.IdPersona = dbo.tb_persona.IdPersona RIGHT OUTER JOIN
                         dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito ON dbo.caj_Caja_Movimiento.IdTipocbte = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdTipocbte AND 
                         dbo.caj_Caja_Movimiento.IdCbteCble = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdCbteCble AND 
                         dbo.caj_Caja_Movimiento.IdEmpresa = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdEmpresa RIGHT OUTER JOIN
                         dbo.ba_Banco_Cuenta AS C RIGHT OUTER JOIN
                         dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo INNER JOIN
                         dbo.ba_Cbte_Ban AS Ba ON dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa = Ba.IdEmpresa AND dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble = Ba.IdTipocbte INNER JOIN
                         dbo.ct_cbtecble_tipo AS B ON Ba.IdTipocbte = B.IdTipoCbte AND Ba.IdEmpresa = B.IdEmpresa LEFT OUTER JOIN
                         dbo.tb_persona AS tb_persona_1 INNER JOIN
                         dbo.cp_orden_pago INNER JOIN
                         dbo.cp_orden_pago_cancelaciones ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND 
                         dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op ON tb_persona_1.IdPersona = dbo.cp_orden_pago.IdPersona ON 
                         Ba.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago AND Ba.IdCbteCble = dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago AND 
                         Ba.IdTipocbte = dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago LEFT OUTER JOIN
                         dbo.tb_persona AS tb_persona_2 ON Ba.IdPersona_Girado_a = tb_persona_2.IdPersona ON C.IdEmpresa = Ba.IdEmpresa AND C.IdBanco = Ba.IdBanco LEFT OUTER JOIN
                         dbo.vwba_Banco_Estado_Cheques ON Ba.IdEmpresa = dbo.vwba_Banco_Estado_Cheques.IdEmpresa AND Ba.IdCbteCble = dbo.vwba_Banco_Estado_Cheques.IdCbteCble AND 
                         Ba.IdTipocbte = dbo.vwba_Banco_Estado_Cheques.IdTipocbte LEFT OUTER JOIN
                         dbo.vwba_Estado_cbte_ban ON Ba.IdEstado_Cbte_Ban_cat = dbo.vwba_Estado_cbte_ban.IdEstado_cbte_ban LEFT OUTER JOIN
                         dbo.ba_tipo_nota ON Ba.IdEmpresa = dbo.ba_tipo_nota.IdEmpresa AND Ba.IdTipoNota = dbo.ba_tipo_nota.IdTipoNota ON dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdEmpresa = Ba.IdEmpresa AND
                          dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdCbteCble = Ba.IdCbteCble AND dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdTipocbte = Ba.IdTipocbte
GROUP BY B.tc_TipoCbte, C.ba_descripcion, Ba.IdEmpresa, Ba.IdCbteCble, Ba.IdTipocbte, Ba.Cod_Cbtecble, Ba.IdPeriodo, Ba.IdBanco, Ba.IdEntidad, Ba.cb_Fecha, Ba.cb_Observacion, Ba.cb_Valor, 
                         Ba.cb_Cheque,  Ba.IdUsuario, Ba.IdUsuario_Anu, Ba.FechaAnulacion, Ba.Fecha_Transac, Ba.Fecha_UltMod, Ba.IdUsuarioUltMod, Ba.Estado, Ba.MotivoAnulacion, 
                          Ba.cb_giradoA, Ba.cb_ciudadChq,  Ba.IdTipoFlujo, Ba.IdTipoNota, dbo.ba_tipo_nota.Descripcion, 
                         Ba.IdPersona_Girado_a, Ba.ValorEnLetras, Ba.IdSucursal, ISNULL(Ba.IdEstado_Cbte_Ban_cat, ''), dbo.vwba_Estado_cbte_ban.ca_descripcion, tb_persona_2.IdTipoDocumento, tb_persona_2.pe_cedulaRuc, 
                         dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan, Ba.IdEstado_cheque_cat, Ba.IdEstado_Preaviso_ch_cat