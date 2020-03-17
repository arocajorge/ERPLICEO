create PROCEDURE Academico.SPACA_ContabilizacionNotas
(
@IdEmpresa int,
@FechaIni date,
@FechaFin date
)
AS
SELECT a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, a.no_fecha, a.sc_observacion, c.pe_nombreCompleto, e.No_Descripcion, rel.ct_IdEmpresa, rel.ct_IdTipoCbte, rel.ct_IdCbteCble,
d.Total AS TotalModulo, sum(isnull(ct.dc_Valor,0)) as TotalContabilidad, d.Total - sum(isnull(ct.dc_Valor,0)) Saldo
FROM     ct_cbtecble_det AS ct INNER JOIN
                  fa_notaCreDeb_x_ct_cbtecble AS rel ON ct.IdEmpresa = rel.ct_IdEmpresa AND ct.IdTipoCbte = rel.ct_IdTipoCbte AND ct.IdCbteCble = rel.ct_IdCbteCble RIGHT OUTER JOIN
                  fa_notaCreDeb AS a INNER JOIN
                  aca_Alumno AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdAlumno = b.IdAlumno INNER JOIN
                  tb_persona AS c ON b.IdPersona = c.IdPersona INNER JOIN
                  fa_notaCreDeb_resumen AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdSucursal = d.IdSucursal AND a.IdBodega = d.IdBodega AND a.IdNota = d.IdNota INNER JOIN
                  fa_TipoNota AS e ON a.IdEmpresa = e.IdEmpresa AND a.IdTipoNota = e.IdTipoNota ON rel.no_IdEmpresa = a.IdEmpresa AND rel.no_IdSucursal = a.IdSucursal AND rel.no_IdBodega = a.IdBodega AND rel.no_IdNota = a.IdNota
where a.Estado = 'A' and a.CreDeb = 'C' AND ISNULL(CT.dc_Valor,1) > 0.01 and a.IdEmpresa = @IdEmpresa and a.no_fecha between @FechaIni and @FechaFin 
group by a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, a.no_fecha, a.sc_observacion, c.pe_nombreCompleto, e.No_Descripcion, rel.ct_IdEmpresa, rel.ct_IdTipoCbte, rel.ct_IdCbteCble, d.Total