--exec [web].[SPFAC_010] 1,1,1,'27/08/2018','27/08/2019',1
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
						 per.pe_cedulaRuc, c.vt_Observacion, 'Facturas IVA '+case when r.ValorIVA > 0 then '12' else '0' end+'%' Tarifa
FROM            fa_factura AS c INNER JOIN
                         aca_alumno AS cli ON c.IdEmpresa = cli.IdEmpresa AND c.IdAlumno = cli.IdAlumno INNER JOIN
                         tb_persona AS per ON cli.IdPersona = per.IdPersona INNER JOIN
                         fa_Vendedor AS ve ON c.IdEmpresa = ve.IdEmpresa AND c.IdVendedor = ve.IdVendedor INNER JOIN
                         tb_sucursal ON c.IdEmpresa = tb_sucursal.IdEmpresa AND c.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
                         fa_catalogo AS cat ON c.IdCatalogo_FormaPago = cat.IdCatalogo LEFT JOIN 
						 fa_factura_resumen AS R on c.IdEmpresa = r.IdEmpresa and c.IdSucursal = r.IdSucursal and c.IdBodega = r.IdBodega and c.IdCbteVta = r.IdCbteVta LEFT JOIN
						 fa_cliente as cl on c.IdEmpresa = cl.IdEmpresa and c.IdCliente = cl.IdCliente left join
						 fa_formaPago as fp on cl.FormaPago = fp.IdFormaPago 
						 
						 left join (
							 select f.IdEmpresa, count(*) FacturasAnuladas
							 from fa_factura as f
							 where f.IdEmpresa = @IdEmpresa and f.IdSucursal between @IdSucursalIni and @IdSucursalFin and f.vt_fecha between @FechaIni and @FechaFin
							 and f.Estado = 'I'  and f.IdAlumno between @IdAlumnoIni and @IdAlumnoFin
							 group by f.IdEmpresa
						 ) as anu on c.IdEmpresa = anu.IdEmpresa
						 where c.IdEmpresa = @IdEmpresa and c.IdSucursal between @IdSucursalIni and @IdSucursalFin and c.vt_fecha between @FechaIni and @FechaFin
						 and c.Estado= case when @MostrarAnulados = 1 then c.Estado else 'A' END and c.IdAlumno between @IdAlumnoIni and @IdAlumnoFin