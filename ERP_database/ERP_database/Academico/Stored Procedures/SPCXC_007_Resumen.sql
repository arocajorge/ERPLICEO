
--exec [Academico].[SPCXC_007_Resumen] 1,'01/01/2021'
CREATE PROCEDURE [Academico].[SPCXC_007_Resumen]
(
@IdEmpresa int,
@FechaCorte date
)
AS
SET ANSI_NULLS ON
declare @w_IdEmpresa int = @IdEmpresa,
@w_FechaCorte DATE = @FechaCorte
SELECT a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdCbteVta, a.vt_tipoDoc, b.Total, ISNULL(c.dc_ValorPago, 0) AS dc_ValorPago, dbo.BankersRounding(b.Total - ISNULL(c.dc_ValorPago, 0), 2) AS Saldo, e.IdAnio, e.IdSede, e.IdJornada, e.IdNivel, 
                  e.IdCurso, e.IdParalelo, i.NomSede, right('00'+cast(h.OrdenJornada as varchar),2)+' '+h.NomJornada as NomJornada,
				  right('00'+ cast(i.OrdenNivel as varchar),2)+' '+i.NomNivel as NomNivel, year(f.FechaDesde) Anio, right('00'+cast(d.IdPeriodo as varchar),2) +' '+ g.NomRubro +' '+ case when g.NumeroCuotas > 1 then k.smes else '' end as NomRubro, 1 as Cantidad
FROM     (SELECT XX.IdEmpresa, XX.IdSucursal, XX.IdBodega_Cbte, XX.IdCbte_vta_nota, XX.dc_TipoDocumento, SUM(XX.dc_ValorPago) AS dc_ValorPago 
                  FROM      cxc_cobro AS X INNER JOIN
                                    cxc_cobro_det AS XX ON X.IdEmpresa = XX.IdEmpresa AND X.IdSucursal = XX.IdSucursal AND X.IdCobro = XX.IdCobro
                  WHERE   (X.IdEmpresa = @w_IdEmpresa) AND (X.cr_estado = 'A') and x.cr_fecha <= @w_FechaCorte
                  GROUP BY XX.IdEmpresa, XX.IdSucursal, XX.IdBodega_Cbte, XX.IdCbte_vta_nota, XX.dc_TipoDocumento) AS c RIGHT OUTER JOIN
                  aca_Matricula AS e LEFT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS h ON e.IdEmpresa = h.IdEmpresa AND e.IdAnio = h.IdAnio AND e.IdSede = h.IdSede AND e.IdNivel = h.IdNivel AND e.IdJornada = h.IdJornada LEFT OUTER JOIN
                  aca_AnioLectivo_Sede_NivelAcademico AS i ON h.IdEmpresa = i.IdEmpresa AND h.IdAnio = i.IdAnio AND h.IdSede = i.IdSede AND h.IdNivel = i.IdNivel RIGHT OUTER JOIN
                  fa_factura AS a INNER JOIN
                  fa_factura_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta INNER JOIN
                  aca_Matricula_Rubro AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdSucursal = d.IdSucursal AND a.IdBodega = d.IdBodega AND a.IdCbteVta = d.IdCbteVta ON e.IdEmpresa = d.IdEmpresa AND e.IdMatricula = d.IdMatricula ON 
                  c.IdEmpresa = a.IdEmpresa AND c.IdSucursal = a.IdSucursal AND c.IdBodega_Cbte = a.IdBodega AND c.IdCbte_vta_nota = a.IdCbteVta AND c.dc_TipoDocumento = a.vt_tipoDoc left join
				  aca_AnioLectivo as f on f.IdEmpresa = e.IdEmpresa and f.IdAnio = e.IdAnio left join
				  aca_AnioLectivo_Rubro as g on d.IdEmpresa = g.IdEmpresa and d.IdAnio = g.IdAnio and d.IdRubro = g.IdRubro left join
				  aca_AnioLectivo_Periodo as j on d.IdEmpresa = j.IdEmpresa and d.IdAnio = j.IdAnio and d.IdPeriodo = j.IdPeriodo left join
				  tb_mes as k on j.IdMes = k.idMes
WHERE  a.IdEmpresa = @w_IdEmpresa and (a.IdAlumno IS NOT NULL) AND (a.Estado = 'A') AND (dbo.BankersRounding(b.Total - ISNULL(c.dc_ValorPago, 0), 2) > 0) and a.vt_fecha <= @w_FechaCorte
UNION ALL
SELECT a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, a.CodDocumentoTipo, b.Total, ISNULL(c.dc_ValorPago, 0) AS dc_ValorPago, dbo.BankersRounding(b.Total - ISNULL(c.dc_ValorPago, 0), 2) AS Saldo, null IdAnio, null IdSede, null IdJornada, null IdNivel, 
                  null IdCurso, null IdParalelo, null NomSede,null NomJornada, '00 Periodos anteriores' NomNivel, year(a.no_fecha) Anio, '00 NOTA DE DEBITO' sc_observacion, 1 as Cantidad
FROM     fa_notaCreDeb AS a INNER JOIN
                  fa_notaCreDeb_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdNota = b.IdNota left join
				  (SELECT XX.IdEmpresa, XX.IdSucursal, XX.IdBodega_Cbte, XX.IdCbte_vta_nota, XX.dc_TipoDocumento, SUM(XX.dc_ValorPago) AS dc_ValorPago 
                  FROM      cxc_cobro AS X INNER JOIN
                                    cxc_cobro_det AS XX ON X.IdEmpresa = XX.IdEmpresa AND X.IdSucursal = XX.IdSucursal AND X.IdCobro = XX.IdCobro

                  WHERE   (X.IdEmpresa = @W_IdEmpresa AND X.cr_estado = 'A' and x.cr_fecha <= @w_FechaCorte)
                  GROUP BY XX.IdEmpresa, XX.IdSucursal, XX.IdBodega_Cbte, XX.IdCbte_vta_nota, XX.dc_TipoDocumento) as c on a.IdEmpresa = c.IdEmpresa and a.IdSucursal = c.IdSucursal and a.IdBodega = c.IdBodega_Cbte and a.IdNota = c.IdCbte_vta_nota and a.CodDocumentoTipo = c.dc_TipoDocumento 
WHERE  a.IdEmpresa = @w_IdEmpresa and (a.IdAlumno IS NOT NULL) AND (a.Estado = 'A') AND (dbo.BankersRounding(b.Total - ISNULL(c.dc_ValorPago, 0), 2) > 0) and a.CreDeb = 'D' and a.no_fecha <= @w_FechaCorte