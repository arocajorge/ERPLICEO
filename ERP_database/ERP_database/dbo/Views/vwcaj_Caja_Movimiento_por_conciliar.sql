CREATE VIEW [dbo].[vwcaj_Caja_Movimiento_por_conciliar]
AS
SELECT dbo.caj_Caja_Movimiento.IdEmpresa, dbo.caj_Caja_Movimiento.IdTipocbte, dbo.caj_Caja_Movimiento.IdCbteCble, dbo.caj_Caja_Movimiento.IdCaja, dbo.caj_Caja_Movimiento.IdTipoMovi, dbo.caj_Caja_Movimiento_Tipo.tm_descripcion, 
                  dbo.caj_Caja_Movimiento.cm_valor, dbo.caj_Caja_Movimiento.cm_observacion, dbo.caj_Caja_Movimiento.cm_fecha, dbo.tb_persona.pe_nombreCompleto, caj_Caja_Movimiento.IdTipo_Persona, caj_Caja_Movimiento.IdEntidad,
				  caj_Caja_Movimiento.IdPersona
FROM     dbo.caj_Caja_Movimiento INNER JOIN
                  dbo.caj_Caja_Movimiento_Tipo ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_Tipo.IdEmpresa AND dbo.caj_Caja_Movimiento.IdTipoMovi = dbo.caj_Caja_Movimiento_Tipo.IdTipoMovi INNER JOIN
                  dbo.caj_Caja ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.caj_Caja_Movimiento.IdCaja = dbo.caj_Caja.IdCaja INNER JOIN
                  dbo.tb_persona ON dbo.caj_Caja_Movimiento.IdPersona = dbo.tb_persona.IdPersona
WHERE  (dbo.caj_Caja_Movimiento.cm_Signo = N'-') AND (dbo.caj_Caja_Movimiento.Estado = 'A') AND (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      dbo.cp_conciliacion_Caja_det_x_ValeCaja AS F
                       WHERE   (dbo.caj_Caja_Movimiento.IdEmpresa = IdEmpresa_movcaja) AND (dbo.caj_Caja_Movimiento.IdTipocbte = IdTipocbte_movcaja) AND (dbo.caj_Caja_Movimiento.IdCbteCble = IdCbteCble_movcaja)))