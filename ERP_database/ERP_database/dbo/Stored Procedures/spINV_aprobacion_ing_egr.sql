--exec [dbo].[spINV_aprobacion_ing_egr] 2,1,1,6,54
CREATE PROCEDURE [dbo].[spINV_aprobacion_ing_egr]
(
@IdEmpresa int,
@IdSucursal int,
@IdBodega int,
@IdMovi_inven_tipo int,
@IdNumMovi numeric
)
AS
BEGIN

BEGIN --VARIABLES
PRINT 'VARIABLES'
DECLARE @IdNumMovi_apro numeric,
@Genera_Diario_Contable varchar(1),
@IdTipoCbte int,
@IdCbteCble numeric,
@signo varchar(1),
@fecha date
END

select @signo = signo ,
@fecha = CM_FECHA
from in_Ing_Egr_Inven 
where IdEmpresa = @IdEmpresa 
and IdSucursal = @IdSucursal  
and IdMovi_inven_tipo = @IdMovi_inven_tipo 
and IdNumMovi = @IdNumMovi

BEGIN --GET ID IN_MOVI_INVE
PRINT 'GET ID IN_MOVI_INVE'
select @IdNumMovi_apro = MAX(IdNumMovi)+1 from in_movi_inve 
where IdEmpresa = @IdEmpresa
AND IdSucursal = @IdSucursal
AND IdBodega = @IdBodega
AND IdMovi_inven_tipo = @IdMovi_inven_tipo

SET @IdNumMovi_apro = ISNULL(@IdNumMovi_apro,1)
END

BEGIN --GENERAR IN_MOVI_INVE
PRINT 'GENERAR IN_MOVI_INVE'
INSERT INTO [dbo].[in_movi_inve]           
([IdEmpresa]				,[IdSucursal]           ,[IdBodega]							,[IdMovi_inven_tipo]				,[IdNumMovi]
,[CodMoviInven]				,[cm_tipo]              ,[cm_observacion]					,[cm_fecha]										
,[Estado]					,[IdMotivo_Inv])
SELECT        
det.IdEmpresa				,det.IdSucursal			,det.IdBodega			,det.IdMovi_inven_tipo				,@IdNumMovi_apro
,cab.CodMoviInven			,cab.signo				,cab.cm_observacion		,cab.cm_fecha						
,'A'						,cab.IdMotivo_Inv
FROM            in_Ing_Egr_Inven AS cab INNER JOIN in_Ing_Egr_Inven_det AS det 
				ON cab.IdEmpresa = det.IdEmpresa AND cab.IdSucursal = det.IdSucursal 
				AND cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo AND cab.IdNumMovi = det.IdNumMovi
WHERE det.IdEmpresa = @IdEmpresa
and det.IdSucursal = @IdSucursal
and det.IdBodega = @IdBodega
and det.IdMovi_inven_tipo = @IdMovi_inven_tipo
and det.IdNumMovi = @IdNumMovi
and cab.Estado = 'A'
GROUP BY det.IdEmpresa				,det.IdSucursal			,det.IdBodega			,det.IdMovi_inven_tipo				
,cab.CodMoviInven			,cab.signo				,cab.cm_observacion		,cab.cm_fecha				
,cab.IdMotivo_Inv
END

BEGIN --GENERAR IN_MOVI_INVE_DETALLE
PRINT 'GENERAR IN_MOVI_INVE_DETALLE'
INSERT INTO [dbo].[in_movi_inve_detalle]
([IdEmpresa]              ,[IdSucursal]					,[IdBodega]						,[IdMovi_inven_tipo]           ,[IdNumMovi]           ,[Secuencia]
,[mv_tipo_movi]           ,[IdProducto]					,[dm_cantidad]					
,[dm_observacion]         ,[mv_costo]											
,[IdUnidadMedida]         ,[dm_cantidad_sinConversion]  ,[IdUnidadMedida_sinConversion] ,[mv_costo_sinConversion]  
    ,[IdMotivo_Inv]	            ,[Costeado])
SELECT        
det.IdEmpresa			  ,det.IdSucursal				,det.IdBodega					,det.IdMovi_inven_tipo			,@IdNumMovi_apro	 ,det.Secuencia
,cab.signo				  ,det.IdProducto				,isnull(C.dm_cantidad,det.dm_cantidad_sinConversion)				
,det.dm_observacion		  ,isnull(C.mv_costo,det.mv_costo_sinConversion)												
,ISNULL(C.IdUnidadMedida_Consumo,det.IdUnidadMedida)		  ,det.dm_cantidad_sinConversion,det.IdUnidadMedida_sinConversion,det.mv_costo_sinConversion	 ,det.IdMotivo_Inv				,1
FROM            in_Ing_Egr_Inven AS cab INNER JOIN in_Ing_Egr_Inven_det AS det 
				ON cab.IdEmpresa = det.IdEmpresa AND cab.IdSucursal = det.IdSucursal 
				AND cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo AND cab.IdNumMovi = det.IdNumMovi left join
				vwin_Ing_Egr_Inven_det_conversion as c on det.IdEmpresa = c.IdEmpresa and det.IdSucursal = c.IdSucursal
				and det.IdMovi_inven_tipo = c.IdMovi_inven_tipo AND C.IdNumMovi = DET.IdNumMovi AND C.Secuencia = DET.Secuencia
WHERE det.IdEmpresa = @IdEmpresa
and det.IdSucursal = @IdSucursal
and det.IdBodega = @IdBodega
and det.IdMovi_inven_tipo = @IdMovi_inven_tipo
and det.IdNumMovi = @IdNumMovi
END

BEGIN --ACTUALIZAR IN_ING_EGR CON PK DE IN_MOVI_INVE_DETALLE
PRINT 'ACTUALIZAR IN_ING_EGR CON PK DE IN_MOVI_INVE_DETALLE'
UPDATE in_Ing_Egr_Inven_det
set IdEmpresa_inv = A.IdEmpresa,
IdSucursal_inv = A.IdSucursal,
IdBodega_inv = A.IdBodega,
IdMovi_inven_tipo_inv = A.IdMovi_inven_tipo,
IdNumMovi_inv = A.IdNumMovi,
secuencia_inv = A.Secuencia,
IdUnidadMedida = a.IdUnidadMedida,
mv_costo = a.mv_costo,
dm_cantidad = a.dm_cantidad
FROM (
SELECT det.IdEmpresa, det.IdSucursal, det.IdBodega, det.IdMovi_inven_tipo, det.IdNumMovi, det.Secuencia, mv_costo, dm_cantidad, IdUnidadMedida
FROM in_movi_inve_detalle det
WHERE det.IdEmpresa = @IdEmpresa
and det.IdSucursal = @IdSucursal
and det.IdBodega = @IdBodega
and det.IdMovi_inven_tipo = @IdMovi_inven_tipo
and det.IdNumMovi = @IdNumMovi_apro
) A
WHERE in_Ing_Egr_Inven_det.IdEmpresa = @IdEmpresa
and in_Ing_Egr_Inven_det.IdSucursal = @IdSucursal
and in_Ing_Egr_Inven_det.IdBodega = @IdBodega
and in_Ing_Egr_Inven_det.IdMovi_inven_tipo = @IdMovi_inven_tipo
and in_Ing_Egr_Inven_det.IdNumMovi = @IdNumMovi
and in_Ing_Egr_Inven_det.Secuencia = A.Secuencia
END

BEGIN --ACTUALIZAR IN_ING_EGR CON PK DE IN_MOVI_INVE_DETALLE
PRINT 'ACTUALIZAR IN_ING_EGR CON PK DE IN_MOVI_INVE_DETALLE'
UPDATE in_Ing_Egr_Inven
set IdEstadoAproba = 'APRO',
FechaAR = GETDATE()
WHERE in_Ing_Egr_Inven.IdEmpresa = @IdEmpresa
and in_Ing_Egr_Inven.IdSucursal = @IdSucursal
and in_Ing_Egr_Inven.IdMovi_inven_tipo = @IdMovi_inven_tipo
and in_Ing_Egr_Inven.IdNumMovi = @IdNumMovi
END

BEGIN --SI ES INGRESO REGISTRO COSTO HISTORICO
IF(@signo = '+')
	BEGIN

		INSERT INTO [dbo].[in_producto_x_tb_bodega_Costo_Historico]
				([IdEmpresa]           ,[IdSucursal]           ,[IdBodega]           ,[IdProducto]			     ,[IdFecha]
				,[Secuencia]           ,[fecha]                ,[costo]              ,[Stock_a_la_fecha]          ,[Observacion]
				,[fecha_trans])

			select d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdProducto, 
			CAST(CAST(YEAR(C.cm_fecha)  AS VARCHAR(4))+RIGHT('00'+ CAST(MONTH(C.cm_fecha) AS VARCHAR(2)),2)+RIGHT('00'+ CAST(DAY(C.cm_fecha) AS VARCHAR(2)),2)AS INT),
			ROW_NUMBER() OVER(ORDER BY D.IdEmpresa) + ISNULL(SEC.SECUENCIA + 1,1), C.cm_fecha, s.CostoTotal/case when s.CantidadTotal = 0 then 1 else s.CantidadTotal end CostoPromedio,s.CantidadTotal,'',GETDATE()
			from in_movi_inve_detalle as d inner join 
			in_movi_inve as c on c.IdEmpresa = d.IdEmpresa and c.IdSucursal  = d.IdSucursal and c.IdBodega = d.IdBodega and c.IdMovi_inven_tipo = d.IdMovi_inven_tipo and c.IdNumMovi = d.IdNumMovi inner join
			in_Ing_Egr_Inven_det as dm on d.IdEmpresa = dm.IdEmpresa_inv and d.IdSucursal = dm.IdSucursal_inv and d.IdBodega = dm.IdBodega_inv and d.IdMovi_inven_tipo = dm.IdMovi_inven_tipo_inv and d.IdNumMovi = dm.IdNumMovi_inv and d.Secuencia = dm.secuencia_inv
			left join(
			select md.IdEmpresa, md.IdSucursal, md.IdBodega, md.IdProducto, ABS(sum(md.dm_cantidad)) CantidadTotal, ABS(sum(md.dm_cantidad * md.mv_costo)) CostoTotal
			from in_movi_inve_detalle as md
			group by md.IdEmpresa, md.IdSucursal, md.IdBodega, md.IdProducto
			) AS S on d.IdEmpresa = s.IdEmpresa and d.IdSucursal = s.IdSucursal and d.IdBodega = s.IdBodega and d.IdProducto = s.IdProducto LEFT JOIN (
			SELECT F.IdEmpresa, F.IdSucursal, F.IdBodega, F.IdProducto, F.IdFecha, MAX(F.Secuencia)Secuencia
			FROM in_producto_x_tb_bodega_Costo_Historico AS F
			WHERE F.IdEmpresa = @IdEmpresa AND F.IdSucursal = @IdSucursal AND F.IdBodega = @IdBodega AND IdFecha = CAST(CAST(YEAR(@Fecha)  AS VARCHAR(4))+RIGHT('00'+ CAST(MONTH(@Fecha) AS VARCHAR(2)),2)+RIGHT('00'+ CAST(DAY(@Fecha) AS VARCHAR(2)),2)AS INT)
			GROUP BY F.IdEmpresa, F.IdSucursal, F.IdBodega, F.IdProducto, F.IdFecha
			) SEC ON D.IdEmpresa = SEC.IdEmpresa AND D.IdSucursal = SEC.IdSucursal AND D.IdBodega = SEC.IdBodega AND D.IdProducto = SEC.IdProducto
			WHERE dm.IdEmpresa = @IdEmpresa and dm.IdSucursal = @IdSucursal AND dm.IdMovi_inven_tipo = @IdMovi_inven_tipo AND dm.IdNumMovi = @IdNumMovi
			group by d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdProducto, C.cm_fecha,s.CantidadTotal, s.CostoTotal, sec.Secuencia
	END
END

RETURN @IdNumMovi_apro
END