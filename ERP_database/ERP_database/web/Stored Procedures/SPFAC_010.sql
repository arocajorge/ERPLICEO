CREATE PROCEDURE [web].[SPFAC_010]
(
@IdEmpresa int,
@IdSucursalIni int, 
@IdSucursalFin int,
@FechaIni datetime,
@FechaFin datetime,
@IdCatalogo_FormaPago varchar(20)
)
AS
SELECT        c.IdEmpresa, c.IdSucursal, c.IdBodega, c.IdCbteVta, c.vt_serie1 + '-' + c.vt_serie2 + '-' + c.vt_NumFactura AS vt_NumFactura, c.IdCbteVta IdCliente, per.pe_nombreCompleto, cat.Nombre AS NombreFormaPago, c.IdCatalogo_FormaPago, c.Estado, 
                         c.vt_fecha, c.IdUsuario Ve_Vendedor, c.IdUsuario IdVendedor, tb_sucursal.Su_Descripcion, tb_sucursal.Su_Telefonos, tb_sucursal.Su_Direccion, tb_sucursal.Su_Ruc,
						 r.SubtotalIVAConDscto, r.SubtotalSinIVAConDscto, r.ValorIVA, r.Total, isnull(emi.FacturasEmitidas,0)FacturasEmitidas, isnull(anu.FacturasAnuladas,0)FacturasAnuladas
FROM            fa_factura AS c INNER JOIN
                         fa_cliente AS cli ON c.IdEmpresa = cli.IdEmpresa AND c.IdCliente = cli.IdCliente INNER JOIN
                         tb_persona AS per ON cli.IdPersona = per.IdPersona INNER JOIN
                         fa_Vendedor AS ve ON c.IdEmpresa = ve.IdEmpresa AND c.IdVendedor = ve.IdVendedor INNER JOIN
                         tb_sucursal ON c.IdEmpresa = tb_sucursal.IdEmpresa AND c.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
                         fa_catalogo AS cat ON c.IdCatalogo_FormaPago = cat.IdCatalogo
						 LEFT JOIN fa_factura_resumen AS R on c.IdEmpresa = r.IdEmpresa and c.IdSucursal = r.IdSucursal and c.IdBodega = r.IdBodega and c.IdCbteVta = r.IdCbteVta
						 
						 left join (
							 select f.IdEmpresa, count(*) FacturasEmitidas
							 from fa_factura as f
							 where f.IdEmpresa = @IdEmpresa and f.IdSucursal between @IdSucursalIni and @IdSucursalFin and f.vt_fecha between @FechaIni and @FechaFin
							 and f.IdCatalogo_FormaPago like case when @IdCatalogo_FormaPago = '' then '%'+@IdCatalogo_FormaPago+'%' else @IdCatalogo_FormaPago end
							 group by f.IdEmpresa
						 ) as emi on c.IdEmpresa = emi.IdEmpresa

						 left join (
							 select f.IdEmpresa, count(*) FacturasAnuladas
							 from fa_factura as f
							 where f.IdEmpresa = @IdEmpresa and f.IdSucursal between @IdSucursalIni and @IdSucursalFin and f.vt_fecha between @FechaIni and @FechaFin
							 and f.Estado = 'I' and f.IdCatalogo_FormaPago like case when @IdCatalogo_FormaPago = '' then '%'+@IdCatalogo_FormaPago+'%' else @IdCatalogo_FormaPago end
							 group by f.IdEmpresa
						 ) as anu on c.IdEmpresa = anu.IdEmpresa

						 where c.IdEmpresa = @IdEmpresa and c.IdSucursal between @IdSucursalIni and @IdSucursalFin and c.vt_fecha between @FechaIni and @FechaFin
						 and c.Estado='A' and c.IdCatalogo_FormaPago like case when @IdCatalogo_FormaPago = '' then '%'+@IdCatalogo_FormaPago+'%' else @IdCatalogo_FormaPago end