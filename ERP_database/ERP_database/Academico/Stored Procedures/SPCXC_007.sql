--EXEC Academico.SPFAC_007 1 ,'2020/4/20'
CREATE PROCEDURE Academico.SPCXC_007
(
@IdEmpresa int,
@FechaCorte date
)
AS

select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdCbteVta, a.vt_fecha, a.vt_Observacion, A.vt_serie1+'-'+A.vt_serie2+'-'+ A.vt_NumFactura vt_NumFactura, c.IdAlumno, c.Codigo as CodigoAlumno, d.pe_nombreCompleto, b.IdAnio, year(isnull(e.FechaDesde,a.vt_fecha)) as Periodo, b.Total, isnull(f.dc_ValorPago,0) as TotalPagado, round(b.Total - isnull(f.dc_ValorPago,0),2) as Saldo,
a.vt_fech_venc, DATEDIFF(day,a.vt_fech_venc,@FechaCorte) as Plazo,
CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) <= 30 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_0_30,
CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) BETWEEN 31 AND 60 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_31_60,
CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) BETWEEN 61 AND 90 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_61_90,
CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) BETWEEN 91 AND 180 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_91_180,
CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) >= 181 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_181
from fa_factura as a inner join
fa_factura_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdCbteVta = b.IdCbteVta inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
aca_AnioLectivo as e on b.IdEmpresa = e.IdEmpresa and b.IdAnio = e.IdAnio left join
(
	select B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento, SUM(B.dc_ValorPago) dc_ValorPago 
	from cxc_cobro as a inner join 
	cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro
	where a.cr_estado = 'A' and a.IdEmpresa = @IdEmpresa and a.cr_fecha <= @FechaCorte
	group by B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento
) as f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdBodega = f.IdBodega_Cbte and a.IdCbteVta = f.IdCbte_vta_nota and a.vt_tipoDoc = f.dc_TipoDocumento
where a.IdEmpresa = @IdEmpresa and a.Estado = 'A' AND a.vt_fecha <= @FechaCorte and round(b.Total - isnull(f.dc_ValorPago,0),2) > 0
UNION ALL
select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, a.no_fecha, a.sc_observacion, CASE WHEN A.NaturalezaNota = 'SRI' THEN A.Serie1+'-'+A.Serie2+'-'+ A.NumNota_Impresa ELSE ISNULL(A.CodNota,cast(A.IdNota as varchar)) END NumNota_Impresa  ,c.IdAlumno, c.Codigo as CodigoAlumno, d.pe_nombreCompleto, b.IdAnio, year(isnull(e.FechaDesde,a.no_fecha)) as Periodo, b.Total, isnull(f.dc_ValorPago,0) as TotalPagado, round(b.Total - isnull(f.dc_ValorPago,0),2) as Saldo,
a.no_fecha_venc, DATEDIFF(day,a.no_fecha_venc,@FechaCorte) as Plazo,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) <= 30 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_0_30,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) BETWEEN 31 AND 60 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_31_60,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) BETWEEN 61 AND 90 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_61_90,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) BETWEEN 91 AND 180 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_91_180,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) >= 181 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_181
from fa_notaCreDeb as a inner join
fa_notaCreDeb_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdNota = b.IdNota inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
aca_AnioLectivo as e on b.IdEmpresa = e.IdEmpresa and b.IdAnio = e.IdAnio left join
(
	select B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento, SUM(B.dc_ValorPago) dc_ValorPago 
	from cxc_cobro as a inner join 
	cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro
	where a.cr_estado = 'A' and a.IdEmpresa = @IdEmpresa and a.cr_fecha <= @FechaCorte
	group by B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento
) as f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdBodega = f.IdBodega_Cbte and a.IdNota = f.IdCbte_vta_nota and a.CodDocumentoTipo = f.dc_TipoDocumento
where a.IdEmpresa = @IdEmpresa and a.Estado = 'A' AND a.no_fecha <= @FechaCorte and round(b.Total - isnull(f.dc_ValorPago,0),2) > 0 and a.CreDeb = 'D'