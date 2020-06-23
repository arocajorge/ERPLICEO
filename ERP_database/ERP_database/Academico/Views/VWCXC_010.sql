CREATE VIEW Academico.VWCXC_010
AS
select a.IdEmpresa, 'FACTURA' Tipo, a.IdCbteVta, 1 as Orden, a.vt_fecha, a.vt_Observacion, a.vt_serie1+'-'+a.vt_serie2+'-'+ a.vt_NumFactura Referencia, 
year(a.vt_fecha) Anio, month(a.vt_fecha)IdMes, e.smes, b.Total AS Debe, 0 as Haber, b.Total as Valor, d.pe_nombreCompleto, c.Codigo as CodigoAlumno, a.IdAlumno
from fa_factura as a inner join
fa_factura_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdCbteVta = b.IdCbteVta inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
tb_mes as e on month(a.vt_fecha) = e.idMes
where a.Estado = 'A' 
UNION ALL

select a.IdEmpresa, CASE WHEN A.CreDeb = 'C' THEN 'NOTA DE CREDITO' ELSE 'NOTA DE DEBITO' END Tipo, a.IdNota, 1 as Orden, a.no_fecha, a.sc_observacion, 
CASE WHEN A.NaturalezaNota = 'SRI' THEN
a.Serie1+'-'+a.Serie2+'-'+ a.NumNota_Impresa ELSE
ISNULL(A.CodNota, CAST(A.IdNota AS VARCHAR))
END Referencia, 
year(a.no_fecha) Anio, month(a.no_fecha)IdMes, e.smes, CASE WHEN A.CreDeb = 'C' THEN 0 ELSE b.Total END AS Debe, CASE WHEN A.CreDeb = 'C' THEN b.Total ELSE 0 END as Haber, 
CASE WHEN A.CreDeb = 'C' THEN b.Total *-1 ELSE b.Total END as Valor, d.pe_nombreCompleto, c.Codigo as CodigoAlumno, a.IdAlumno
from fa_notaCreDeb as a inner join
fa_notaCreDeb_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdNota = b.IdNota inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
tb_mes as e on month(a.no_fecha) = e.idMes
where a.Estado = 'A'


UNION ALL
select a.IdEmpresa, 
	CASE WHEN SUM(B.dc_ValorProntoPago) IS NULL THEN 'PAGO ANTICIPADO' 
	WHEN F.IdCobro IS NOT NULL THEN 'CONCILIACION NC' ELSE
	CASE WHEN A.IdCobro_tipo = 'NTCR' THEN 'NOTA DE CREDITO' ELSE 'PAGO' END
END
Tipo, a.IdCobro, 2 as Orden, a.cr_fecha, a.cr_observacion +' '+isnull(a.cr_ObservacionPantalla,''), cast(a.IdCobro as varchar) Referencia, 
year(a.cr_fecha) Anio, month(a.cr_fecha)IdMes, e.smes, 0 AS Debe,isnull(sum(b.dc_ValorPago),a.cr_TotalCobro) as Haber, isnull(sum(b.dc_ValorPago) *-1,a.cr_TotalCobro) as Valor, d.pe_nombreCompleto, c.Codigo as CodigoAlumno, a.IdAlumno
from cxc_cobro as a INNER join
cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
tb_mes as e on month(a.cr_fecha) = e.idMes LEFT JOIN
cxc_ConciliacionNotaCredito AS f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdCobro = f.IdCobro
where a.cr_estado = 'A' AND NOT EXISTS(
	SELECT A1.IdEmpresa FROM fa_notaCreDeb AS A1 INNER JOIN
	fa_notaCreDeb_resumen AS B1 ON A1.IdEmpresa = B1.IdEmpresa AND A1.IdSucursal = B1.IdSucursal AND A1.IdBodega = B1.IdBodega AND A1.IdNota = B1.IdNota INNER JOIN
	fa_notaCreDeb_x_cxc_cobro AS C1 ON A1.IdEmpresa = C1.IdEmpresa_nt AND A1.IdSucursal = C1.IdSucursal_nt AND A1.IdBodega = C1.IdBodega_nt AND A1.IdNota = C1.IdNota_nt INNER JOIN
	cxc_cobro AS D1 ON C1.IdEmpresa_cbr = D1.IdEmpresa AND C1.IdSucursal_cbr = D1.IdSucursal AND C1.IdCobro_cbr = D1.IdCobro
	WHERE DBO.BankersRounding(B1.Total - C1.Valor_cobro,2) = 0 AND A1.no_fecha = D1.cr_fecha
	AND A.IdEmpresa = D1.IdEmpresa 
	AND A.IdSucursal = D1.IdSucursal
	AND A.IdCobro = D1.IdCobro
)
group by a.IdEmpresa, a.IdCobro, a.cr_fecha, a.cr_observacion +' '+isnull(a.cr_ObservacionPantalla,''), cast(a.IdCobro as varchar), 
year(a.cr_fecha), month(a.cr_fecha), e.smes, d.pe_nombreCompleto, c.Codigo, a.IdAlumno, a.cr_TotalCobro, a.IdCobro_tipo, F.IdCobro