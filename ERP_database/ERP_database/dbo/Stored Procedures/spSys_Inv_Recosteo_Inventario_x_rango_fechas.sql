--exec [dbo].[spSys_Inv_Recosteo_Inventario_x_rango_fechas] 1,10,3,'2018/04/01','2018/04/30',5
CREATE proc [dbo].[spSys_Inv_Recosteo_Inventario_x_rango_fechas] 
(
@IdEmpresa int,
@IdSucursal int,
@IdBodega int,
@Fecha_ini datetime,
@Fecha_fin datetime,
@cant_Decimales int
)
AS
BEGIN
/*
DECLARE @IdEmpresa int
DECLARE @IdSucursal int
DECLARE @IdBodega int
DECLARE @Fecha_ini datetime
DECLARE @Fecha_fin datetime
DECLARE @cant_Decimales int

SET @IdEmpresa = 1
SET @IdSucursal = 2
SET @IdBodega = 3
SET @Fecha_ini = '01/01/2016'
SET @Fecha_fin = '31/12/2016'
SET @cant_Decimales = 5
*/
--VARIABLES PARA CURSOR X SUCURSAL X BODEGA X PRODUCTO
	declare @C_IdEmpresa int 
	declare @C_IdSucursal int 
	declare @C_IdBodega int
	declare @C_IdProducto numeric

 --VARIABLES PARA CURSOR X MOVIMIENTO
	declare @C2_IdEmpresa int 
	declare @C2_IdSucursal int 
	declare @C2_IdBodega int 
	declare @C2_cm_fecha datetime
	declare @C2_IdProducto numeric	
	declare @C2_IdMovi_inven_tipo int 
	declare @C2_IdNumMovi numeric
	declare @C2_Secuencia int 
	declare @C2_dm_cantidad float
	declare @C2_mv_costo float

 -- VARIABLES PARA COSTEO
	DECLARE @W_Cantidad_compra float
	DECLARE @W_Costo_compra float
	DECLARE @W_Cantidad_a_la_fecha float
	DECLARE @W_Costo_total_a_la_fecha float
	DECLARE @W_Costo_promedio float
	DECLARE @W_Ult_Costo_promedio float
	DECLARE @W_IdFecha int
	DECLARE @W_Secuencia int

 --ELIMINO LA TABLA DE IN_PRODUCTO_X_TB_BODEGA_COSTO_HISTORICO LOS REGISTROS DE LA SUCURSAL Y BODEGA QUE VOY A RECOSTEAR
	PRINT 'LIMPIAR TABLA'
	DELETE in_producto_x_tb_bodega_Costo_Historico
	where IdEmpresa = @IdEmpresa
	and IdSucursal = @IdSucursal
	and IdBodega = @IdBodega
	and fecha >= @Fecha_ini

	PRINT 'ELIMINO TABLA DE RECOSTEO'
	--LIMPIO LA TABLA DE RECOSTEO 
	delete in_Recosteo_Productos_x_movi_inve_detalle

--RESETEO CAMPOS DE RECOSTEO
	PRINT 'RESETEO CAMPO DE RECOSTEO EN IN_MOVI_INVE'
UPDATE in_movi_inve_detalle set Costeado = 0
where exists(
	SELECT det.IdEmpresa,det.IdSucursal,det.IdBodega,det.IdProducto,cab.cm_fecha, det.dm_cantidad, det.mv_costo FROM in_movi_inve cab inner join in_movi_inve_detalle det
	on cab.IdEmpresa = det.IdEmpresa and cab.IdSucursal = det.IdSucursal and cab.IdBodega = det.IdBodega
	and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo and cab.IdNumMovi = det.IdNumMovi
	WHERE det.IdEmpresa = @IdEmpresa
	and det.IdSucursal = @IdSucursal
	and det.IdBodega = @IdBodega
	and cab.cm_fecha >= @Fecha_ini
	and in_movi_inve_detalle.IdEmpresa = det.IdEmpresa
	and in_movi_inve_detalle.IdSucursal = det.IdSucursal
	and in_movi_inve_detalle.IdBodega = det.IdBodega
	and in_movi_inve_detalle.IdMovi_inven_tipo = det.IdMovi_inven_tipo
	and in_movi_inve_detalle.IdNumMovi = det.IdNumMovi
	and in_movi_inve_detalle.Secuencia = det.Secuencia
)

--INSERTO EN UNA TABLA TEMPORAL LOS REGISTROS QUE VOY A RECOSTEAR
	PRINT 'INSERTO EN TABLA TEMPORAL LOS REGISTROS QUE VOY A RECOSTEAR'
	insert into in_Recosteo_Productos_x_movi_inve_detalle
	(IdEmpresa,IdSucursal,IdBodega,IdProducto)
	select cab.IdEmpresa,cab.IdSucursal,cab.IdBodega,det.IdProducto 
	from in_movi_inve_detalle det inner join
	in_movi_inve cab on 
	cab.IdEmpresa = det.IdEmpresa 
	and cab.IdSucursal = det.IdSucursal 
	and cab.IdBodega = det.IdBodega 
	and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo 
	and cab.IdNumMovi = det.IdNumMovi
	where cab.IdEmpresa = @IdEmpresa
	and cab.IdSucursal = @IdSucursal
	and cab.IdBodega = @IdBodega
	and cab.cm_fecha between @Fecha_ini and @Fecha_fin
	group by cab.IdEmpresa,cab.IdSucursal,cab.IdBodega,det.IdProducto


--DECLARO CURSOR POR PRODUCTO
		DECLARE product_cursor CURSOR FOR   
		SELECT  IdEmpresa, IdSucursal, IdBodega, IdProducto
		FROM    in_Recosteo_Productos_x_movi_inve_detalle
		
		OPEN			product_cursor  --ABRO CURSOR
		FETCH NEXT FROM product_cursor 
		INTO			@C_IdEmpresa, @C_IdSucursal, @C_IdBodega, @C_IdProducto

		IF @@FETCH_STATUS <> 0   
		PRINT 'NO HAY DATOS PARA CONTINUAR'       
  
		WHILE @@FETCH_STATUS = 0  
		BEGIN  --DECLARO CURSOR POR MOVIMIENTO DE EL PRODUCTO EN EL PRIMER CURSOR
				DECLARE Movi_Inven_x_product_cursor CURSOR FOR   
					select cab.IdEmpresa,cab.IdSucursal,cab.IdBodega,cab.cm_fecha,det.IdProducto,
					cab.IdMovi_inven_tipo,cab.IdNumMovi,det.Secuencia,dm_cantidad,mv_costo
					from in_movi_inve_detalle det inner join
					in_movi_inve cab on cab.IdEmpresa = det.IdEmpresa and cab.IdSucursal = det.IdSucursal 
					and cab.IdBodega = det.IdBodega and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo and cab.IdNumMovi = det.IdNumMovi 
					where cab.IdEmpresa = @C_IdEmpresa and cab.IdSucursal = @C_IdSucursal and cab.IdBodega = @C_IdBodega 
					and det.IdProducto = @C_IdProducto and cab.cm_fecha between @Fecha_ini and @Fecha_fin
					ORDER BY cab.IdEmpresa,cab.IdSucursal,cab.IdBodega,det.IdProducto,cab.cm_fecha asc, det.dm_cantidad desc
				OPEN Movi_Inven_x_product_cursor  
				FETCH NEXT FROM Movi_Inven_x_product_cursor 
				into @C2_IdEmpresa			,@C2_IdSucursal			,@C2_IdBodega		,@C2_cm_fecha			,@C2_IdProducto 
					,@C2_IdMovi_inven_tipo	,@C2_IdNumMovi			,@C2_Secuencia		,@C2_dm_cantidad		,@C2_mv_costo 

				IF @@FETCH_STATUS <> 0   
				PRINT 'NO EXISTE MOVIMIENTO PARA EL PRODUCTO'

				WHILE @@FETCH_STATUS = 0  
				BEGIN 
					SET @W_Cantidad_compra = 0
							SET @W_Costo_compra = 0
							SET @W_Cantidad_a_la_fecha = 0
							SET @W_Ult_Costo_promedio = 0
							SET @W_Secuencia = 0
							SET @W_Costo_promedio = 0
							SET @W_Costo_total_a_la_fecha = 0

					IF(@C2_dm_cantidad > 0) -- SI MOVIMIENTO ES POSITIVO INSERTO EN TABLA DE COSTO HISTORICO
					BEGIN
					PRINT 'ACTUALIZO CAMPO DE RECOSTEO'
						--ACTUALIZO CAMPO DE PROCESO DE RECOSTEO
							UPDATE in_movi_inve_detalle set Costeado = 1
							where IdEmpresa = @C2_IdEmpresa
							and IdSucursal = @C2_IdSucursal
							and IdBodega = @C2_IdBodega
							and IdMovi_inven_tipo = @C2_IdMovi_inven_tipo
							and IdNumMovi = @C2_IdNumMovi
							and Secuencia = @C2_Secuencia
						--CANTIDAD COMPRA
							set @W_Cantidad_compra = @C2_dm_cantidad 
						--COSTO COMPRA
							set @W_Costo_compra = ROUND(@C2_mv_costo ,5)
							PRINT 'OBTENGO STOCK'
						--CANTIDAD Y COSTO A LA FECHA
							SELECT @W_Cantidad_a_la_fecha = SUM(det.dm_cantidad)
							, @W_Costo_total_a_la_fecha = SUM(det.dm_cantidad * det.mv_costo)
							FROM in_movi_inve cab inner join in_movi_inve_detalle det
							on cab.IdEmpresa = det.IdEmpresa and cab.IdSucursal = det.IdSucursal and cab.IdBodega = det.IdBodega
							and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo and cab.IdNumMovi = det.IdNumMovi
							WHERE det.IdEmpresa = @C2_IdEmpresa and det.IdSucursal = @C2_IdSucursal and det.IdBodega = @C2_IdBodega
							and det.IdProducto = @C2_IdProducto and cab.cm_fecha <= @C2_cm_fecha and Costeado = 1 
							GROUP BY det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto								
							PRINT 'COSTO PROMEDIO'
						--COSTO PROMEDIO
							SET @W_Ult_Costo_promedio = 
							(
								SELECT top 1 costo FROM in_producto_x_tb_bodega_Costo_Historico
								WHERE IdEmpresa = @C2_IdEmpresa
								and IdSucursal = @C2_IdSucursal
								and IdBodega = @C2_IdBodega
								and IdProducto = @C2_IdProducto
								and fecha <= @C2_cm_fecha
								ORDER BY IdEmpresa,IdSucursal,IdBodega,IdProducto,fecha DESC, Secuencia desc
							)			
							PRINT'CREO IDFECHA'			
						--CREAR IDFECHA
							SET @W_IdFecha = CAST(CAST(YEAR(@C2_cm_fecha) AS VARCHAR) + (REPLICATE('0',2-LEN(MONTH(@C2_cm_fecha))) + CAST(MONTH(@C2_cm_fecha) AS VARCHAR)) + (REPLICATE('0',2-LEN(DAY(@C2_cm_fecha))) + CAST(DAY(@C2_cm_fecha) AS VARCHAR)) AS INT)
						--OBTENER SECUENCIA
							PRINT 'OBTENGO SECUENCIA'
							SELECT @W_Secuencia = max(Secuencia)+1 FROM in_producto_x_tb_bodega_Costo_Historico
							WHERE IdEmpresa = @C2_IdEmpresa
								and IdSucursal = @C2_IdSucursal
								and IdBodega = @C2_IdBodega
								and IdProducto = @C2_IdProducto
								and fecha = @C2_cm_fecha															
						--CORRECCION DE DATA Y CALCULO DE COSTO PROMEDIO
							SET @W_Cantidad_compra = ISNULL(@W_Cantidad_compra,0)
							SET @W_Costo_compra = ISNULL(@W_Costo_compra,0)
							SET @W_Cantidad_a_la_fecha = isnull(@W_Cantidad_a_la_fecha,0)
							SET @W_Ult_Costo_promedio = ISNULL(@W_Ult_Costo_promedio,0)
							SET @W_Secuencia = isnull(@W_Secuencia,1)
							
								BEGIN TRY
							SET @W_Costo_promedio = (@W_Costo_total_a_la_fecha)/ (@W_Cantidad_a_la_fecha)
								END TRY
								BEGIN CATCH
							SET @W_Costo_promedio = @W_Ult_Costo_promedio
								END CATCH

							SET @W_Costo_promedio = abs(@W_Costo_promedio)

						--INSERTO NUEVO COSTO 
						PRINT 'INSERTO NUEVO COSTO'
							INSERT INTO [dbo].[in_producto_x_tb_bodega_Costo_Historico]
							([IdEmpresa]            ,[IdSucursal]           ,[IdBodega]           ,[IdProducto]
							,[IdFecha]	            ,[Secuencia]	        ,[fecha]		
							,[costo]				,[Stock_a_la_fecha]     
							,[Observacion]			,[fecha_trans])
							VALUES
							(
							@C2_IdEmpresa			,@C2_IdSucursal			,@C2_IdBodega			,@C2_IdProducto
							,@W_IdFecha				,@W_Secuencia			,@C2_cm_fecha			
							,@W_Costo_promedio		,@W_Cantidad_a_la_fecha + @C2_dm_cantidad
							,'SU'+CAST(@C2_IdSucursal AS VARCHAR) + ' BO'+CAST( @C2_IdBodega AS VARCHAR) + 'TM'+CAST(@C2_IdMovi_inven_tipo AS VARCHAR)+ 'NM'+CAST(@C2_IdNumMovi AS VARCHAR)
							,GETDATE()
							)
							
					END
					ELSE
					BEGIN							
						PRINT 'GET ULTIMO COSTO PROMEDIO'
						--COSTO PROMEDIO
							SET @W_Ult_Costo_promedio = 
							(
								SELECT top 1 costo FROM in_producto_x_tb_bodega_Costo_Historico
								WHERE IdEmpresa = @C2_IdEmpresa
								and IdSucursal = @C2_IdSucursal
								and IdBodega = @C2_IdBodega
								and IdProducto = @C2_IdProducto
								and fecha <= @C2_cm_fecha
								ORDER BY IdEmpresa,IdSucursal,IdBodega,IdProducto,fecha DESC, Secuencia desc
							)		
						--CORRECCION DE DATA Y CALCULO DE COSTO PROMEDIO
								SET @W_Cantidad_compra = ISNULL(@W_Cantidad_compra,0)
								SET @W_Costo_compra = ISNULL(@W_Costo_compra,0)
								SET @W_Cantidad_a_la_fecha = isnull(@W_Cantidad_a_la_fecha,0)
								SET @W_Ult_Costo_promedio = ISNULL(@W_Ult_Costo_promedio,0)		
						--ACTUALIZO CAMPO DE PROCESO DE RECOSTEO
								UPDATE in_movi_inve_detalle  set mv_costo = @W_Ult_Costo_promedio, Costeado = 1
								where IdEmpresa = @C2_IdEmpresa
								and IdSucursal = @C2_IdSucursal
								and IdBodega = @C2_IdBodega
								and IdMovi_inven_tipo = @C2_IdMovi_inven_tipo
								and IdNumMovi = @C2_IdNumMovi
								and Secuencia = @C2_Secuencia



								UPDATE in_Ing_Egr_Inven_det set mv_costo = @W_Ult_Costo_promedio
								where IdEmpresa_inv = @C2_IdEmpresa
								and IdSucursal_inv = @C2_IdSucursal
								and IdBodega_inv = @C2_IdBodega
								and IdMovi_inven_tipo_inv = @C2_IdMovi_inven_tipo
								and IdNumMovi_inv = @C2_IdNumMovi
								and secuencia_inv = @C2_Secuencia
						
					END
				FETCH NEXT FROM Movi_Inven_x_product_cursor 
					into @C2_IdEmpresa		,@C2_IdSucursal			,@C2_IdBodega	,@C2_cm_fecha		,@C2_IdProducto 
					,@C2_IdMovi_inven_tipo	,@C2_IdNumMovi	,@C2_Secuencia		,@C2_dm_cantidad 
					,@C2_mv_costo 
				END  
				CLOSE Movi_Inven_x_product_cursor  
				DEALLOCATE Movi_Inven_x_product_cursor
				
		FETCH NEXT FROM product_cursor 
		INTO @C_IdEmpresa, @C_IdSucursal, @C_IdBodega, @C_IdProducto
		END  
		CLOSE product_cursor  --CIERRO CURSOR
		DEALLOCATE product_cursor  

UPDATE in_movi_inve_detalle set mv_costo = a.costo_egreso 
FROM(
SELECT in_transferencia.IdEmpresa, in_transferencia.IdTransferencia, in_transferencia.tr_fecha, in_transferencia.IdSucursalOrigen, in_transferencia.IdBodegaOrigen, in_transferencia.IdMovi_inven_tipo_SucuOrig, in_transferencia.IdNumMovi_Ing_Egr_Inven_Origen,
				ingreso.mv_costo AS costo_ingreso, 
                egreso.mv_costo AS costo_egreso,
egreso.IdEmpresa_inv IdEmpresa_egr, egreso.IdSucursal_inv IdSucursal_egr, egreso.IdBodega_inv IdBodega_egr, egreso.IdMovi_inven_tipo_inv IdMovi_inven_tipo_egr, egreso.IdNumMovi_inv IdNumMovi_inv_egr, egreso.secuencia_inv secuencia_egr, 
ingreso.IdEmpresa_inv AS IdEmpresa_ing, ingreso.IdSucursal_inv AS IdSucursal_ing, ingreso.IdBodega_inv AS IdBodega_ing, ingreso.IdMovi_inven_tipo_inv AS IdMovi_inven_tipo_ing, ingreso.IdNumMovi_inv AS IdNumMovi_ing, ingreso.secuencia_inv AS secuencia_ing
FROM     in_transferencia INNER JOIN
                  in_Ing_Egr_Inven_det AS egreso ON in_transferencia.IdEmpresa = egreso.IdEmpresa AND in_transferencia.IdSucursalOrigen = egreso.IdSucursal AND in_transferencia.IdMovi_inven_tipo_SucuOrig = egreso.IdMovi_inven_tipo AND 
                  in_transferencia.IdNumMovi_Ing_Egr_Inven_Origen = egreso.IdNumMovi INNER JOIN
                  in_Ing_Egr_Inven_det AS ingreso ON in_transferencia.IdEmpresa_Ing_Egr_Inven_Destino = ingreso.IdEmpresa AND in_transferencia.IdSucursal_Ing_Egr_Inven_Destino = ingreso.IdSucursal AND 
                  in_transferencia.IdMovi_inven_tipo_SucuDest = ingreso.IdMovi_inven_tipo AND in_transferencia.IdNumMovi_Ing_Egr_Inven_Destino = ingreso.IdNumMovi AND egreso.Secuencia = ingreso.Secuencia
WHERE  in_transferencia.tr_fecha between @Fecha_ini and GETDATE()
and in_transferencia.IdEmpresa = @IdEmpresa
and in_transferencia.IdSucursalOrigen = @IdSucursal
and in_transferencia.IdBodegaOrigen = @IdBodega
and in_transferencia.Estado = 'A'
) A
where in_movi_inve_detalle.IdEmpresa = a.IdEmpresa_ing
and in_movi_inve_detalle.IdSucursal = a.IdSucursal_ing
and in_movi_inve_detalle.IdBodega = a.IdBodega_ing
and in_movi_inve_detalle.IdMovi_inven_tipo = a.IdMovi_inven_tipo_ing
and in_movi_inve_detalle.IdNumMovi = a.IdNumMovi_ing
and in_movi_inve_detalle.Secuencia = a.secuencia_ing


UPDATE in_Ing_Egr_Inven_det set mv_costo = a.costo_egreso 
FROM(
SELECT in_transferencia.IdEmpresa, in_transferencia.IdTransferencia, in_transferencia.tr_fecha, in_transferencia.IdSucursalOrigen, in_transferencia.IdBodegaOrigen, in_transferencia.IdMovi_inven_tipo_SucuOrig, in_transferencia.IdNumMovi_Ing_Egr_Inven_Origen,
				ingreso.mv_costo AS costo_ingreso, 
                egreso.mv_costo AS costo_egreso,
egreso.IdEmpresa_inv IdEmpresa_egr, egreso.IdSucursal_inv IdSucursal_egr, egreso.IdBodega_inv IdBodega_egr, egreso.IdMovi_inven_tipo_inv IdMovi_inven_tipo_egr, egreso.IdNumMovi_inv IdNumMovi_inv_egr, egreso.secuencia_inv secuencia_egr, 
ingreso.IdEmpresa AS IdEmpresa_ing, ingreso.IdSucursal AS IdSucursal_ing, ingreso.IdBodega_inv AS IdBodega_ing, ingreso.IdMovi_inven_tipo AS IdMovi_inven_tipo_ing, ingreso.IdNumMovi AS IdNumMovi_ing, ingreso.Secuencia AS secuencia_ing
FROM     in_transferencia INNER JOIN
                  in_Ing_Egr_Inven_det AS egreso ON in_transferencia.IdEmpresa = egreso.IdEmpresa AND in_transferencia.IdSucursalOrigen = egreso.IdSucursal AND in_transferencia.IdMovi_inven_tipo_SucuOrig = egreso.IdMovi_inven_tipo AND 
                  in_transferencia.IdNumMovi_Ing_Egr_Inven_Origen = egreso.IdNumMovi INNER JOIN
                  in_Ing_Egr_Inven_det AS ingreso ON in_transferencia.IdEmpresa_Ing_Egr_Inven_Destino = ingreso.IdEmpresa AND in_transferencia.IdSucursal_Ing_Egr_Inven_Destino = ingreso.IdSucursal AND 
                  in_transferencia.IdMovi_inven_tipo_SucuDest = ingreso.IdMovi_inven_tipo AND in_transferencia.IdNumMovi_Ing_Egr_Inven_Destino = ingreso.IdNumMovi AND egreso.Secuencia = ingreso.Secuencia
WHERE  in_transferencia.tr_fecha between @Fecha_ini and GETDATE()
and in_transferencia.IdEmpresa = @IdEmpresa
and in_transferencia.IdSucursalOrigen = @IdSucursal
and in_transferencia.IdBodegaOrigen = @IdBodega
and in_transferencia.Estado = 'A'
) A
where in_Ing_Egr_Inven_det.IdEmpresa = a.IdEmpresa_ing
and in_Ing_Egr_Inven_det.IdSucursal = a.IdSucursal_ing
and in_Ing_Egr_Inven_det.IdMovi_inven_tipo = a.IdMovi_inven_tipo_ing
and in_Ing_Egr_Inven_det.IdNumMovi = a.IdNumMovi_ing
and in_Ing_Egr_Inven_det.Secuencia = a.secuencia_ing

END