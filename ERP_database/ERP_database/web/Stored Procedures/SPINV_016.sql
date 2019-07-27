
--EXEC [web].[SPINV_016] 2,1,1,1,99999,1,99999,1,99999,1,99999,'2019/01/05','31/01/2019',0,'ADMIN'
CREATE PROCEDURE [web].[SPINV_016]
(
@IdEmpresa int,
@IdSucursalIni int,
@IdSucursalFin int,

@IdCategoriaIni int,
@IdCategoriaFin int,
@IdLineaIni int,
@IdLineaFin int,
@IdGrupoIni int,
@IdGrupoFin int,
@IdSubGrupoIni int,
@IdSubGrupoFin int,

@FechaIni date,
@FechaFin date,
@NoMostrarSinVenta bit,
@IdUsuario varchar(50)
)
AS

DELETE [web].[in_SPINV_016] where IdUsuario = @IdUsuario

INSERT INTO [web].[in_SPINV_016]
           ([IdEmpresa]
           ,[IdSucursal]
           ,[IdProducto]
           ,[IdUsuario]
           ,[SaldoInicial]
           ,[CantidadIngresada]
           ,[CantidadVendida]
           ,[CostoPromedio]
           ,[CostoTotal]
           ,[Stock]
           ,[PrecioVenta])

select pb.IdEmpresa, pb.IdSucursal, pb.IdProducto, @IdUsuario,
ISNULL(d.SaldoInicial,0),0,0,0,0,0,p.precio_1
from in_Producto as p inner join 
in_producto_x_tb_bodega as pb on p.IdEmpresa = pb.IdEmpresa and p.IdProducto = pb.IdProducto inner join 
tb_sucursal as s on s.IdEmpresa = pb.IdEmpresa and s.IdSucursal = pb.IdSucursal left join (
select det.IdEmpresa, det.IdSucursal, det.IdProducto, sum(det.dm_cantidad) SaldoInicial
from in_movi_inve_detalle as det inner join 
in_movi_inve as cab on cab.IdEmpresa = det.IdEmpresa and cab.IdSucursal = det.IdSucursal and cab.IdBodega = det.IdBodega and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo and cab.IdNumMovi = det.IdNumMovi
where cab.cm_fecha < @FechaIni
group by det.IdEmpresa, det.IdSucursal, det.IdProducto
) as d on d.IdEmpresa = pb.IdEmpresa and d.IdSucursal = pb.IdSucursal and d.IdProducto = pb.IdProducto
where pb.IdEmpresa = @IdEmpresa
and pb.IdSucursal between @IdSucursalIni and @IdSucursalFin
and cast(p.IdCategoria as int) between @IdCategoriaIni and @IdCategoriaFin
and p.IdLinea between @IdLineaIni and @IdLineaFin
and p.IdGrupo between @IdGrupoIni and @IdGrupoFin
and p.IdSubGrupo between @IdSubGrupoIni and @IdSubGrupoFin
GROUP BY PB.IdEmpresa, PB.IdSucursal, PB.IdProducto, D.SaldoInicial, P.precio_1

BEGIN --ACTUALIZO INGRESOS POR COMPRA
update [web].[in_SPINV_016] set CantidadIngresada = A.Ingresos
FROM(
select G.IdEmpresa, G.IdSucursal, G.IdProducto, SUM(G.Ingresos)Ingresos from(
SELECT di.IdEmpresa, di.IdSucursal, di.IdProducto, SUM(di.dm_cantidad) AS Ingresos
FROM     in_Ing_Egr_Inven_det AS d INNER JOIN
                  in_movi_inve_detalle AS di ON d.IdEmpresa_inv = di.IdEmpresa AND d.IdSucursal_inv = di.IdSucursal AND d.IdBodega_inv = di.IdBodega AND d.IdMovi_inven_tipo_inv = di.IdMovi_inven_tipo AND d.IdNumMovi_inv = di.IdNumMovi AND 
                  d.secuencia_inv = di.Secuencia INNER JOIN
                  in_Ing_Egr_Inven AS c ON d.IdEmpresa = c.IdEmpresa AND d.IdSucursal = c.IdSucursal AND d.IdMovi_inven_tipo = c.IdMovi_inven_tipo AND d.IdNumMovi = c.IdNumMovi INNER JOIN
                  cp_orden_giro_x_in_Ing_Egr_Inven AS r ON c.IdEmpresa = r.inv_IdEmpresa AND c.IdSucursal = r.inv_IdSucursal AND c.IdMovi_inven_tipo = r.inv_IdMovi_inven_tipo AND c.IdNumMovi = r.inv_IdNumMovi
where c.IdEmpresa = @IdEmpresa and c.IdSucursal between @IdSucursalIni and @IdSucursalFin and
c.cm_fecha between @FechaIni and @FechaFin
GROUP BY di.IdEmpresa, di.IdSucursal, di.IdProducto
union all
SELECT d.IdEmpresa, d.IdSucursal, d.IdProducto, sum(m.dm_cantidad)
FROM in_Ing_Egr_Inven_det as d inner join in_movi_inve_detalle as m
on m.IdEmpresa = d.IdEmpresa_inv and m.IdSucursal = d.IdSucursal_inv and m.IdBodega = d.IdBodega_inv and m.IdMovi_inven_tipo = d.IdMovi_inven_tipo_inv and m.IdNumMovi = d.IdNumMovi_inv and m.Secuencia = d.secuencia_inv inner join
in_Ing_Egr_Inven c on c.IdEmpresa = d.IdEmpresa and c.IdSucursal = d.IdSucursal and c.IdMovi_inven_tipo = d.IdMovi_inven_tipo and c.IdNumMovi = d.IdNumMovi
where c.IdEmpresa = @IdEmpresa and c.IdSucursal between @IdSucursalIni and @IdSucursalFin and
c.cm_fecha between @FechaIni and @FechaFin AND D.dm_cantidad > 0 AND NOT EXISTS (
SELECT f.inv_IdEmpresa FROM cp_orden_giro_x_in_Ing_Egr_Inven f
where f.inv_IdEmpresa = c.IdEmpresa
and f.inv_IdSucursal = c.IdSucursal
and f.inv_IdMovi_inven_tipo = c.IdMovi_inven_tipo
and f.inv_IdNumMovi = c.IdNumMovi
) and not exists(
select f.IdEmpresa from in_transferencia as f
where f.IdEmpresa_Ing_Egr_Inven_Destino = c.IdEmpresa
and f.IdSucursal_Ing_Egr_Inven_Destino = c.IdSucursal
and f.IdMovi_inven_tipo_SucuDest = c.IdMovi_inven_tipo
and f.IdNumMovi_Ing_Egr_Inven_Destino = c.IdNumMovi
)
GROUP BY d.IdEmpresa, d.IdSucursal, d.IdProducto
) G
GROUP  BY G.IdEmpresa, G.IdSucursal, G.IdProducto
) A
WHERE [web].[in_SPINV_016].IdEmpresa = A.IdEmpresa
AND [web].[in_SPINV_016].IdSucursal = A.IdSucursal
AND [web].[in_SPINV_016].IdProducto = A.IdProducto
AND [web].[in_SPINV_016].IdUsuario = @IdUsuario
END

BEGIN -- ACTUALIZO VENTAS
update [web].[in_SPINV_016] set CantidadVendida = ABS(A.Ventas)
FROM(

SELECT G.IdEmpresa, G.IdSucursal, G.IdProducto, SUM(G.Ventas) Ventas
FROM (
SELECT di.IdEmpresa, di.IdSucursal, di.IdProducto, SUM(di.dm_cantidad) AS Ventas
FROM     in_Ing_Egr_Inven_det AS d INNER JOIN
                  in_movi_inve_detalle AS di ON d.IdEmpresa_inv = di.IdEmpresa AND d.IdSucursal_inv = di.IdSucursal AND d.IdBodega_inv = di.IdBodega AND d.IdMovi_inven_tipo_inv = di.IdMovi_inven_tipo AND d.IdNumMovi_inv = di.IdNumMovi AND 
                  d.secuencia_inv = di.Secuencia INNER JOIN
                  in_Ing_Egr_Inven AS c ON d.IdEmpresa = c.IdEmpresa AND d.IdSucursal = c.IdSucursal AND d.IdMovi_inven_tipo = c.IdMovi_inven_tipo AND d.IdNumMovi = c.IdNumMovi INNER JOIN
                  fa_factura_x_in_Ing_Egr_Inven AS R ON c.IdEmpresa = R.IdEmpresa_in_eg_x_inv AND c.IdSucursal = R.IdSucursal_in_eg_x_inv AND c.IdMovi_inven_tipo = R.IdMovi_inven_tipo_in_eg_x_inv AND 
                  c.IdNumMovi = R.IdNumMovi_in_eg_x_inv
where c.IdEmpresa = @IdEmpresa and c.IdSucursal between @IdSucursalIni and @IdSucursalFin and
c.cm_fecha between @FechaIni and @FechaFin
GROUP BY di.IdEmpresa, di.IdSucursal, di.IdProducto

UNION ALL

SELECT d.IdEmpresa, d.IdSucursal, d.IdProducto, sum(m.dm_cantidad)
FROM in_Ing_Egr_Inven_det as d inner join in_movi_inve_detalle as m
on m.IdEmpresa = d.IdEmpresa_inv and m.IdSucursal = d.IdSucursal_inv and m.IdBodega = d.IdBodega_inv and m.IdMovi_inven_tipo = d.IdMovi_inven_tipo_inv and m.IdNumMovi = d.IdNumMovi_inv and m.Secuencia = d.secuencia_inv inner join
in_Ing_Egr_Inven c on c.IdEmpresa = d.IdEmpresa and c.IdSucursal = d.IdSucursal and c.IdMovi_inven_tipo = d.IdMovi_inven_tipo and c.IdNumMovi = d.IdNumMovi
where c.IdEmpresa = @IdEmpresa and c.IdSucursal between @IdSucursalIni and @IdSucursalFin and
c.cm_fecha between @FechaIni and @FechaFin AND D.dm_cantidad < 0 
and not exists(
select f.IdEmpresa from in_transferencia as f
where f.IdEmpresa_Ing_Egr_Inven_Origen = c.IdEmpresa
and f.IdSucursal_Ing_Egr_Inven_Origen = c.IdSucursal
and f.IdMovi_inven_tipo_SucuOrig = c.IdMovi_inven_tipo
and f.IdNumMovi_Ing_Egr_Inven_Origen = c.IdNumMovi
)
and not exists(
select f.IdEmpresa_fa from fa_factura_det_x_in_Ing_Egr_Inven_det as f
where f.IdEmpresa_eg = c.IdEmpresa
and f.IdSucursal_eg = c.IdSucursal
and f.IdMovi_inven_tipo_eg = c.IdMovi_inven_tipo
and f.IdNumMovi_eg = c.IdNumMovi
)
GROUP BY d.IdEmpresa, d.IdSucursal, d.IdProducto
) G
GROUP BY G.IdEmpresa, G.IdSucursal, G.IdProducto
) A
WHERE [web].[in_SPINV_016].IdEmpresa = A.IdEmpresa
AND [web].[in_SPINV_016].IdSucursal = A.IdSucursal
AND [web].[in_SPINV_016].IdProducto = A.IdProducto
AND [web].[in_SPINV_016].IdUsuario = @IdUsuario
END

BEGIN --ACTUALIZO COSTO PROMEDIO
update [web].[in_SPINV_016] set CostoPromedio = B.costo
FROM(
select a.IdEmpresa, a.IdSucursal, a.IdProducto, costo from (
select ROW_NUMBER() over(partition by c.IdEmpresa, c.IdSucursal, c.IdProducto order by c.IdEmpresa, c.IdSucursal, c.IdProducto, c.fecha desc,c.IdFecha desc, c.Secuencia desc) Fila, 
c.IdEmpresa, c.IdSucursal, c.IdProducto, c.costo, C.IdFecha,c.Secuencia,c.fecha
from in_producto_x_tb_bodega_Costo_Historico as c
where c.IdEmpresa = @IdEmpresa and c.IdSucursal between @IdSucursalIni and @IdSucursalFin and c.fecha <= @FechaFin
--order by c.IdEmpresa, c.IdSucursal, c.IdProducto, c.fecha desc,c.IdFecha desc, c.Secuencia desc
)A WHERE A.Fila = 1) B 
WHERE [web].[in_SPINV_016].IdEmpresa = B.IdEmpresa
AND [web].[in_SPINV_016].IdSucursal = B.IdSucursal
AND [web].[in_SPINV_016].IdProducto = B.IdProducto
AND [web].[in_SPINV_016].IdUsuario = @IdUsuario
END

BEGIN --ACTUALIZO STOCK
	update [web].[in_SPINV_016] set Stock = a.SaldoFinal
	from(
	select det.IdEmpresa, det.IdSucursal, det.IdProducto, sum(det.dm_cantidad) SaldoFinal
	from in_movi_inve_detalle as det inner join 
	in_movi_inve as cab on cab.IdEmpresa = det.IdEmpresa and cab.IdSucursal = det.IdSucursal and cab.IdBodega = det.IdBodega and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo and cab.IdNumMovi = det.IdNumMovi
	where cab.IdEmpresa = @IdEmpresa and cab.IdSucursal between @IdSucursalIni and @IdSucursalFin and cab.cm_fecha <= @FechaFin
	group by det.IdEmpresa, det.IdSucursal, det.IdProducto
	) a 
	where [web].[in_SPINV_016].IdEmpresa = a.IdEmpresa
	and [web].[in_SPINV_016].IdSucursal = a.IdSucursal
	and [web].[in_SPINV_016].IdProducto = a.IdProducto
	and [web].[in_SPINV_016].IdUsuario = @IdUsuario
END

update [web].[in_SPINV_016] set CostoTotal = CantidadVendida * CostoPromedio
where IdUsuario = @IdUsuario

delete [web].[in_SPINV_016] 
where IdUsuario = @IdUsuario and SaldoInicial = 0 and CantidadIngresada = 0 and CantidadVendida = 0 and Stock = 0

if(@NoMostrarSinVenta = 1)
begin
	delete [web].[in_SPINV_016] 
	where IdUsuario = @IdUsuario and CantidadVendida = 0
end

SELECT r.IdEmpresa, r.IdSucursal, r.IdProducto, r.IdUsuario, r.SaldoInicial, r.CantidadIngresada, r.CantidadVendida, r.CostoPromedio, r.CostoTotal, r.Stock, r.PrecioVenta, s.Su_Descripcion, p.pr_descripcion, c.ca_Categoria, l.nom_linea, 
                  g.nom_grupo, sg.nom_subgrupo
FROM     in_Producto AS p LEFT OUTER JOIN
                  in_subgrupo AS sg INNER JOIN
                  in_linea AS l INNER JOIN
                  in_categorias AS c ON l.IdEmpresa = c.IdEmpresa AND l.IdCategoria = c.IdCategoria INNER JOIN
                  in_grupo AS g ON l.IdEmpresa = g.IdEmpresa AND l.IdCategoria = g.IdCategoria AND l.IdLinea = g.IdLinea ON sg.IdEmpresa = g.IdEmpresa AND sg.IdCategoria = g.IdCategoria AND sg.IdLinea = g.IdLinea AND sg.IdGrupo = g.IdGrupo ON 
                  p.IdEmpresa = sg.IdEmpresa AND p.IdCategoria = sg.IdCategoria AND p.IdLinea = sg.IdLinea AND p.IdGrupo = sg.IdGrupo AND p.IdSubGrupo = sg.IdSubgrupo RIGHT OUTER JOIN
                  web.in_SPINV_016 AS r ON p.IdEmpresa = r.IdEmpresa AND p.IdProducto = r.IdProducto LEFT OUTER JOIN
                  tb_sucursal AS s ON r.IdEmpresa = s.IdEmpresa AND r.IdSucursal = s.IdSucursal
where @IdUsuario = r.IdUsuario