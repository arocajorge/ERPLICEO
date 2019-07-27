CREATE PROCEDURE [dbo].[sppro_GetProductoFacturadosPorFecha]
(
@IdEmpresa int, @IdSucursal int, @IdBodega int, @FechaIni DATE, @FechaFin DATE
)
as
/*
set @IdEmpresa = 1
set @IdSucursal = 1
set @IdBodega = 1
set @FechaIni = DATEFROMPARTS(2018,1,1)
set @FechaFin = DATEFROMPARTS(2018,12,31)*/

select d.IdEmpresa, d.IdProducto, p.pr_descripcion, ISNULL(sum(d.vt_cantidad),0) vt_cantidad, min(c.vt_fecha) vt_fecha, U.IdUnidadMedida, u.Descripcion as NombreUnidad, isnull(inv.stock,0)stock, ISNULL(fab.CantidadFabricada,0)CantidadFabricada
from fa_factura_det as d inner join fa_factura as c
on c.IdEmpresa = d.IdEmpresa and c.IdSucursal = d.IdSucursal
and c.IdBodega = d.IdBodega and c.IdCbteVta = d.IdCbteVta
left join in_Producto as p on p.IdEmpresa = d.IdEmpresa
and p.IdProducto = d.IdProducto inner join in_UnidadMedida as u
on p.IdUnidadMedida_Consumo = u.IdUnidadMedida
inner join in_ProductoTipo as t on p.IdEmpresa = t.IdEmpresa
and p.IdProductoTipo = t.IdProductoTipo
left join (
select f.IdEmpresa, f.IdSucursal, f.IdBodega, f.IdProducto, sum(f.dm_cantidad) stock from in_movi_inve_detalle as f
inner join in_movi_inve as fc on f.IdEmpresa = fc.IdEmpresa and f.IdSucursal = fc.IdSucursal and 
f.IdBodega = fc.IdBodega and f.IdMovi_inven_tipo = fc.IdMovi_inven_tipo and f.IdNumMovi = fc.IdNumMovi
where fc.IdEmpresa = @IdEmpresa
and fc.IdSucursal = @IdSucursal
and fc.IdBodega = @IdBodega
and fc.cm_fecha <= @FechaFin
group by f.IdEmpresa, f.IdSucursal, f.IdBodega, f.IdProducto
) as inv on d.IdEmpresa = inv.IdEmpresa and d.IdProducto = INV.IdProducto
left join (
select fd.IdEmpresa, fd.IdProducto, sum(fd.Cantidad) CantidadFabricada
from pro_fabricacion as fc inner join pro_FabricacionDet as fd
on fc.IdEmpresa = fd.IdEmpresa and fc.IdFabricacion = fd.IdFabricacion
where fc.IdEmpresa = @IdEmpresa and fc.ing_IdSucursal = @IdSucursal
and fc.ing_IdBodega = @IdBodega and fc.Fecha between @FechaIni and @FechaFin
and fc.Estado = 1 and fc.egr_IdNumMovi > 0
group by fd.IdEmpresa, fd.IdProducto
) fab on d.IdEmpresa = fab.IdEmpresa and d.IdProducto = fab.IdProducto

where c.IdEmpresa = @IdEmpresa and c.IdSucursal = @IdSucursal
and c.IdBodega = @IdBodega and
c.vt_fecha between @FechaIni and @FechaFin and t.Aparece_fabricacion = 1
and c.Estado = 'A'
group by d.IdEmpresa, d.IdProducto, p.pr_descripcion, U.IdUnidadMedida,u.Descripcion, inv.stock, fab.CantidadFabricada