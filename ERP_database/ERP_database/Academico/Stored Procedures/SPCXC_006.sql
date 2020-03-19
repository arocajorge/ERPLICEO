CREATE PROCEDURE Academico.SPCXC_006
(
@IdEmpresa int, 
@IdSucursal int,
@FechaIni date, 
@FechaFin date
)
AS

select A.IdEmpresa, A.IdSucursal, A.IdLiquidacion, A.Lote, A.Fecha, A.IdBanco, B.ba_descripcion,ISNULL(E.ValorCobro,0) ValorCobro, ISNULL(C.ValorComision,0) ValorComision, ISNULL(D.ValorImpuesto,0) ValorImpuesto, 
ISNULL(ValorCobro,0) - ISNULL(ValorComision,0) - ISNULL(ValorImpuesto,0) DepositoNeto, a.Observacion, F.Su_Descripcion
from cxc_LiquidacionTarjeta AS A INNER JOIN
ba_Banco_Cuenta AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdBanco = B.IdBanco INNER JOIN
tb_sucursal as F ON A.IdEmpresa = F.IdEmpresa AND A.IdSucursal = F.IdSucursal LEFT JOIN
(
SELECT A1.IdEmpresa, a1.IdSucursal, A1.IdLiquidacion, A1.IdMotivo, A2.Descripcion, SUM(A1.Valor) ValorComision 
FROM cxc_LiquidacionTarjetaDet AS A1 INNER JOIN
cxc_MotivoLiquidacionTarjeta AS A2 ON A1.IdEmpresa = A2.IdEmpresa AND A1.IdMotivo = A2.IdMotivo inner join
cxc_LiquidacionTarjeta as A3 ON A1.IdEmpresa = A3.IdEmpresa AND A1.IdSucursal = A3.IdSucursal AND A1.IdLiquidacion = A3.IdLiquidacion
WHERE A1.IdMotivo = 1 AND A3.Estado = 1 AND A3.Fecha BETWEEN @FechaIni AND @FechaFin AND A3.IdEmpresa = @IdEmpresa and a3.IdSucursal = @IdSucursal
group by A1.IdEmpresa, a1.IdSucursal, A1.IdLiquidacion, A1.IdMotivo, A2.Descripcion
) AS C ON A.IdEmpresa = C.IdEmpresa AND A.IdSucursal = C.IdSucursal AND A.IdLiquidacion = C.IdLiquidacion LEFT JOIN
(
SELECT A1.IdEmpresa, a1.IdSucursal, A1.IdLiquidacion, A1.IdMotivo, A2.Descripcion, SUM(A1.Valor) ValorImpuesto
FROM cxc_LiquidacionTarjetaDet AS A1 INNER JOIN
cxc_MotivoLiquidacionTarjeta AS A2 ON A1.IdEmpresa = A2.IdEmpresa AND A1.IdMotivo = A2.IdMotivo inner join
cxc_LiquidacionTarjeta as A3 ON A1.IdEmpresa = A3.IdEmpresa AND A1.IdSucursal = A3.IdSucursal AND A1.IdLiquidacion = A3.IdLiquidacion
WHERE A1.IdMotivo = 2 AND A3.Estado = 1 AND A3.Fecha BETWEEN @FechaIni AND @FechaFin AND A3.IdEmpresa = @IdEmpresa and a3.IdSucursal = @IdSucursal
group by A1.IdEmpresa, a1.IdSucursal, A1.IdLiquidacion, A1.IdMotivo, A2.Descripcion
) AS D ON A.IdEmpresa = D.IdEmpresa AND A.IdSucursal = D.IdSucursal AND A.IdLiquidacion = D.IdLiquidacion LEFT JOIN
(
SELECT A1.IdEmpresa, a1.IdSucursal, A1.IdLiquidacion, SUM(A1.Valor) ValorCobro
FROM cxc_LiquidacionTarjeta_x_cxc_cobro AS A1 inner join
cxc_LiquidacionTarjeta as A3 ON A1.IdEmpresa = A3.IdEmpresa AND A1.IdSucursal = A3.IdSucursal AND A1.IdLiquidacion = A3.IdLiquidacion
WHERE A3.Estado = 1 AND A3.Fecha BETWEEN @FechaIni AND @FechaFin AND A3.IdEmpresa = @IdEmpresa and a3.IdSucursal = @IdSucursal
group by A1.IdEmpresa, a1.IdSucursal, A1.IdLiquidacion
) AS E ON A.IdEmpresa = E.IdEmpresa AND A.IdSucursal = E.IdSucursal AND A.IdLiquidacion = E.IdLiquidacion
WHERE A.Estado = 1 AND A.IdEmpresa = @IdEmpresa AND A.Fecha BETWEEN @FechaIni AND @FechaFin and A.IdSucursal = @IdSucursal