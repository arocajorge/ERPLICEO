create view vwba_TipoFlujo_Movimiento
as
SELECT        dbo.ba_TipoFlujo_Movimiento.IdEmpresa, dbo.ba_TipoFlujo_Movimiento.IdMovimiento, dbo.ba_TipoFlujo_Movimiento.IdTipoFlujo, dbo.ba_TipoFlujo.Descricion, dbo.ba_TipoFlujo_Movimiento.IdSucursal, 
                         dbo.tb_sucursal.Su_Descripcion, dbo.ba_TipoFlujo_Movimiento.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, dbo.ba_TipoFlujo_Movimiento.Valor, dbo.ba_TipoFlujo_Movimiento.Fecha, 
                         dbo.ba_TipoFlujo_Movimiento.Estado
FROM            dbo.ba_Banco_Cuenta INNER JOIN
                         dbo.ba_TipoFlujo ON dbo.ba_Banco_Cuenta.IdEmpresa = dbo.ba_TipoFlujo.IdEmpresa INNER JOIN
                         dbo.ba_TipoFlujo_Movimiento ON dbo.ba_Banco_Cuenta.IdEmpresa = dbo.ba_TipoFlujo_Movimiento.IdEmpresa AND dbo.ba_Banco_Cuenta.IdBanco = dbo.ba_TipoFlujo_Movimiento.IdBanco AND 
                         dbo.ba_TipoFlujo.IdEmpresa = dbo.ba_TipoFlujo_Movimiento.IdEmpresa AND dbo.ba_TipoFlujo.IdTipoFlujo = dbo.ba_TipoFlujo_Movimiento.IdTipoFlujo INNER JOIN
                         dbo.tb_sucursal ON dbo.ba_TipoFlujo_Movimiento.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ba_TipoFlujo_Movimiento.IdSucursal = dbo.tb_sucursal.IdSucursal