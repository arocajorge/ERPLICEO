--exec [Academico].[SPFAC_006] 1,1,1,1,99999,'2020/7/1','2020/7/31',0
CREATE PROCEDURE [Academico].[SPFAC_006]
(
@IdEmpresa int,
@IdSucursalIni int, 
@IdSucursalFin int,
@IdAlumnoIni numeric,
@IdAlumnoFin numeric,
@FechaIni datetime,
@FechaFin datetime,
@MostrarAnulados bit
)
AS
SELECT        c.IdEmpresa, c.IdSucursal, c.IdBodega, c.IdCbteVta, c.vt_serie1 + '-' + c.vt_serie2 + '-' + c.vt_NumFactura AS vt_NumFactura, c.IdAlumno, per.pe_nombreCompleto, cat.Nombre AS NombreFormaPago, c.IdCatalogo_FormaPago, c.Estado, 
                         c.vt_fecha, c.IdUsuario Ve_Vendedor, c.IdUsuario IdVendedor, tb_sucursal.Su_Descripcion, tb_sucursal.Su_Telefonos, tb_sucursal.Su_Direccion, tb_sucursal.Su_Ruc,
						 r.SubtotalIVAConDscto, r.SubtotalSinIVAConDscto, r.ValorIVA, r.Total, isnull(anu.FacturasAnuladas,0)FacturasAnuladas, '['+fp.IdFormaPago+'] '+ fp.nom_FormaPago nom_FormaPago,
						 per.pe_cedulaRuc, c.vt_Observacion, 'Facturas IVA '+case when r.ValorIVA > 0 then '12' else '0' end+'%' Tarifa, m.IdJornada, nj.OrdenJornada, nj.NomJornada, m.IdNivel, sn.OrdenNivel,cast(sn.OrdenNivel as varchar)+' '+ sn.NomNivel NomNivel, ar.NomRubro, cast(cast(year(c.vt_fecha) as varchar)+right('00'+cast(MONTH(c.vt_fecha) as varchar),2) as int) idMes, mes.smes,
						 ar.NomRubro+' ' +case when ar.NumeroCuotas > 1 then right('00'+cast(arp.Secuencia as varchar),2)+'/'+cast(ar.NumeroCuotas as varchar) else '' end as NomRuboDetalle, mr.IdPeriodo
FROM            fa_factura AS c INNER JOIN
                         aca_alumno AS cli ON c.IdEmpresa = cli.IdEmpresa AND c.IdAlumno = cli.IdAlumno INNER JOIN
                         tb_persona AS per ON cli.IdPersona = per.IdPersona INNER JOIN
                         fa_Vendedor AS ve ON c.IdEmpresa = ve.IdEmpresa AND c.IdVendedor = ve.IdVendedor INNER JOIN
                         tb_sucursal ON c.IdEmpresa = tb_sucursal.IdEmpresa AND c.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
                         fa_catalogo AS cat ON c.IdCatalogo_FormaPago = cat.IdCatalogo LEFT JOIN 
						 fa_factura_resumen AS R on c.IdEmpresa = r.IdEmpresa and c.IdSucursal = r.IdSucursal and c.IdBodega = r.IdBodega and c.IdCbteVta = r.IdCbteVta LEFT JOIN
						 fa_cliente as cl on c.IdEmpresa = cl.IdEmpresa and c.IdCliente = cl.IdCliente left join
						 fa_formaPago as fp on cl.FormaPago = fp.IdFormaPago left join
						 aca_Matricula_Rubro as mr on c.IdEmpresa = mr.IdEmpresa and c.IdSucursal = mr.IdSucursal and c.IdBodega = mr.IdBodega and c.IdCbteVta = mr.IdCbteVta left join 
						 aca_Matricula as m on m.IdEmpresa = mr.IdEmpresa and m.IdMatricula = mr.IdMatricula left join
						 aca_AnioLectivo_NivelAcademico_Jornada as nj on m.IdEmpresa = nj.IdEmpresa and m.IdAnio = nj.IdAnio and m.IdSede = nj.IdSede and m.IdNivel = nj.IdNivel and m.IdJornada = nj.IdJornada left join
						 aca_AnioLectivo_Sede_NivelAcademico as sn on sn.IdEmpresa = nj.IdEmpresa and sn.IdAnio = nj.IdAnio and sn.IdSede = nj.IdSede and sn.IdNivel = nj.IdNivel	left join
						 aca_AnioLectivo_Rubro as ar on mr.IdEmpresa = ar.IdEmpresa and mr.IdRubro = ar.IdRubro and mr.IdAnio = ar.IdAnio inner join
						 tb_mes as mes on month(c.vt_fecha) = mes.idMes left join
						 aca_AnioLectivo_Rubro_Periodo as arp on mr.IdEmpresa = arp.IdEmpresa and mr.IdAnio = arp.IdAnio and mr.IdRubro = arp.IdRubro and mr.IdPeriodo = arp.IdPeriodo left join
						 aca_AnioLectivo_Periodo as ap on ap.IdEmpresa = mr.IdEmpresa and ap.IdAnio =  mr.IdAnio and ap.IdPeriodo = mr.IdPeriodo left join
						 tb_mes as mesPeriodo on mesPeriodo.idMes = ap.IdMes
						 left join (
							 select f.IdEmpresa, count(*) FacturasAnuladas
							 from fa_factura as f
							 where f.IdEmpresa = @IdEmpresa and f.IdSucursal between @IdSucursalIni and @IdSucursalFin and f.vt_fecha between @FechaIni and @FechaFin
							 and f.Estado = 'I'  and f.IdAlumno between @IdAlumnoIni and @IdAlumnoFin
							 group by f.IdEmpresa
						 ) as anu on c.IdEmpresa = anu.IdEmpresa
						 where c.IdEmpresa = @IdEmpresa and c.IdSucursal between @IdSucursalIni and @IdSucursalFin and c.vt_fecha between @FechaIni and @FechaFin
						 and c.Estado= case when @MostrarAnulados = 1 then c.Estado else 'A' END and c.IdAlumno between @IdAlumnoIni and @IdAlumnoFin