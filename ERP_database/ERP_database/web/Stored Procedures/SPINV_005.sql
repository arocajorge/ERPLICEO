CREATE PROCEDURE [web].[SPINV_005] 	
@IdEmpresa int,
@IdSucursal_ini int,
@IdSucursal_fin int,
@IdBodega_ini int,
@IdBodega_fin int,
@IdProducto_ini numeric,
@IdProducto_fin numeric,
@Fecha_ini datetime,
@Fecha_fin datetime,
@IdUsuario varchar(20),
@No_Mostrar_valores_en_0 bit,
@Mostrar_detallado bit,
@IdProductoPadre_ini numeric,
@IdProductoPadre_fin numeric
AS
BEGIN

--ELIMINO REGISTROS DE LA TABLA DEL REPORTE
DELETE [web].[in_SPINV_005] WHERE IdUsuario = @IdUsuario

--INSERTO REGISTROS CON MOVIMIENTOS 
			INSERT INTO [web].[in_SPINV_005]
           ([IdEmpresa]           ,[IdSucursal]           ,[IdBodega]           ,[IdProducto]		,[IdUsuario]
		   ,[Saldo_ini_cant]      ,[Saldo_ini_cost]       ,[Saldo_fin_cant]     ,[Saldo_fin_cost]
		   ,[mov_ing_cant]		  ,[mov_ing_cost]		  ,[mov_egr_cant]		,[mov_egr_cost]		,[IdProductoPadre])
			SELECT		det.IdEmpresa	      ,det.IdSucursal		  ,det.IdBodega		    ,det.IdProducto 	,@IdUsuario
			,0					  ,0					  ,0					,0
			,0					  ,0					  ,0					,0			,isnull(p.IdProducto_padre,0)
			FROM		in_movi_inve cab inner join in_movi_inve_detalle det
			on cab.IdEmpresa = det.IdEmpresa and cab.IdSucursal = det.IdSucursal
			and cab.IdBodega = det.IdBodega and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
			and cab.IdNumMovi = det.IdNumMovi inner join in_Producto as p
			on det.IdEmpresa = p.IdEmpresa and det.IdProducto = p.IdProducto
			WHERE cab.IdEmpresa = @IdEmpresa
			and cab.IdSucursal between @IdSucursal_ini and @IdSucursal_fin
			and cab.IdBodega between @IdBodega_ini and @IdBodega_fin
			and det.IdProducto between @IdProducto_ini and @IdProducto_fin
			and cab.cm_fecha <= @Fecha_fin
			and isnull(p.IdProducto_padre,p.IdProducto) between @IdProductoPadre_ini and @IdProductoPadre_fin
			group by det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto, p.IdProducto_padre

--SI NO DEBE MOSTRAR ITEMS SIN MOVIMIENTOS BORRO LOS ITEMS QUE NO TUVIERON MOVIMIENTO EN ESE RANGO DE FECHAS
		if(@No_Mostrar_valores_en_0 = 1)
		begin
			DELETE [web].[in_SPINV_005]
			WHERE NOT EXISTS(
				SELECT det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto
				FROM in_movi_inve cab inner join in_movi_inve_detalle det
				on cab.IdEmpresa = det.IdEmpresa and cab.IdSucursal = det.IdSucursal
				and cab.IdBodega = det.IdBodega and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
				and cab.IdNumMovi = det.IdNumMovi
				WHERE cab.IdEmpresa = @IdEmpresa
				and cab.IdSucursal between @IdSucursal_ini and @IdSucursal_fin
				and cab.IdBodega between @IdBodega_ini and @IdBodega_fin
				and det.IdProducto between @IdProducto_ini and @IdProducto_fin
				and cab.cm_fecha BETWEEN @Fecha_ini AND @Fecha_fin
			
				and cab.IdEmpresa = [web].[in_SPINV_005].[IdEmpresa]
				and cab.IdSucursal = [web].[in_SPINV_005].[IdSucursal]
				and cab.IdBodega = [web].[in_SPINV_005].[IdBodega]
				and det.IdProducto = [web].[in_SPINV_005].[IdProducto]
			
				group by det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto
				) AND IdUsuario = @IdUsuario
		end

--ACTUALIZO SALDO INICIAL
UPDATE [web].[in_SPINV_005]
   SET [Saldo_ini_cant] = A.Saldo_ini_cant
      ,[Saldo_ini_cost] = A.Saldo_ini_cost
FROM(
			SELECT        det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto, sum(det.dm_cantidad) Saldo_ini_cant, sum(det.dm_cantidad * mv_costo) Saldo_ini_cost
			FROM            in_movi_inve AS cab INNER JOIN
							in_movi_inve_detalle AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdSucursal = det.IdSucursal AND cab.IdBodega = det.IdBodega AND 
							cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo AND cab.IdNumMovi = det.IdNumMovi
							WHERE cab.IdEmpresa = @IdEmpresa
							and cab.IdSucursal between @IdSucursal_ini and @IdSucursal_fin
							and cab.IdBodega between @IdBodega_ini and @IdBodega_fin
							and det.IdProducto between @IdProducto_ini and @IdProducto_fin
							and cab.cm_fecha < @Fecha_ini
							group by det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto
) A
where A.IdEmpresa = [web].[in_SPINV_005].IdEmpresa
and A.IdSucursal = [web].[in_SPINV_005].IdSucursal
AND A.IdBodega = [web].[in_SPINV_005].IdBodega
AND A.IdProducto = [web].[in_SPINV_005].IdProducto
AND [web].[in_SPINV_005].[IdUsuario] = @IdUsuario

--ACTUALIZO SALDO FINAL
UPDATE [web].[in_SPINV_005]
   SET [Saldo_fin_cant] = A.Saldo_fin_cant
      ,[Saldo_fin_cost] = A.Saldo_fin_cost
FROM(
			SELECT        det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto, sum(det.dm_cantidad) Saldo_fin_cant, sum(det.dm_cantidad * mv_costo) Saldo_fin_cost
			FROM            in_movi_inve AS cab INNER JOIN
							in_movi_inve_detalle AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdSucursal = det.IdSucursal AND cab.IdBodega = det.IdBodega AND 
							cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo AND cab.IdNumMovi = det.IdNumMovi
							WHERE cab.IdEmpresa = @IdEmpresa
							and cab.IdSucursal between @IdSucursal_ini and @IdSucursal_fin
							and cab.IdBodega between @IdBodega_ini and @IdBodega_fin
							and det.IdProducto between @IdProducto_ini and @IdProducto_fin
							and cab.cm_fecha <= @Fecha_fin
							group by det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdProducto
) A
where A.IdEmpresa = [web].[in_SPINV_005].IdEmpresa
and A.IdSucursal = [web].[in_SPINV_005].IdSucursal
AND A.IdBodega = [web].[in_SPINV_005].IdBodega
AND A.IdProducto = [web].[in_SPINV_005].IdProducto
AND [web].[in_SPINV_005].[IdUsuario] = @IdUsuario

IF(@Mostrar_detallado = 0)
BEGIN
--ACTUALIZO LOS INGRESOS Y EGRESOS QUE TUVIERON LOS REGISTROS DURANTE EL RANGO DE FECHAS
		UPDATE [web].[in_SPINV_005] 
		SET mov_egr_cant = ABS(A.cant_egr)
		,mov_egr_cost = ABS(A.cost_total_egr)
		,mov_ing_cant = ABS(A.cant_ing)
		,mov_ing_cost = ABS(A.cos_total_ing)
		FROM(
				SELECT        in_movi_inve_detalle.IdEmpresa, in_movi_inve_detalle.IdSucursal, in_movi_inve_detalle.IdBodega, in_movi_inve_detalle.IdProducto, 
						sum(iif(in_movi_inve_detalle.dm_cantidad > 0,in_movi_inve_detalle.dm_cantidad,0)) cant_ing ,
						sum(iif(in_movi_inve_detalle.dm_cantidad > 0,in_movi_inve_detalle.dm_cantidad,0) * iif(in_movi_inve_detalle.dm_cantidad > 0,in_movi_inve_detalle.mv_costo,0)) cos_total_ing,
						sum(iif(in_movi_inve_detalle.dm_cantidad < 0,in_movi_inve_detalle.dm_cantidad,0)) cant_egr ,
						sum(iif(in_movi_inve_detalle.dm_cantidad < 0,in_movi_inve_detalle.dm_cantidad,0) * iif(in_movi_inve_detalle.dm_cantidad < 0,in_movi_inve_detalle.mv_costo,0)) cost_total_egr							
				FROM            in_movi_inve INNER JOIN
						in_movi_inve_detalle ON in_movi_inve.IdEmpresa = in_movi_inve_detalle.IdEmpresa AND in_movi_inve.IdSucursal = in_movi_inve_detalle.IdSucursal AND 
						in_movi_inve.IdBodega = in_movi_inve_detalle.IdBodega AND in_movi_inve.IdMovi_inven_tipo = in_movi_inve_detalle.IdMovi_inven_tipo AND 
						in_movi_inve.IdNumMovi = in_movi_inve_detalle.IdNumMovi
				WHERE	in_movi_inve.cm_fecha between @Fecha_ini and @Fecha_fin
				group by in_movi_inve_detalle.IdEmpresa, in_movi_inve_detalle.IdSucursal, in_movi_inve_detalle.IdBodega, in_movi_inve_detalle.IdProducto
		) A
		WHERE A.IdEmpresa = [web].[in_SPINV_005].IdEmpresa
		and A.IdSucursal = [web].[in_SPINV_005].IdSucursal
		and A.IdBodega = [web].[in_SPINV_005].IdBodega
		and A.IdProducto = [web].[in_SPINV_005].IdProducto
		and [web].[in_SPINV_005].IdUsuario = @IdUsuario
END



--VISTA FINAL
SELECT * FROM (
				SELECT        det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdMovi_inven_tipo, in_Ing_Egr_Inven_det.IdNumMovi, det.Secuencia, det.IdProducto
						,[web].[in_SPINV_005].Saldo_ini_cant,([web].[in_SPINV_005].Saldo_ini_cost / iif([web].[in_SPINV_005].Saldo_ini_cant=0,1,[web].[in_SPINV_005].Saldo_ini_cant)) Cost_prom_ini, [web].[in_SPINV_005].Saldo_ini_cost
						
						,isnull(iif(det.dm_cantidad > 0,abs(det.dm_cantidad),0),0) cant_ing
						,isnull(iif(det.dm_cantidad > 0,abs(det.mv_costo),0),0) cost_ing
						,isnull(iif(det.dm_cantidad > 0,abs(det.dm_cantidad * det.mv_costo),0),0) total_ing
						,isnull(iif(det.dm_cantidad < 0,abs(det.dm_cantidad),0),0) cant_egr
						,isnull(iif(det.dm_cantidad < 0,abs(det.mv_costo),0),0) cost_egr				
						,isnull(iif(det.dm_cantidad < 0,abs(det.dm_cantidad * det.mv_costo),0),0) total_egr
						,[web].[in_SPINV_005].Saldo_ini_cant + sum(isnull(det.dm_cantidad,0))
						over(partition by 
						det.IdEmpresa, det.IdSucursal,det.IdBodega, det.IdProducto
						order by det.IdEmpresa asc, det.IdSucursal asc ,det.IdBodega asc, det.IdProducto asc,cab.cm_fecha asc,det.dm_cantidad desc, det.IdNumMovi ASC,det.Secuencia ASC
						rows unbounded preceding) as Saldo_cant
						,
						([web].[in_SPINV_005].Saldo_ini_cost + sum(isnull(det.mv_costo*det.dm_cantidad,0))
						over(partition by 
						det.IdEmpresa, det.IdSucursal,det.IdBodega, det.IdProducto
						order by det.IdEmpresa asc, det.IdSucursal asc ,det.IdBodega asc, det.IdProducto asc,cab.cm_fecha asc,det.dm_cantidad desc, det.IdNumMovi ASC,det.Secuencia ASC
						rows unbounded preceding))
						/iif(
						([web].[in_SPINV_005].Saldo_ini_cant + sum(isnull(det.dm_cantidad,0))
						over(partition by 
						det.IdEmpresa, det.IdSucursal,det.IdBodega, det.IdProducto
						order by det.IdEmpresa asc, det.IdSucursal asc ,det.IdBodega asc, det.IdProducto asc,cab.cm_fecha asc,det.dm_cantidad desc, det.IdNumMovi ASC,det.Secuencia ASC
						rows unbounded preceding)) = 0 , 1 ,
						([web].[in_SPINV_005].Saldo_ini_cant + sum(isnull(det.dm_cantidad,0))
						over(partition by 
						det.IdEmpresa, det.IdSucursal,det.IdBodega, det.IdProducto
						order by det.IdEmpresa asc, det.IdSucursal asc ,det.IdBodega asc, det.IdProducto asc,cab.cm_fecha asc,det.dm_cantidad desc, det.IdNumMovi ASC,det.Secuencia ASC
						rows unbounded preceding)))
						 as Saldo_cost_prom
						,[web].[in_SPINV_005].Saldo_ini_cost + sum(isnull(det.mv_costo*det.dm_cantidad,0))
						over(partition by 
						det.IdEmpresa, det.IdSucursal,det.IdBodega, det.IdProducto
						order by det.IdEmpresa asc, det.IdSucursal asc ,det.IdBodega asc, det.IdProducto asc,cab.cm_fecha asc,det.dm_cantidad desc, det.IdNumMovi ASC,det.Secuencia ASC
						rows unbounded preceding) as Saldo_cost
						
						 ,[web].[in_SPINV_005].Saldo_fin_cant,([web].[in_SPINV_005].Saldo_fin_cost / iif([web].[in_SPINV_005].Saldo_fin_cant=0,1,[web].[in_SPINV_005].Saldo_fin_cant)) Cost_prom_fin, [web].[in_SPINV_005].Saldo_fin_cost
						, [web].[in_SPINV_005].IdUsuario, det.dm_observacion, cab.cm_fecha, in_movi_inven_tipo.cm_descripcionCorta as tipo_movi, tb_bodega.cod_bodega, tb_bodega.bo_Descripcion AS nom_bodega, 
						tb_sucursal.codigo AS cod_sucursal, tb_sucursal.Su_Descripcion AS nom_sucursal, in_Ing_Egr_Inven_det.IdEmpresa_oc, in_Ing_Egr_Inven_det.IdSucursal_oc, 
						in_Ing_Egr_Inven_det.IdOrdenCompra, 
						
						IIF(cp_orden_giro.co_factura is null, cast(cast(fa_factura.vt_numFactura as numeric) as varchar(20)), cast(cast(cp_orden_giro.co_factura as numeric) as varchar(20))) AS num_factura, 

						tb_persona.pe_nombreCompleto AS nom_proveedor, in_Producto.pr_codigo, 
						in_Producto.pr_descripcion + ' '+pre.nom_presentacion + ' ' + ISNULL(in_Producto.lote_num_lote,'') + ' ' + (iif(in_Producto.lote_fecha_vcto is null,'',CONVERT(varchar(10), in_Producto.lote_fecha_vcto, 103))) pr_descripcion, 
						in_UnidadMedida.IdUnidadMedida, in_UnidadMedida.Descripcion as nom_unidad_consumo, in_UnidadMedida.cod_alterno as cod_unidad_consumo, [web].[in_SPINV_005].[IdProductoPadre]
FROM            fa_factura INNER JOIN
                         fa_factura_x_in_Ing_Egr_Inven ON fa_factura.IdEmpresa = fa_factura_x_in_Ing_Egr_Inven.IdEmpresa_fa AND fa_factura.IdSucursal = fa_factura_x_in_Ing_Egr_Inven.IdSucursal_fa AND 
                         fa_factura.IdBodega = fa_factura_x_in_Ing_Egr_Inven.IdBodega_fa AND fa_factura.IdCbteVta = fa_factura_x_in_Ing_Egr_Inven.IdCbteVta_fa AND fa_factura.IdEmpresa = fa_factura_x_in_Ing_Egr_Inven.IdEmpresa_fa AND 
                         fa_factura.IdSucursal = fa_factura_x_in_Ing_Egr_Inven.IdSucursal_fa AND fa_factura.IdBodega = fa_factura_x_in_Ing_Egr_Inven.IdBodega_fa AND fa_factura.IdCbteVta = fa_factura_x_in_Ing_Egr_Inven.IdCbteVta_fa RIGHT OUTER JOIN
                         in_movi_inve AS cab INNER JOIN
                         in_movi_inve_detalle AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdSucursal = det.IdSucursal AND cab.IdBodega = det.IdBodega AND cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo AND 
                         cab.IdNumMovi = det.IdNumMovi INNER JOIN
                         in_movi_inven_tipo ON cab.IdEmpresa = in_movi_inven_tipo.IdEmpresa AND cab.IdMovi_inven_tipo = in_movi_inven_tipo.IdMovi_inven_tipo INNER JOIN
                         tb_bodega ON cab.IdEmpresa = tb_bodega.IdEmpresa AND cab.IdSucursal = tb_bodega.IdSucursal AND cab.IdBodega = tb_bodega.IdBodega INNER JOIN
                         tb_sucursal ON tb_bodega.IdEmpresa = tb_sucursal.IdEmpresa AND tb_bodega.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                         in_Ing_Egr_Inven_det ON det.IdEmpresa = in_Ing_Egr_Inven_det.IdEmpresa_inv AND det.IdSucursal = in_Ing_Egr_Inven_det.IdSucursal_inv AND det.IdBodega = in_Ing_Egr_Inven_det.IdBodega_inv AND 
                         det.IdMovi_inven_tipo = in_Ing_Egr_Inven_det.IdMovi_inven_tipo_inv AND det.IdNumMovi = in_Ing_Egr_Inven_det.IdNumMovi_inv AND det.Secuencia = in_Ing_Egr_Inven_det.secuencia_inv RIGHT OUTER JOIN
                         in_Producto INNER JOIN
                         web.in_SPINV_005 ON in_Producto.IdEmpresa = web.in_SPINV_005.IdEmpresa AND in_Producto.IdProducto = web.in_SPINV_005.IdProducto ON det.IdEmpresa = web.in_SPINV_005.IdEmpresa AND 
                         det.IdSucursal = web.in_SPINV_005.IdSucursal AND det.IdBodega = web.in_SPINV_005.IdBodega AND det.IdProducto = web.in_SPINV_005.IdProducto INNER JOIN
                         in_UnidadMedida ON in_UnidadMedida.IdUnidadMedida = in_Producto.IdUnidadMedida_Consumo ON fa_factura_x_in_Ing_Egr_Inven.IdEmpresa_in_eg_x_inv = in_Ing_Egr_Inven_det.IdEmpresa AND 
                         fa_factura_x_in_Ing_Egr_Inven.IdSucursal_in_eg_x_inv = in_Ing_Egr_Inven_det.IdSucursal AND fa_factura_x_in_Ing_Egr_Inven.IdMovi_inven_tipo_in_eg_x_inv = in_Ing_Egr_Inven_det.IdMovi_inven_tipo AND 
                         fa_factura_x_in_Ing_Egr_Inven.IdNumMovi_in_eg_x_inv = in_Ing_Egr_Inven_det.IdNumMovi LEFT OUTER JOIN
                         tb_persona INNER JOIN
                         cp_proveedor ON tb_persona.IdPersona = cp_proveedor.IdPersona INNER JOIN
                         com_ordencompra_local ON cp_proveedor.IdEmpresa = com_ordencompra_local.IdEmpresa AND cp_proveedor.IdProveedor = com_ordencompra_local.IdProveedor ON 
                         in_Ing_Egr_Inven_det.IdEmpresa_oc = com_ordencompra_local.IdEmpresa AND in_Ing_Egr_Inven_det.IdSucursal_oc = com_ordencompra_local.IdSucursal AND 
                         in_Ing_Egr_Inven_det.IdOrdenCompra = com_ordencompra_local.IdOrdenCompra LEFT OUTER JOIN
                         cp_orden_giro_x_in_Ing_Egr_Inven INNER JOIN
                         cp_orden_giro ON cp_orden_giro_x_in_Ing_Egr_Inven.og_IdEmpresa = cp_orden_giro.IdEmpresa AND cp_orden_giro_x_in_Ing_Egr_Inven.og_IdCbteCble_Ogiro = cp_orden_giro.IdCbteCble_Ogiro AND 
                         cp_orden_giro_x_in_Ing_Egr_Inven.og_IdTipoCbte_Ogiro = cp_orden_giro.IdTipoCbte_Ogiro ON in_Ing_Egr_Inven_det.IdEmpresa = cp_orden_giro_x_in_Ing_Egr_Inven.inv_IdEmpresa AND 
                         in_Ing_Egr_Inven_det.IdSucursal = cp_orden_giro_x_in_Ing_Egr_Inven.inv_IdSucursal AND in_Ing_Egr_Inven_det.IdMovi_inven_tipo = cp_orden_giro_x_in_Ing_Egr_Inven.inv_IdMovi_inven_tipo AND 
                         in_Ing_Egr_Inven_det.IdNumMovi = cp_orden_giro_x_in_Ing_Egr_Inven.inv_IdNumMovi  LEFT OUTER JOIN
                         in_presentacion AS pre ON in_Producto.IdEmpresa = pre.IdEmpresa AND in_Producto.IdPresentacion = pre.IdPresentacion
						 
						 
						WHERE 
						det.IdEmpresa=@IdEmpresa
						and det.IdSucursal between @IdSucursal_ini and @IdSucursal_fin
						and det.IdBodega between @IdBodega_ini and @IdBodega_fin
						and [web].[in_SPINV_005].IdUsuario = @IdUsuario 
						and cab.cm_fecha between @Fecha_ini and @Fecha_fin
						AND @Mostrar_detallado = 1
						


				UNION
						SELECT        [web].[in_SPINV_005].IdEmpresa, [web].[in_SPINV_005].IdSucursal, [web].[in_SPINV_005].IdBodega, null, null, null, [web].[in_SPINV_005].IdProducto
						,[web].[in_SPINV_005].Saldo_ini_cant
						,([web].[in_SPINV_005].Saldo_ini_cost / iif([web].[in_SPINV_005].Saldo_ini_cant=0,1,[web].[in_SPINV_005].Saldo_ini_cant)) Cost_prom_ini, [web].[in_SPINV_005].Saldo_ini_cost
						,0 cant_ing
						,0 cost_ing
						,0 total_ing
						,0 cant_egr
						,0 cost_egr				
						,0 total_egr
						,[web].[in_SPINV_005].Saldo_ini_cant Saldo_cant
						,([web].[in_SPINV_005].Saldo_ini_cost / iif([web].[in_SPINV_005].Saldo_ini_cant=0,1,[web].[in_SPINV_005].Saldo_ini_cant)) as Saldo_cost_prom
						,[web].[in_SPINV_005].Saldo_ini_cost Saldo_cost
						,[web].[in_SPINV_005].Saldo_fin_cant,([web].[in_SPINV_005].Saldo_fin_cost / iif([web].[in_SPINV_005].Saldo_fin_cant=0,1,[web].[in_SPINV_005].Saldo_fin_cant)) Cost_prom_fin, [web].[in_SPINV_005].Saldo_fin_cost
						,[web].[in_SPINV_005].IdUsuario, '', @Fecha_ini, '' as tipo_movi, tb_bodega.cod_bodega, tb_bodega.bo_Descripcion AS nom_bodega, 
						tb_sucursal.codigo AS cod_sucursal, tb_sucursal.Su_Descripcion AS nom_sucursal, null, null, 
						null, null AS num_factura, null AS nom_proveedor, in_Producto.pr_codigo, 

						in_Producto.pr_descripcion + ' '+pre.nom_presentacion + ' ' + ISNULL(in_Producto.lote_num_lote,'') + ' ' + (iif(in_Producto.lote_fecha_vcto is null,'',CONVERT(varchar(10), in_Producto.lote_fecha_vcto, 103))) pr_descripcion, 

						in_UnidadMedida.IdUnidadMedida, in_UnidadMedida.Descripcion as nom_unidad_consumo, in_UnidadMedida.cod_alterno as cod_unidad_consumo, [web].[in_SPINV_005].[IdProductoPadre]
						FROM            in_Producto INNER JOIN
						[web].[in_SPINV_005] ON in_Producto.IdEmpresa = [web].[in_SPINV_005].IdEmpresa AND in_Producto.IdProducto = [web].[in_SPINV_005].IdProducto INNER JOIN
						tb_sucursal INNER JOIN
						tb_bodega ON tb_sucursal.IdEmpresa = tb_bodega.IdEmpresa AND tb_sucursal.IdSucursal = tb_bodega.IdSucursal ON 
						[web].[in_SPINV_005].IdEmpresa = tb_bodega.IdEmpresa AND [web].[in_SPINV_005].IdSucursal = tb_bodega.IdSucursal AND [web].[in_SPINV_005].IdBodega = tb_bodega.IdBodega
						inner join in_UnidadMedida on in_UnidadMedida.IdUnidadMedida = in_Producto.IdUnidadMedida_Consumo
						
						LEFT JOIN in_presentacion as pre on in_Producto.IdEmpresa = pre.IdEmpresa and in_Producto.IdPresentacion = pre.IdPresentacion
						WHERE NOT EXISTS(
						SELECT cab.IdEmpresa FROM in_movi_inve cab inner join in_movi_inve_detalle det
						on cab.IdEmpresa = det.IdEmpresa
						and cab.IdSucursal = det.IdSucursal
						and cab.IdBodega = det.IdBodega
						and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
						and cab.IdNumMovi = det.IdNumMovi
						where cab.cm_fecha between @Fecha_ini and @Fecha_fin
						and det.IdEmpresa = [web].[in_SPINV_005].IdEmpresa
						and det.IdSucursal = [web].[in_SPINV_005].IdSucursal
						and det.IdBodega = [web].[in_SPINV_005].IdBodega
						and det.IdProducto = [web].[in_SPINV_005].IdProducto
						)
						and @IdUsuario =[web].[in_SPINV_005].IdUsuario and @Mostrar_detallado = 1
						UNION						
						SELECT       [web].[in_SPINV_005].IdEmpresa, [web].[in_SPINV_005].IdSucursal, [web].[in_SPINV_005].IdBodega, null, null, null, [web].[in_SPINV_005].IdProducto
									,[web].[in_SPINV_005].Saldo_ini_cant
									,([web].[in_SPINV_005].Saldo_ini_cost / iif([web].[in_SPINV_005].Saldo_ini_cant=0,1,[web].[in_SPINV_005].Saldo_ini_cant)) Cost_prom_ini, [web].[in_SPINV_005].Saldo_ini_cost
									,[web].[in_SPINV_005].mov_ing_cant cant_ing
									,[web].[in_SPINV_005].mov_ing_cost / iif([web].[in_SPINV_005].mov_ing_cant = 0 , 1,[web].[in_SPINV_005].mov_ing_cant) cost_ing
									,[web].[in_SPINV_005].mov_ing_cost  total_ing
									,[web].[in_SPINV_005].mov_egr_cant cant_egr
									,[web].[in_SPINV_005].mov_egr_cost / iif([web].[in_SPINV_005].mov_egr_cant = 0 , 1,[web].[in_SPINV_005].mov_egr_cant) cost_egr				
									,[web].[in_SPINV_005].mov_egr_cost total_egr
									,[web].[in_SPINV_005].Saldo_ini_cant Saldo_cant
									,([web].[in_SPINV_005].Saldo_ini_cost / iif([web].[in_SPINV_005].Saldo_ini_cant=0,1,[web].[in_SPINV_005].Saldo_ini_cant)) as Saldo_cost_prom
									,[web].[in_SPINV_005].Saldo_ini_cost Saldo_cost
									,[web].[in_SPINV_005].Saldo_fin_cant,([web].[in_SPINV_005].Saldo_fin_cost / iif([web].[in_SPINV_005].Saldo_fin_cant=0,1,[web].[in_SPINV_005].Saldo_fin_cant)) Cost_prom_fin, [web].[in_SPINV_005].Saldo_fin_cost
									,[web].[in_SPINV_005].IdUsuario, '', @Fecha_ini, '' as tipo_movi, tb_bodega.cod_bodega, tb_bodega.bo_Descripcion AS nom_bodega, 
									tb_sucursal.codigo AS cod_sucursal, tb_sucursal.Su_Descripcion AS nom_sucursal, null, null, 
									null, null AS num_factura, null AS nom_proveedor, in_Producto.pr_codigo, 
									in_Producto.pr_descripcion + ' '+pre.nom_presentacion + ' ' + ISNULL(in_Producto.lote_num_lote,'') + ' ' + (iif(in_Producto.lote_fecha_vcto is null,'',CONVERT(varchar(10), in_Producto.lote_fecha_vcto, 103))) pr_descripcion,
									in_UnidadMedida.IdUnidadMedida, in_UnidadMedida.Descripcion as nom_unidad_consumo, in_UnidadMedida.cod_alterno as cod_unidad_consumo,
									[web].[in_SPINV_005].[IdProductoPadre]
						FROM            [web].[in_SPINV_005] INNER JOIN
								in_Producto ON [web].[in_SPINV_005].IdEmpresa = in_Producto.IdEmpresa AND [web].[in_SPINV_005].IdProducto = in_Producto.IdProducto INNER JOIN
								tb_sucursal INNER JOIN
								tb_bodega ON tb_sucursal.IdEmpresa = tb_bodega.IdEmpresa AND tb_sucursal.IdSucursal = tb_bodega.IdSucursal ON 
								[web].[in_SPINV_005].IdEmpresa = tb_bodega.IdEmpresa AND [web].[in_SPINV_005].IdSucursal = tb_bodega.IdSucursal AND [web].[in_SPINV_005].IdBodega = tb_bodega.IdBodega
								inner join in_UnidadMedida on in_UnidadMedida.IdUnidadMedida = in_Producto.IdUnidadMedida_Consumo
								LEFT JOIN in_presentacion as pre on in_Producto.IdEmpresa = pre.IdEmpresa and in_Producto.IdPresentacion = pre.IdPresentacion

						WHERE [web].[in_SPINV_005].IdUsuario = @IdUsuario 
						and @Mostrar_detallado = 0
						and tb_bodega.IdEmpresa=@IdEmpresa
						and tb_bodega.IdSucursal between @IdSucursal_ini and @IdSucursal_fin
						and tb_bodega.IdBodega between @IdBodega_ini and @IdBodega_fin
	


		) A ORDER BY A.IdEmpresa,A.IdSucursal,A.IdBodega,A.IdProducto,A.cm_fecha,A.cant_ing desc , A.cant_egr 


END
