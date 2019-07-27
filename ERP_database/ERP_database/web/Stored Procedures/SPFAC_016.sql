--EXEC [web].[SPFAC_016] 2,3,'1/1/2019','31/1/2019'
CREATE PROCEDURE [web].[SPFAC_016]
(
@IdEmpresa int, --= 2,
@IdSucursal int,-- = 3,
@FechaIni date,-- = DATEFROMPARTS(2019,2,18),
@FechaFin date --= DATEFROMPARTS(2019,4,4)
)
AS

set datefirst 1;
--select datepart(week, DATEFROMPARTS(2019,1,6));
--SELECT DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)
DECLARE @Anio int = year(@FechaIni)
select A.IdEmpresa, A.IdSucursal, A.Su_Descripcion, A.ANIO, A.Semana, A.DescripcionSemana, SUM(A.vt_total)vt_total 
from (
SELECT fa_factura.IdEmpresa, fa_factura.IdSucursal, RIGHT('00'+CAST(fa_factura.IdSucursal AS VARCHAR(2)),2) +' '+ tb_sucursal.Su_Descripcion Su_Descripcion, YEAR(fa_factura.vt_fecha) ANIO, 

CASE WHEN YEAR(DATEADD(ww, DATEDIFF(ww,0,fa_factura.vt_fecha), 0)) < @Anio then 1 else
datepart(week, DATEADD(ww, DATEDIFF(ww,0,fa_factura.vt_fecha), 0)) end Semana,

fa_factura.vt_fecha, round(CAST(fa_factura_resumen.SubtotalSinDscto AS FLOAT),2) vt_total,
RIGHT('00'+ 
CAST(

CASE WHEN YEAR(DATEADD(ww, DATEDIFF(ww,0,fa_factura.vt_fecha), 0)) < @Anio then 1 else
datepart(week, DATEADD(ww, DATEDIFF(ww,0,fa_factura.vt_fecha), 0)) end

 AS VARCHAR(2)),2)+' '+
'SEMANA '+
RIGHT('00'+CAST(DAY(DATEADD(ww, DATEDIFF(ww,0,fa_factura.vt_fecha), 0)) AS VARCHAR(2)),2) + '-'+
RIGHT('00'+CAST(DAY(DATEADD(DAY,6, DATEADD(ww, DATEDIFF(ww,0,fa_factura.vt_fecha), 0))) AS VARCHAR(2)),2) DescripcionSemana
FROM     fa_factura INNER JOIN
                  tb_sucursal ON fa_factura.IdEmpresa = tb_sucursal.IdEmpresa AND fa_factura.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                  tb_mes ON month(fa_factura.vt_fecha) = tb_mes.idMes INNER JOIN
				  fa_factura_resumen ON fa_factura.IdEmpresa = fa_factura_resumen.IdEmpresa AND fa_factura.IdSucursal = fa_factura_resumen.IdSucursal
				  AND fa_factura.IdBodega = fa_factura_resumen.IdBodega AND fa_factura.IdCbteVta = fa_factura_resumen.IdCbteVta
WHERE fa_factura.IdEmpresa = @IdEmpresa and fa_factura.IdSucursal = @IdSucursal and fa_factura.vt_fecha BETWEEN @FechaIni and @FechaFin and fa_factura.Estado = 'A'
) A GROUP BY A.IdEmpresa, A.IdSucursal, A.Su_Descripcion, A.ANIO, A.Semana, A.DescripcionSemana