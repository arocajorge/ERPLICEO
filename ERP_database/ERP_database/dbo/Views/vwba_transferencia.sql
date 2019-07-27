create view [dbo].[vwba_transferencia]
as
SELECT     dbo.ba_transferencia.IdTransferencia, dbo.ba_transferencia.IdEmpresa_origen, dbo.ba_transferencia.IdCbteCble_origen, dbo.ba_transferencia.IdTipocbte_origen, 
                      dbo.ba_transferencia.IdEmpresa_destino, dbo.ba_transferencia.IdCbteCble_destino, dbo.ba_transferencia.IdTipocbte_destino, dbo.ba_transferencia.IdBanco_destino, 
                      dbo.ba_transferencia.IdBanco_origen, dbo.ba_transferencia.tr_observacion, dbo.ba_transferencia.tr_valor, dbo.ba_transferencia.tr_fecha, 
                      dbo.ba_transferencia.tr_estado, dbo.ba_transferencia.IdUsuario, dbo.ba_transferencia.IdUsuario_Anu, dbo.ba_transferencia.Fecha_Transac, 
                      dbo.ba_transferencia.FechaAnulacion, dbo.ba_transferencia.Fecha_UltMod, dbo.ba_transferencia.IdUsuarioUltMod, dbo.ba_transferencia.ip, 
                      dbo.ba_transferencia.nom_pc, dbo.ba_transferencia.tr_MotivoAnulacion, dbo.tb_empresa.em_nombre AS NEmpresaOrigen, 
                      tb_empresa_1.em_nombre AS NEmpresaDestino, dbo.ba_Banco_Cuenta.ba_descripcion AS NBancoOrigen, 
                      ba_Banco_Cuenta_1.ba_descripcion AS NBancoDestino
FROM         dbo.tb_empresa INNER JOIN
                      dbo.ba_transferencia ON dbo.tb_empresa.IdEmpresa = dbo.ba_transferencia.IdEmpresa_origen INNER JOIN
                      dbo.ba_Banco_Cuenta ON dbo.ba_transferencia.IdEmpresa_origen = dbo.ba_Banco_Cuenta.IdEmpresa AND 
                      dbo.ba_transferencia.IdBanco_origen = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                      dbo.ba_Banco_Cuenta AS ba_Banco_Cuenta_1 ON dbo.ba_transferencia.IdEmpresa_destino = ba_Banco_Cuenta_1.IdEmpresa AND 
                      dbo.ba_transferencia.IdBanco_destino = ba_Banco_Cuenta_1.IdBanco INNER JOIN
                      dbo.tb_empresa AS tb_empresa_1 ON dbo.ba_transferencia.IdEmpresa_destino = tb_empresa_1.IdEmpresa