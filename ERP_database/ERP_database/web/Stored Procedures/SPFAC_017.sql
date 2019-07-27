--EXEC [web].[SPFAC_017] 2,1,'01/01/2019','31/12/2019'
CREATE PROCEDURE [web].[SPFAC_017]
(
@IdEmpresa int,
@IdMarca int,
@FechaIni date,
@FechaFin date
)
AS
SELECT fa_factura.IdEmpresa, in_Marca.Descripcion MarcaDescripcion, YEAR(fa_factura.vt_fecha) ANIO, month(fa_factura.vt_fecha) MES,RIGHT('00'+CAST(month(fa_factura.vt_fecha) AS VARCHAR(2)),2)+' '+ tb_mes.smes NomMes, round(sum(fa_factura_det.vt_cantidad * fa_factura_det.vt_Precio),2)vt_total
FROM     fa_factura INNER JOIN
                  fa_factura_det ON fa_factura.IdEmpresa = fa_factura_det.IdEmpresa AND fa_factura.IdSucursal = fa_factura_det.IdSucursal AND fa_factura.IdBodega = fa_factura_det.IdBodega AND 
                  fa_factura.IdCbteVta = fa_factura_det.IdCbteVta INNER JOIN
                  tb_sucursal ON fa_factura_det.IdEmpresa = tb_sucursal.IdEmpresa AND fa_factura_det.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
                  in_Producto ON fa_factura_det.IdEmpresa = in_Producto.IdEmpresa AND fa_factura_det.IdProducto = in_Producto.IdProducto INNER JOIN
				  in_Marca on in_Producto.IdEmpresa = in_Marca.IdEmpresa and in_Producto.IdMarca = in_marca.IdMarca INNER JOIN
                  tb_mes ON month(fa_factura.vt_fecha) = tb_mes.idMes
WHERE fa_factura.IdEmpresa = @IdEmpresa and in_Producto.IdMarca = @IdMarca and fa_factura.vt_fecha between @FechaIni and @FechaFin and fa_factura.Estado = 'A'
group by fa_factura.IdEmpresa, in_Marca.Descripcion, YEAR(fa_factura.vt_fecha), month(fa_factura.vt_fecha), tb_mes.smes