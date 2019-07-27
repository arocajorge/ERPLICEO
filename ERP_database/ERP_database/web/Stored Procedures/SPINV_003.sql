
--EXEC web.SPINV_003 2,2,2,1,1,0,99999,'3',29,0,0,'09/09/2019',0,0,9999
CREATE PROCEDURE [web].[SPINV_003]
(
@IdEmpresa int,
@IdSucursal_ini int,
@IdSucursal_fin int,
@IdBodega_ini int,
@IdBodega_fin int,
@IdProducto_ini numeric,
@IdProducto_fin numeric,
@IdCategoria varchar(20),
@IdLinea int,
@IdGrupo int,
@IdSubGrupo int,
@fecha_corte datetime,
@mostrar_stock_0 bit,
@IdMarcaIni int,
@IdMarcaFin int
)
AS
BEGIN

DELETE web.in_SPINV_003

BEGIN --INSERTO EN TABLA PK DE PRODUCTOS A MOSTRAR
	INSERT INTO web.in_SPINV_003
	SELECT in_producto_x_tb_bodega.IdEmpresa, in_producto_x_tb_bodega.IdSucursal, in_producto_x_tb_bodega.IdBodega, in_producto_x_tb_bodega.IdProducto, 0 AS Expr1, 0 AS Expr2, 0 AS Expr3, in_Producto.IdCategoria, in_Producto.IdLinea, 
		in_Producto.IdGrupo, in_Producto.IdSubGrupo, in_Producto.IdMarca
	FROM     in_producto_x_tb_bodega INNER JOIN
		in_Producto ON in_producto_x_tb_bodega.IdEmpresa = in_Producto.IdEmpresa AND in_producto_x_tb_bodega.IdProducto = in_Producto.IdProducto
	where in_producto_x_tb_bodega.IdEmpresa = @IdEmpresa
	AND IdSucursal between @IdSucursal_ini and @IdSucursal_fin
	AND IdBodega BETWEEN @IdBodega_ini and @IdBodega_fin
	and isnull(in_Producto.IdProducto_padre,in_Producto.IdProducto) between @IdProducto_ini and @IdProducto_fin
	and in_Producto.IdMarca between @IdMarcaIni and @IdMarcaFin
END

BEGIN --FILTRO POR CATEGORIZACION
	IF(@IdCategoria <> '')
	BEGIN
		DELETE web.in_SPINV_003 
		WHERE IdCategoria <>  @IdCategoria

		IF(@IdLinea != 0)
			BEGIN
				DELETE web.in_SPINV_003 
				WHERE IdLinea != @IdLinea

				IF(@IdGrupo != 0)
					BEGIN
					DELETE web.in_SPINV_003 
					WHERE IdGrupo != @IdGrupo
						IF(@IdSubGrupo != 0)
							BEGIN
								DELETE web.in_SPINV_003 
								WHERE IdSubGrupo != @IdSubGrupo
							END
					END					
			END	
	END
END

BEGIN --ACTUALIZO STOCK Y COSTO A LA FECHA
	UPDATE web.in_SPINV_003 SET Stock = ROUND(A.cantidad,2), Costo_total = A.costo_total, Costo_promedio = IIF(A.cantidad = 0, 0 ,A.costo_total / A.cantidad)
	FROM(
	SELECT det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto, sum(dm_cantidad) cantidad, sum(dm_cantidad * mv_costo) costo_total
	FROM in_movi_inve cab inner join
	in_movi_inve_detalle det 
	on cab.IdEmpresa = det.IdEmpresa
	and cab.IdSucursal = det.IdSucursal
	and cab.IdBodega = det.IdBodega
	and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
	and cab.IdNumMovi = det.IdNumMovi
	inner join web.in_SPINV_003 sp
	on sp.IdEmpresa = det.IdEmpresa
	and sp.IdSucursal = det.IdSucursal
	and sp.IdBodega = det.IdBodega
	and sp.IdProducto = det.IdProducto
	WHERE cab.cm_fecha <= @fecha_corte	
	group by det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto
	) A
	WHERE web.in_SPINV_003.IdEmpresa = A.IdEmpresa
	AND web.in_SPINV_003.IdSucursal = A.IdSucursal
	AND web.in_SPINV_003.IdBodega = A.IdBodega
	and web.in_SPINV_003.IdProducto = A.IdProducto
END

IF(@mostrar_stock_0 = 0)--ELIMINO STOCK 0 SI EL PARAMETRO LO DICE
BEGIN
	DELETE web.in_SPINV_003 
	WHERE Stock = 0
END


SELECT sp.IdEmpresa, sp.IdSucursal, sp.IdBodega, sp.IdProducto, sp.Stock, sp.Costo_promedio, sp.Costo_total, s.Su_Descripcion, b.bo_Descripcion, p.pr_codigo, p.pr_descripcion, p.lote_num_lote, p.lote_fecha_vcto, c.IdCategoria, c.ca_Categoria, 
                  l.IdLinea, l.nom_linea, g.IdGrupo, g.nom_grupo, sg.IdSubgrupo, sg.nom_subgrupo, pr.IdPresentacion, pr.nom_presentacion, sp.IdMarca, mar.Descripcion AS NomMarca
FROM     in_linea AS l INNER JOIN
                  in_grupo AS g INNER JOIN
                  in_subgrupo AS sg ON g.IdEmpresa = sg.IdEmpresa AND g.IdCategoria = sg.IdCategoria AND g.IdLinea = sg.IdLinea AND g.IdGrupo = sg.IdGrupo ON l.IdEmpresa = g.IdEmpresa AND l.IdCategoria = g.IdCategoria AND 
                  l.IdLinea = g.IdLinea INNER JOIN
                  in_categorias AS c ON l.IdEmpresa = c.IdEmpresa AND l.IdCategoria = c.IdCategoria RIGHT OUTER JOIN
                  web.in_SPINV_003 AS sp INNER JOIN
                  in_Producto AS p ON sp.IdEmpresa = p.IdEmpresa AND sp.IdProducto = p.IdProducto LEFT OUTER JOIN
                  tb_sucursal AS s INNER JOIN
                  tb_bodega AS b ON s.IdEmpresa = b.IdEmpresa AND s.IdSucursal = b.IdSucursal ON sp.IdEmpresa = b.IdEmpresa AND sp.IdSucursal = b.IdSucursal AND sp.IdBodega = b.IdBodega ON sg.IdEmpresa = p.IdEmpresa AND 
                  sg.IdCategoria = p.IdCategoria AND sg.IdLinea = p.IdLinea AND sg.IdGrupo = p.IdGrupo AND sg.IdSubgrupo = p.IdSubGrupo LEFT OUTER JOIN
                  in_presentacion AS pr ON p.IdEmpresa = pr.IdEmpresa AND p.IdPresentacion = pr.IdPresentacion LEFT OUTER JOIN
                  in_Marca AS mar ON mar.IdEmpresa = sp.IdEmpresa AND mar.IdMarca = sp.IdMarca
END
