-- EXEC WEB.SPINV_010 1,0,99999,'',0,0,0,'','','01/01/2018','2018/12/31',0
CREATE PROCEDURE [web].[SPINV_010]
(
@IdEmpresa int,
@IdProductoPadreIni numeric,
@IdProductoPadreFin numeric,
@IdCategoria varchar(20),
@IdLinea int,
@IdGrupo int,
@IdSubGrupo int,
@IdUsuario varchar(50),
@IdMarcaIni int,
@IdMarcaFin int,
@FechaIni datetime,
@FechaFin datetime,
@MostrarSinMovimiento bit
)
AS

update in_Producto set pr_descripcion = a.pr_descripcion, IdMarca = a.IdMarca, IdPresentacion = a.IdPresentacion, IdCod_Impuesto_Iva = a.IdCod_Impuesto_Iva, IdCategoria = a.IdCategoria,
IdLinea = a.IdLinea, IdGrupo = a.IdGrupo, IdSubGrupo = a.IdSubGrupo
from
(
select * from in_Producto as f
where f.IdProducto_padre is null
)a
where in_Producto.IdEmpresa = a.IdEmpresa
and in_Producto.IdProducto_padre = a.IdProducto

DELETE [web].[in_SPINV_010] WHERE IdEmpresa = @IdEmpresa and IdUsuario = @IdUsuario

BEGIN --SET RANGO DE FECHAS
	DECLARE @AnioInicio int, @AnioFin int,
	@PeriodoIni int, @PeriodoFin int, @PeriodoCorte int, @Mes int

	SET @AnioInicio = YEAR(@FechaIni)
	SET @AnioFin = YEAR(@FechaFin)
	SET @PeriodoIni = CAST(cast(@AnioInicio as varchar(4)) + RIGHT('00' + Ltrim(Rtrim(CAST( MONTH(@FechaIni) AS VARCHAR(2)))),2) AS INT)
	SET @PeriodoFin = CAST(cast(@AnioFin as varchar(4)) + RIGHT('00' + Ltrim(Rtrim(CAST( MONTH(@FechaFin) AS VARCHAR(2)))),2) AS INT)

END

BEGIN --INSERT DATA
	IF(@MostrarSinMovimiento = 0)
	BEGIN
		WHILE @AnioInicio <= @AnioFin
		BEGIN
			INSERT INTO [web].[in_SPINV_010]
					   ([IdEmpresa]
					   ,[IdUsuario]
					   ,[IdAnio]
					   ,[IdProducto]
					   ,[pr_descripcion]
					   ,[IdPresentacion]
					   ,[IdCategoria]
					   ,[IdLinea]
					   ,[IdGrupo]
					   ,[IdSubGrupo]
					   ,[IdMarca]
					   ,[Enero]
					   ,[Febrero]
					   ,[Marzo]
					   ,[Abril]
					   ,[Mayo]
					   ,[Junio]
					   ,[Julio]
					   ,[Agosto]
					   ,[Septiembre]
					   ,[Octubre]
					   ,[Noviembre]
					   ,[Diciembre]
					   ,[Total]
					   ,[StockActual])
			select A.IdEmpresa, A.IdUsuario,@AnioInicio, A.IdProducto,a.pr_descripcion, a.IdPresentacion ,A.IdCategoria, A.IdLinea, A.IdGrupo, A.IdSubGrupo, A.IdMarca,
			0,0,0,0,0,0,0,0,0,0,0,0,0,0
			from(
			SELECT        fa_factura_det.IdEmpresa, @IdUsuario IdUsuario, CASE WHEN in_Producto.IdProducto_padre IS NULL THEN  in_Producto.IdProducto ELSE IN_producto.IdProducto_padre end as IdProducto, in_Producto.pr_descripcion, in_Producto.IdPresentacion, in_Producto.IdCategoria, in_Producto.IdLinea, in_Producto.IdGrupo, in_Producto.IdSubGrupo, in_Producto.IdMarca
			FROM            fa_factura_det INNER JOIN
									 in_Producto ON fa_factura_det.IdEmpresa = in_Producto.IdEmpresa AND fa_factura_det.IdProducto = in_Producto.IdProducto INNER JOIN
									 fa_factura ON fa_factura_det.IdEmpresa = fa_factura.IdEmpresa AND fa_factura_det.IdSucursal = fa_factura.IdSucursal AND fa_factura_det.IdBodega = fa_factura.IdBodega AND fa_factura_det.IdCbteVta = fa_factura.IdCbteVta
			where fa_factura.IdEmpresa = @IdEmpresa and fa_factura.vt_fecha between @FechaIni and @FechaFin AND fa_factura.Estado = 'A' AND in_Producto.Estado = 'A' AND in_producto.IdMarca between @IdMarcaIni and @IdMarcaFin
			
			) A
			where a.IdProducto between @IdProductoPadreIni and @IdProductoPadreFin
			GROUP BY A.IdEmpresa, A.IdUsuario, A.IdProducto,a.pr_descripcion, a.IdPresentacion, A.IdCategoria, A.IdLinea, A.IdGrupo, A.IdSubGrupo, A.IdMarca
		SET @AnioInicio = @AnioInicio + 1
		END
	END	
	ELSE
	BEGIN
		WHILE @AnioInicio <= @AnioFin
		BEGIN
			INSERT INTO [web].[in_SPINV_010]
					   ([IdEmpresa]
					   ,[IdUsuario]
					   ,[IdAnio]
					   ,[IdProducto]
					   ,[pr_descripcion]
					   ,[IdPresentacion]
					   ,[IdCategoria]
					   ,[IdLinea]
					   ,[IdGrupo]
					   ,[IdSubGrupo]
					   ,[IdMarca]
					   ,[Enero]
					   ,[Febrero]
					   ,[Marzo]
					   ,[Abril]
					   ,[Mayo]
					   ,[Junio]
					   ,[Julio]
					   ,[Agosto]
					   ,[Septiembre]
					   ,[Octubre]
					   ,[Noviembre]
					   ,[Diciembre]
					   ,[Total]
					   ,[StockActual])
			SELECT IdEmpresa, @IdUsuario, @AnioInicio, IdProducto,pr_descripcion, IdPresentacion, IdCategoria, IdLinea, IdGrupo, IdSubGrupo, IdMarca,
			0,0,0,0,0,0,0,0,0,0,0,0,0,0
			FROM in_Producto
			WHERE IdEmpresa = @IdEmpresa AND Estado = 'A' AND IdProducto_padre IS NULL and IdMarca between @IdMarcaIni and @IdMarcaFin
			and IdProducto between @IdProductoPadreIni and @IdProductoPadreFin
		SET @AnioInicio = @AnioInicio + 1
		END
	END
END

BEGIN --FILTRO POR CATEGORIZACION
	IF(@IdCategoria != '')
	BEGIN
		IF(@IdLinea != 0)
			BEGIN
				IF(@IdGrupo != 0)
					BEGIN
						IF(@IdSubGrupo != 0)
							BEGIN
								DELETE web.in_SPINV_010 
								WHERE IdCategoria <>  @IdCategoria
								AND IdLinea != @IdLinea
								AND IdGrupo != @IdGrupo
								AND IdSubGrupo != @IdSubGrupo
								AND IdEmpresa = @IdEmpresa
								AND IdUsuario = @IdUsuario
							END
						ELSE
							DELETE web.in_SPINV_010 
							WHERE IdCategoria <>  @IdCategoria
							AND IdLinea != @IdLinea
							AND IdGrupo != @IdGrupo
							AND IdEmpresa = @IdEmpresa
							AND IdUsuario = @IdUsuario
					END
				ELSE
					DELETE web.in_SPINV_010 
					WHERE IdCategoria <> @IdCategoria
					AND IdLinea != @IdLinea
					AND IdEmpresa = @IdEmpresa
					AND IdUsuario = @IdUsuario
			END
		ELSE
			DELETE web.in_SPINV_010 
			WHERE IdCategoria <> @IdCategoria
			AND IdEmpresa = @IdEmpresa
			AND IdUsuario = @IdUsuario
	END
END

SET @AnioInicio = YEAR(@FechaIni)

BEGIN --RELLENO VENTAS POR PERIODOS
	WHILE @AnioInicio <= @AnioFin
	BEGIN
		if(@PeriodoIni = @PeriodoFin)
			SET @PeriodoCorte = CAST(cast(@AnioInicio as varchar(4)) + RIGHT('00' + Ltrim(Rtrim(CAST( MONTH(@FechaFin) AS VARCHAR(2)))),2) AS INT)
		ELSE
			SET @PeriodoCorte = CAST(cast(@AnioInicio as varchar(4)) + '12' AS INT)
		
		WHILE @PeriodoIni <= @PeriodoCorte
		BEGIN
				SET @Mes = (@PeriodoIni) - (@AnioInicio * 100)
				
					update web.in_SPINV_010 SET 
					Enero = IIF(@MES = 1, A.Cantidad,Enero),
					Febrero = IIF(@MES = 2, A.Cantidad,Febrero),
					Marzo = IIF(@MES = 3, A.Cantidad,Marzo),
					Abril = IIF(@MES = 4, A.Cantidad,Abril),
					Mayo = IIF(@MES = 5, A.Cantidad,Mayo),
					Junio = IIF(@MES = 6, A.Cantidad,Junio),
					Julio = IIF(@MES = 7, A.Cantidad,Julio),
					Agosto = IIF(@MES = 8, A.Cantidad,Agosto),
					Septiembre = IIF(@MES = 9, A.Cantidad,Septiembre),
					Octubre = IIF(@MES = 10, A.Cantidad,Octubre),
					Noviembre = IIF(@MES = 11, A.Cantidad,Noviembre),
					Diciembre = IIF(@MES = 12, A.Cantidad,Diciembre)
					from(
						select G.IdEmpresa, G.IdProducto, SUM(G.Cantidad) Cantidad
						from(
						SELECT   FC.IdEmpresa, ISNULL(P.IdProducto_padre,P.IdProducto) IdProducto, FD.vt_cantidad Cantidad
						FROM            fa_factura AS FC INNER JOIN
						fa_factura_det AS FD ON FC.IdEmpresa = FD.IdEmpresa AND FC.IdSucursal = FD.IdSucursal AND FC.IdBodega = FD.IdBodega AND FC.IdCbteVta = FD.IdCbteVta INNER JOIN
						in_Producto AS P ON FD.IdEmpresa = P.IdEmpresa AND FD.IdProducto = P.IdProducto
						WHERE FC.IdEmpresa = @IdEmpresa AND CAST(CAST(YEAR(FC.vt_fecha) AS VARCHAR(4))
+ RIGHT('00'+CAST(MONTH(FC.vt_fecha) AS VARCHAR(2)),2) AS INT) = @PeriodoIni and P.IdMarca between @IdMarcaIni and @IdMarcaFin
						) G group by G.IdEmpresa, G.IdProducto
					) A WHERE web.in_SPINV_010.IdEmpresa = @IdEmpresa AND web.in_SPINV_010.IdProducto = A.IdProducto
					AND web.in_SPINV_010.IdUsuario = @IdUsuario and web.in_SPINV_010.IdAnio = @AnioInicio				

				SET @PeriodoIni = @PeriodoIni + 1
		END
		SET @AnioInicio = @AnioInicio + 1
	END
END

BEGIN --CALCULO TOTALES
	UPDATE [web].in_SPINV_010 SET Total = Enero + Febrero + Marzo + Abril + Mayo + Junio + Julio + Agosto + Septiembre + Octubre + Noviembre + Diciembre
	WHERE IdEmpresa = @IdEmpresa and IdUsuario = @IdUsuario
END

BEGIN --ACTUALIZO STOCK A LA FECHA
	UPDATE [web].in_SPINV_010 SET StockActual = A.Cantidad
	from(
		SELECT G.IdEmpresa, G.IdProducto, SUM(G.dm_cantidad) Cantidad
		FROM(
		SELECT D.IdEmpresa, ISNULL(P.IdProducto_padre,P.IdProducto) IdProducto, D.dm_cantidad
		FROM in_movi_inve_detalle AS D INNER JOIN in_Producto AS P 
		ON D.IdEmpresa = P.IdEmpresa AND D.IdProducto = P.IdProducto
		INNER JOIN in_movi_inve AS C ON C.IdEmpresa = D.IdEmpresa
		AND C.IdSucursal = D.IdSucursal AND C.IdBodega = D.IdBodega 
		AND C.IdMovi_inven_tipo = D.IdMovi_inven_tipo
		AND C.IdNumMovi = D.IdNumMovi
		WHERE C.IdEmpresa = @IdEmpresa AND C.cm_fecha <= GETDATE()
		) G GROUP BY G.IdEmpresa, G.IdProducto
	) A WHERE A.IdEmpresa = [web].in_SPINV_010.IdEmpresa 
	AND A.IdProducto = [web].in_SPINV_010.IdProducto
	AND [web].in_SPINV_010.IdUsuario = @IdUsuario
	AND [web].in_SPINV_010.IdEmpresa = @IdEmpresa
END

SELECT        web.in_SPINV_010.IdEmpresa, web.in_SPINV_010.IdUsuario, web.in_SPINV_010.IdAnio, web.in_SPINV_010.IdProducto,web.in_SPINV_010.pr_descripcion, web.in_SPINV_010.IdCategoria, web.in_SPINV_010.IdLinea, web.in_SPINV_010.IdGrupo, 
                         web.in_SPINV_010.IdSubGrupo, web.in_SPINV_010.IdMarca, web.in_SPINV_010.Enero, web.in_SPINV_010.Febrero, web.in_SPINV_010.Marzo, web.in_SPINV_010.Abril, web.in_SPINV_010.Mayo, web.in_SPINV_010.Junio, 
                         web.in_SPINV_010.Julio, web.in_SPINV_010.Agosto, web.in_SPINV_010.Septiembre, web.in_SPINV_010.Octubre, web.in_SPINV_010.Noviembre, web.in_SPINV_010.Diciembre, web.in_SPINV_010.Total, 
                         web.in_SPINV_010.StockActual, in_categorias.ca_Categoria, in_linea.nom_linea, in_grupo.nom_grupo, in_subgrupo.nom_subgrupo, in_Marca.Descripcion AS NomMarca, web.in_SPINV_010.IdPresentacion, 
                         in_presentacion.nom_presentacion
FROM            web.in_SPINV_010 INNER JOIN
                         in_Marca ON web.in_SPINV_010.IdEmpresa = in_Marca.IdEmpresa AND web.in_SPINV_010.IdMarca = in_Marca.IdMarca INNER JOIN
                         in_presentacion ON web.in_SPINV_010.IdEmpresa = in_presentacion.IdEmpresa AND web.in_SPINV_010.IdPresentacion = in_presentacion.IdPresentacion LEFT OUTER JOIN
                         in_linea INNER JOIN
                         in_categorias ON in_linea.IdEmpresa = in_categorias.IdEmpresa AND in_linea.IdCategoria = in_categorias.IdCategoria INNER JOIN
                         in_grupo ON in_linea.IdEmpresa = in_grupo.IdEmpresa AND in_linea.IdCategoria = in_grupo.IdCategoria AND in_linea.IdLinea = in_grupo.IdLinea INNER JOIN
                         in_subgrupo ON in_grupo.IdEmpresa = in_subgrupo.IdEmpresa AND in_grupo.IdCategoria = in_subgrupo.IdCategoria AND in_grupo.IdLinea = in_subgrupo.IdLinea AND in_grupo.IdGrupo = in_subgrupo.IdGrupo ON 
                         web.in_SPINV_010.IdEmpresa = in_subgrupo.IdEmpresa AND web.in_SPINV_010.IdCategoria = in_subgrupo.IdCategoria AND web.in_SPINV_010.IdLinea = in_subgrupo.IdLinea AND 
                         web.in_SPINV_010.IdGrupo = in_subgrupo.IdGrupo AND web.in_SPINV_010.IdSubGrupo = in_subgrupo.IdSubgrupo
WHERE        (web.in_SPINV_010.IdEmpresa = @IdEmpresa) AND (web.in_SPINV_010.IdUsuario = @IdUsuario)