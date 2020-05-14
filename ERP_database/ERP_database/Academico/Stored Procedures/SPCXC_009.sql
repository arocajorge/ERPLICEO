--EXEC [Academico].[SPCXC_009] 1,'2020/01/01','2020/12/31'
CREATE PROCEDURE [Academico].[SPCXC_009]
(
@IdEmpresa int,
@FechaIni date,
@FechaFin date
)
AS

select b.IdEmpresa, b.IdSucursal, b.IdCobro, b.secuencial, b.IdBodega_Cbte, b.IdCbte_vta_nota, b.dc_TipoDocumento, b.dc_ValorPago, a.cr_fecha, isnull(g.vt_fecha,h.no_Fecha)vt_fecha,
cast(year(isnull(g.vt_fecha,h.no_Fecha)) as varchar) +' / '+ right('00'+cast(f.idMes as varchar),2) Periodo, d.pe_nombreCompleto, c.Codigo, b.IdCobro_tipo, 'COBROS' AS Tipo, 1 as Orden,
ISNULL(i.OrdenRubros,cast(year(h.no_Fecha) as varchar) +' / '+ right('00'+cast(month(h.no_fecha) as varchar),2)) OrdenRubros, isnull(i.Observacion,'NOTA DE DEBITO '+ cast(year(h.no_Fecha) as varchar) +' / '+ right('00'+cast(month(h.no_fecha) as varchar),2)) Observacion
from cxc_cobro as a inner join
cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
cxc_cobro_tipo as e on b.IdCobro_tipo = e.IdCobro_tipo left join
tb_mes as f on month(a.cr_fecha) = f.idMes left join 
fa_factura as g on b.IdEmpresa = g.IdEmpresa and b.IdSucursal = g.IdSucursal and b.IdBodega_Cbte = g.IdBodega and b.IdCbte_vta_nota = g.IdCbteVta and b.dc_TipoDocumento = g.vt_tipoDoc left join
fa_notaCreDeb as h on b.IdEmpresa = h.IdEmpresa and b.IdSucursal = h.IdSucursal and b.IdBodega_Cbte = h.IdBodega and b.IdCbte_vta_nota = h.IdNota and b.dc_TipoDocumento = h.CodDocumentoTipo LEFT JOIN
(
	select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdCbteVta, CAST(YEAR(D.FechaDesde) AS VARCHAR)+' / '+ RIGHT('00'+CAST(d.IdMes AS VARCHAR),2) OrdenRubros, c.NomRubro+' '+
	case when c.NumeroCuotas <= 1 then
	''
	else cast(e.Secuencia as varchar)+'/' + cast(c.NumeroCuotas as varchar) end Observacion
	from aca_Matricula_Rubro as a inner join
	aca_Matricula as b on a.IdEmpresa = b.IdEmpresa and a.IdMatricula = b.IdMatricula left join
	aca_AnioLectivo_Rubro as c on a.IdEmpresa = c.IdEmpresa and a.IdAnio = c.IdAnio and a.IdRubro = c.IdRubro left join
	aca_AnioLectivo_Periodo as d on a.IdEmpresa = d.IdEmpresa and a.IdAnio = d.IdAnio and a.IdPeriodo = d.IdPeriodo left join
	aca_AnioLectivo_Rubro_Periodo as e on a.IdEmpresa= e.IdEmpresa and a.IdAnio = e.IdAnio and a.IdRubro = e.IdRubro and a.IdPeriodo = e.IdPeriodo
	where a.IdCbteVta is not null
) AS I on G.IdEmpresa= i.IdEmpresa and G.IdSucursal = i.IdSucursal and G.IdBodega = i.IdBodega and G.IdCbteVta = I.IdCbteVta
where a.cr_estado = 'A' AND B.estado = 'A' and b.IdCobro_tipo in ('CHQF','CHQV','DEPO','EFEC','TARJ') and a.cr_fecha between @FechaIni and @FechaFin
UNION ALL
select a.IdEmpresa, a.IdSucursal, a.IdCobro, 1, null, null, null, a.cr_TotalCobro, a.cr_fecha, a.cr_fecha, cast(year(a.cr_fecha) as varchar) +' / '+ right('00'+cast(b.idMes as varchar),2) Periodo,
d.pe_nombreCompleto, c.Codigo, a.IdCobro_tipo, 'PAGOS ANTICIPADOS', 2 as Orden, 'PAGOS ANTICIPADOS '+cast(year(a.cr_fecha) as varchar) +' / '+ right('00'+cast(b.idMes as varchar),2),'PAGOS ANTICIPADOS '+cast(year(a.cr_fecha) as varchar) +' / '+ right('00'+cast(b.idMes as varchar),2)
from cxc_cobro as a inner join
tb_mes as b on b.idMes = month(a.cr_fecha) inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona
WHERE NOT EXISTS(
	SELECT f.IdEmpresa FROM cxc_cobro_det as f
	where a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdCobro = f.IdCobro
) AND a.cr_estado = 'A' AND A.IdCobro_tipo in ('CHQF','CHQV','DEPO','EFEC','TARJ') and a.cr_fecha between @FechaIni and @FechaFin