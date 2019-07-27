
--exec [spACTF_Rpt014] 1,'28/02/2017','admin'
CREATE PROCEDURE [web].[SPACTF_004_resumen]
@IdEmpresa int,
@Fecha_corte datetime,
@IdUsuario varchar(20),
@IdActivoFijoTipo_ini int,
@IdActivoFijoTipo_fin int,
@IdCategoria_ini int,
@IdCategoria_fin int,
@Estado_Proceso varchar(20)
as
BEGIN

delete [web].[Af_SPACTF_004_detalle] where IdEmpresa = @IdEmpresa and IdUsuario = @IdUsuario
delete [web].[Af_SPACTF_004_resumen] where IdEmpresa = @IdEmpresa and IdUsuario = @IdUsuario
insert into [web].[Af_SPACTF_004_detalle]

select AF.IdEmpresa, AF.IdActivoFijo, @IdUsuario ,tb_sucursal.IdSucursal, tb_sucursal.Su_Descripcion,  AF.CodActivoFijo, AF.Af_Nombre, tipo.IdActivoFijoTipo IdActijoFijoTipo, tipo.Af_Descripcion tipo_AF, cat.IdCategoriaAF, cat.Descripcion Categoria_AF,
		af.Af_costo_compra, isnull(af.Af_Depreciacion_acum,0) + ISNULL(DEP.Valor_Depreciacion,0) Af_Depreciacion_acum, af.Af_costo_compra - (isnull(af.Af_Depreciacion_acum,0) + ISNULL(DEP.Valor_Depreciacion,0)) AS Costo_actual,
		ISNULL(ult_depreciacion.Valor_Depreciacion,0) valor_ult_depreciacion, af.Af_fecha_compra
FROM            Af_Activo_fijo AS AF INNER JOIN
										 Af_Activo_fijo_Categoria AS cat ON AF.IdEmpresa = cat.IdEmpresa AND AF.IdCategoriaAF = cat.IdCategoriaAF AND 
										 AF.IdActivoFijoTipo = cat.IdActivoFijoTipo INNER JOIN
										 Af_Activo_fijo_tipo AS tipo ON cat.IdEmpresa = tipo.IdEmpresa AND cat.IdActivoFijoTipo = tipo.IdActivoFijoTipo INNER JOIN
										 tb_sucursal ON AF.IdEmpresa = tb_sucursal.IdEmpresa AND AF.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
											(SELECT        det.IdEmpresa, det.IdActivoFijo, sum(det.Valor_Depreciacion) Valor_Depreciacion
												FROM            Af_Depreciacion AS cab INNER JOIN
															Af_Depreciacion_Det AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdDepreciacion = det.IdDepreciacion
												WHERE cab.IdEmpresa = @IdEmpresa and cab.Fecha_Depreciacion <= @Fecha_corte							 
												group by det.IdEmpresa, det.IdActivoFijo
											) AS DEP ON 
										 AF.IdEmpresa = DEP.IdEmpresa AND AF.IdActivoFijo = DEP.IdActivoFijo

										 LEFT OUTER JOIN 
(select * from (
			select
			af.IdEmpresa,
			af.IdActivoFijo,			
			cab.IdDepreciacion,
			cab.Fecha_Depreciacion,
			det.Valor_Depreciacion,
			row_number() over(partition by af.IdEmpresa, af.IdActivoFijo order by cab.Fecha_Depreciacion desc) as rn		
			FROM  Af_Depreciacion AS cab INNER JOIN
			Af_Depreciacion_Det AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdDepreciacion = det.IdDepreciacion INNER JOIN
			Af_Activo_fijo AS af ON det.IdEmpresa = af.IdEmpresa AND det.IdActivoFijo = af.IdActivoFijo
			WHERE cab.IdEmpresa = @IdEmpresa and cab.Fecha_Depreciacion <= @Fecha_corte
			) t
where t.rn = 1) AS ult_depreciacion on ult_depreciacion.IdEmpresa = af.IdEmpresa and ult_depreciacion.IdActivoFijo = af.IdActivoFijo and af.Af_fecha_compra <= @Fecha_corte
WHERE af.IdEmpresa = @IdEmpresa 
				AND NOT EXISTS(
				SELECT venta.IdEmpresa FROM Af_Venta_Activo venta
				where venta.IdEmpresa = af.IdEmpresa
				and venta.IdActivoFijo = af.IdActivoFijo				
				)
				and not exists(
				select retiro.IdEmpresa from Af_Retiro_Activo retiro
				where retiro.IdEmpresa = af.IdEmpresa
				and retiro.IdActivoFijo = af.IdActivoFijo				
				)
				and af.IdActivoFijoTipo between @IdActivoFijoTipo_ini and @IdCategoria_fin
				and af.IdCategoriaAF between @IdCategoria_ini and @IdCategoria_fin
				and af.Estado_Proceso like '%'+@Estado_Proceso+'%'


insert into [web].[Af_SPACTF_004_detalle]

select AF.IdEmpresa, AF.IdActivoFijo, @IdUsuario ,tb_sucursal.IdSucursal, tb_sucursal.Su_Descripcion,  AF.CodActivoFijo, AF.Af_Nombre, tipo.IdActivoFijoTipo IdActijoFijoTipo, tipo.Af_Descripcion tipo_AF, cat.IdCategoriaAF, cat.Descripcion Categoria_AF,
		af.Af_costo_compra, isnull(af.Af_Depreciacion_acum,0) + ISNULL(DEP.Valor_Depreciacion,0) Af_Depreciacion_acum, af.Af_costo_compra - (isnull(af.Af_Depreciacion_acum,0) + ISNULL(DEP.Valor_Depreciacion,0)) AS Costo_actual,
		ISNULL(ult_depreciacion.Valor_Depreciacion,0) valor_ult_depreciacion, af.Af_fecha_compra
FROM            Af_Activo_fijo AS AF INNER JOIN
										 Af_Activo_fijo_Categoria AS cat ON AF.IdEmpresa = cat.IdEmpresa AND AF.IdCategoriaAF = cat.IdCategoriaAF AND 
										 AF.IdActivoFijoTipo = cat.IdActivoFijoTipo INNER JOIN
										 Af_Activo_fijo_tipo AS tipo ON cat.IdEmpresa = tipo.IdEmpresa AND cat.IdActivoFijoTipo = tipo.IdActivoFijoTipo INNER JOIN
										 tb_sucursal ON AF.IdEmpresa = tb_sucursal.IdEmpresa AND AF.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
											(SELECT        det.IdEmpresa, det.IdActivoFijo, sum(det.Valor_Depreciacion) Valor_Depreciacion
												FROM            Af_Depreciacion AS cab INNER JOIN
															Af_Depreciacion_Det AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdDepreciacion = det.IdDepreciacion
												WHERE cab.IdEmpresa = @IdEmpresa and cab.Fecha_Depreciacion <= @Fecha_corte							 
												group by det.IdEmpresa, det.IdActivoFijo
											) AS DEP ON 
										 AF.IdEmpresa = DEP.IdEmpresa AND AF.IdActivoFijo = DEP.IdActivoFijo

										 LEFT OUTER JOIN 
(select * from (
			select
			af.IdEmpresa,
			af.IdActivoFijo,			
			cab.IdDepreciacion,
			cab.Fecha_Depreciacion,
			det.Valor_Depreciacion,
			row_number() over(partition by af.IdEmpresa, af.IdActivoFijo order by cab.Fecha_Depreciacion desc) as rn		
			FROM  Af_Depreciacion AS cab INNER JOIN
			Af_Depreciacion_Det AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdDepreciacion = det.IdDepreciacion INNER JOIN
			Af_Activo_fijo AS af ON det.IdEmpresa = af.IdEmpresa AND det.IdActivoFijo = af.IdActivoFijo
			WHERE cab.IdEmpresa = @IdEmpresa and cab.Fecha_Depreciacion <= @Fecha_corte
			) t
where t.rn = 1) AS ult_depreciacion on ult_depreciacion.IdEmpresa = af.IdEmpresa and ult_depreciacion.IdActivoFijo = af.IdActivoFijo and af.Af_fecha_compra <= @Fecha_corte
WHERE af.IdEmpresa = @IdEmpresa 
				AND EXISTS(
				SELECT venta.IdEmpresa FROM Af_Venta_Activo venta
				where venta.IdEmpresa = af.IdEmpresa
				and venta.IdActivoFijo = af.IdActivoFijo
				and venta.Fecha_Venta >= @Fecha_corte
				)
				and af.IdActivoFijoTipo between @IdActivoFijoTipo_ini and @IdCategoria_fin
				and af.IdCategoriaAF between @IdCategoria_ini and @IdCategoria_fin
				and af.Estado_Proceso like '%'+@Estado_Proceso+'%'

insert into [web].[Af_SPACTF_004_detalle]

select AF.IdEmpresa, AF.IdActivoFijo, @IdUsuario ,tb_sucursal.IdSucursal, tb_sucursal.Su_Descripcion,  AF.CodActivoFijo, AF.Af_Nombre, tipo.IdActivoFijoTipo IdActijoFijoTipo, tipo.Af_Descripcion tipo_AF, cat.IdCategoriaAF, cat.Descripcion Categoria_AF,
		af.Af_costo_compra, isnull(af.Af_Depreciacion_acum,0) + ISNULL(DEP.Valor_Depreciacion,0) Af_Depreciacion_acum, af.Af_costo_compra - (isnull(af.Af_Depreciacion_acum,0) + ISNULL(DEP.Valor_Depreciacion,0)) AS Costo_actual,
		ISNULL(ult_depreciacion.Valor_Depreciacion,0) valor_ult_depreciacion, af.Af_fecha_compra
FROM            Af_Activo_fijo AS AF INNER JOIN
										 Af_Activo_fijo_Categoria AS cat ON AF.IdEmpresa = cat.IdEmpresa AND AF.IdCategoriaAF = cat.IdCategoriaAF AND 
										 AF.IdActivoFijoTipo = cat.IdActivoFijoTipo INNER JOIN
										 Af_Activo_fijo_tipo AS tipo ON cat.IdEmpresa = tipo.IdEmpresa AND cat.IdActivoFijoTipo = tipo.IdActivoFijoTipo INNER JOIN
										 tb_sucursal ON AF.IdEmpresa = tb_sucursal.IdEmpresa AND AF.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
											(SELECT        det.IdEmpresa, det.IdActivoFijo, sum(det.Valor_Depreciacion) Valor_Depreciacion
												FROM            Af_Depreciacion AS cab INNER JOIN
															Af_Depreciacion_Det AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdDepreciacion = det.IdDepreciacion
												WHERE cab.IdEmpresa = @IdEmpresa and cab.Fecha_Depreciacion <= @Fecha_corte							 
												group by det.IdEmpresa, det.IdActivoFijo
											) AS DEP ON 
										 AF.IdEmpresa = DEP.IdEmpresa AND AF.IdActivoFijo = DEP.IdActivoFijo

										 LEFT OUTER JOIN 
(select * from (
			select
			af.IdEmpresa,
			af.IdActivoFijo,			
			cab.IdDepreciacion,
			cab.Fecha_Depreciacion,
			det.Valor_Depreciacion,
			row_number() over(partition by af.IdEmpresa, af.IdActivoFijo order by cab.Fecha_Depreciacion desc) as rn		
			FROM  Af_Depreciacion AS cab INNER JOIN
			Af_Depreciacion_Det AS det ON cab.IdEmpresa = det.IdEmpresa AND cab.IdDepreciacion = det.IdDepreciacion INNER JOIN
			Af_Activo_fijo AS af ON det.IdEmpresa = af.IdEmpresa AND det.IdActivoFijo = af.IdActivoFijo
			WHERE cab.IdEmpresa = @IdEmpresa and cab.Fecha_Depreciacion <= @Fecha_corte
			) t
where t.rn = 1) AS ult_depreciacion on ult_depreciacion.IdEmpresa = af.IdEmpresa and ult_depreciacion.IdActivoFijo = af.IdActivoFijo and af.Af_fecha_compra <= @Fecha_corte
WHERE af.IdEmpresa = @IdEmpresa 
				AND EXISTS(
				SELECT venta.IdEmpresa FROM Af_Retiro_Activo venta
				where venta.IdEmpresa = af.IdEmpresa
				and venta.IdActivoFijo = af.IdActivoFijo
				and venta.Fecha_Retiro >= @Fecha_corte
				)
				and af.IdActivoFijoTipo between @IdActivoFijoTipo_ini and @IdCategoria_fin
				and af.IdCategoriaAF between @IdCategoria_ini and @IdCategoria_fin
				and af.Estado_Proceso like '%'+@Estado_Proceso+'%'

insert into [web].[Af_SPACTF_004_resumen]
SELECT        IdEmpresa, IdActivoFijoTipo, IdUsuario, tipo_AF, SUM(Af_costo_compra) AS Af_costo_compra, SUM(Af_Depreciacion_acum) AS Af_Depreciacion_acum, SUM(valor_ult_depreciacion) AS valor_ult_depreciacion, 
                         SUM(Costo_actual) AS Costo_actual
FROM            [web].[Af_SPACTF_004_detalle]
WHERE IdEmpresa = @IdEmpresa and IdUsuario = @IdUsuario
GROUP BY IdEmpresa, IdActivoFijoTipo, IdUsuario, tipo_AF


SELECT [IdEmpresa]
      ,[IdActivoFijoTipo]
      ,[IdUsuario]
      ,[Af_Descripcion]
      ,[Af_costo_compra]
      ,[Valor_Depreciacion]
      ,[Valor_ult_depreciacion]
      ,[Costo_neto]
  FROM [web].[Af_SPACTF_004_resumen]
  where IdEmpresa = @IdEmpresa and IdUsuario = @IdUsuario

  
END
