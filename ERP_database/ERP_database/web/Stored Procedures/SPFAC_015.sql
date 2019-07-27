--exec [web].[SPFAC_015] 2,3,'01/01/2019','31/12/2019'
CREATE PROCEDURE [web].[SPFAC_015]
(
@IdEmpresa int,
@IdSucursal int,
@FechaIni date,
@FechaFin date
)
AS
SELECT fa_factura.IdEmpresa, fa_factura.IdSucursal, Right('00'+ cast(fa_factura.IdSucursal as varchar(2)),2)+' '+ tb_sucursal.Su_Descripcion Su_Descripcion, YEAR(fa_factura.vt_fecha) ANIO, month(fa_factura.vt_fecha) MES, 
Right('00'+ cast(month(fa_factura.vt_fecha) as varchar(2)),2)+' '+ tb_mes.smes NomMes, ROUND(SUM(CAST(fa_factura_resumen.SubtotalSinDscto AS FLOAT)),2) vt_total
FROM     fa_factura INNER JOIN
                  tb_sucursal ON fa_factura.IdEmpresa = tb_sucursal.IdEmpresa AND fa_factura.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                  tb_mes ON month(fa_factura.vt_fecha) = tb_mes.idMes INNER JOIN
				  fa_factura_resumen ON fa_factura.IdEmpresa = fa_factura_resumen.IdEmpresa AND fa_factura.IdSucursal = fa_factura_resumen.IdSucursal
				  AND fa_factura.IdBodega = fa_factura_resumen.IdBodega AND fa_factura.IdCbteVta = fa_factura_resumen.IdCbteVta
				  
WHERE fa_factura.IdEmpresa = @IdEmpresa and fa_factura.IdSucursal = @IdSucursal and fa_factura.vt_fecha between @FechaIni and @FechaFin and fa_factura.Estado = 'A'
group by fa_factura.IdEmpresa, fa_factura.IdSucursal, tb_sucursal.Su_Descripcion, YEAR(fa_factura.vt_fecha), month(fa_factura.vt_fecha), tb_mes.smes