--EXEC spSys_in_consulta_errores_frecuentes 'admin', 0

CREATE PROCEDURE [dbo].[spSys_in_consulta_errores_frecuentes]
@IdUsuario varchar(20),
@Corregir bit
as
BEGIN

BEGIN --REGISTROS EN TABLA DE PRE-APROBACION QUE ESTAN APROBADOS Y DEBERIAN GENERAR MOVIMIENTO PERO NO LO HICIERON
SELECT 'REGISTROS EN TABLA DE PRE-APROBACION QUE ESTAN APROBADOS Y DEBERIAN GENERAR MOVIMIENTO PERO NO LO HICIERON'
SELECT        in_Ing_Egr_Inven_det.IdEmpresa,in_Ing_Egr_Inven_det.IdSucursal,in_Ing_Egr_Inven_det.IdBodega,in_Ing_Egr_Inven_det.IdMovi_inven_tipo, in_Ing_Egr_Inven_det.IdNumMovi,in_Ing_Egr_Inven_det.Secuencia,
			 in_Ing_Egr_Inven.IdEstadoAproba,
			in_Ing_Egr_Inven.cm_fecha
FROM            in_Ing_Egr_Inven_det INNER JOIN
                in_movi_inven_tipo ON in_Ing_Egr_Inven_det.IdEmpresa = in_movi_inven_tipo.IdEmpresa AND in_Ing_Egr_Inven_det.IdMovi_inven_tipo = in_movi_inven_tipo.IdMovi_inven_tipo INNER JOIN
                in_Ing_Egr_Inven ON in_Ing_Egr_Inven_det.IdEmpresa = in_Ing_Egr_Inven.IdEmpresa AND in_Ing_Egr_Inven_det.IdSucursal = in_Ing_Egr_Inven.IdSucursal AND 
                in_Ing_Egr_Inven_det.IdMovi_inven_tipo = in_Ing_Egr_Inven.IdMovi_inven_tipo AND in_Ing_Egr_Inven_det.IdNumMovi = in_Ing_Egr_Inven.IdNumMovi INNER JOIN
                in_Motivo_Inven ON in_Ing_Egr_Inven.IdEmpresa = in_Motivo_Inven.IdEmpresa AND in_Ing_Egr_Inven.IdMotivo_Inv = in_Motivo_Inven.IdMotivo_Inv
WHERE		in_Motivo_Inven.Genera_Movi_Inven = 'S'  and in_Ing_Egr_Inven_det.IdEmpresa_inv is null and in_Ing_Egr_Inven.Estado = 'A'
and in_Ing_Egr_Inven.IdEstadoAproba = 'APRO'

IF(@Corregir = 1 )
BEGIN
UPDATE in_Ing_Egr_Inven set IdEstadoAproba = 'PEND' from
(
SELECT        in_Ing_Egr_Inven_det.IdEmpresa,in_Ing_Egr_Inven_det.IdSucursal,in_Ing_Egr_Inven_det.IdBodega,in_Ing_Egr_Inven_det.IdMovi_inven_tipo, in_Ing_Egr_Inven_det.IdNumMovi,in_Ing_Egr_Inven_det.Secuencia,
			 in_Ing_Egr_Inven.IdEstadoAproba,
			in_Ing_Egr_Inven.cm_fecha
FROM            in_Ing_Egr_Inven_det INNER JOIN
                in_movi_inven_tipo ON in_Ing_Egr_Inven_det.IdEmpresa = in_movi_inven_tipo.IdEmpresa AND in_Ing_Egr_Inven_det.IdMovi_inven_tipo = in_movi_inven_tipo.IdMovi_inven_tipo INNER JOIN
                in_Ing_Egr_Inven ON in_Ing_Egr_Inven_det.IdEmpresa = in_Ing_Egr_Inven.IdEmpresa AND in_Ing_Egr_Inven_det.IdSucursal = in_Ing_Egr_Inven.IdSucursal AND 
                in_Ing_Egr_Inven_det.IdMovi_inven_tipo = in_Ing_Egr_Inven.IdMovi_inven_tipo AND in_Ing_Egr_Inven_det.IdNumMovi = in_Ing_Egr_Inven.IdNumMovi INNER JOIN
                in_Motivo_Inven ON in_Ing_Egr_Inven.IdEmpresa = in_Motivo_Inven.IdEmpresa AND in_Ing_Egr_Inven.IdMotivo_Inv = in_Motivo_Inven.IdMotivo_Inv
WHERE		in_Motivo_Inven.Genera_Movi_Inven = 'S'  and in_Ing_Egr_Inven_det.IdEmpresa_inv is null and in_Ing_Egr_Inven.Estado = 'A'
and in_Ing_Egr_Inven.IdEstadoAproba = 'APRO'
) A
WHERE in_Ing_Egr_Inven.IdEmpresa = A.IdEmpresa
AND in_Ing_Egr_Inven.IdSucursal = A.IdSucursal
AND in_Ing_Egr_Inven.IdMovi_inven_tipo = A.IdMovi_inven_tipo
AND in_Ing_Egr_Inven.IdNumMovi = A.IdNumMovi

END
END

BEGIN --REGISTROS APROBADOS QUE NO TIENEN RELACION CON TABLA PRE-APROBACION --in_movi_inve_detalle
		SELECT 'REGISTROS APROBADOS QUE NO TIENEN RELACION CON TABLA PRE-APROBACION --in_movi_inve_detalle'


		select cab.IdEmpresa, CAB.IdSucursal, CAB.IdBodega, cab.IdMovi_inven_tipo, cab.IdNumMovi,
				ct.IdEmpresa_ct, ct.IdTipoCbte_ct,ct.IdCbteCble_ct		
		 from in_movi_inve_detalle mov
		inner join in_movi_inve cab on
		cab.IdEmpresa = mov.IdEmpresa
		and cab.IdSucursal = mov.IdSucursal
		and cab.IdBodega = mov.IdBodega
		and cab.IdMovi_inven_tipo = mov.IdMovi_inven_tipo
		and cab.IdNumMovi = mov.IdNumMovi
		left join in_movi_inve_detalle_x_ct_cbtecble_det ct
		on mov.IdEmpresa = ct.IdEmpresa_inv
		and mov.IdSucursal = ct.IdSucursal_inv
		and mov.IdBodega = ct.IdBodega_inv
		and mov.IdMovi_inven_tipo = ct.IdMovi_inven_tipo_inv
		and mov.IdNumMovi = ct.IdNumMovi_inv
		and mov.Secuencia = ct.Secuencia_inv
		where not exists(
			select * from in_Ing_Egr_Inven_det DET 
			where DET.IdEmpresa_inv = mov.IdEmpresa
			and DET.IdSucursal_inv = mov.IdSucursal
			and DET.IdBodega_inv = mov.IdBodega
			and DET.IdMovi_inven_tipo_inv = mov.IdMovi_inven_tipo
			and DET.IdNumMovi_inv = mov.IdNumMovi
			and DET.secuencia_inv = mov.Secuencia
		)group by cab.IdEmpresa, CAB.IdSucursal, CAB.IdBodega, cab.IdMovi_inven_tipo, cab.IdNumMovi,
				ct.IdEmpresa_ct, ct.IdTipoCbte_ct,ct.IdCbteCble_ct

		IF(@Corregir = 1)
		BEGIN
			DELETE [dbo].[tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] WHERE IdUsuario = @IdUsuario

			INSERT INTO [dbo].[tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion]
			([IdEmpresa]
           ,[IdSucursal]
           ,[IdBodega]
           ,[IdMovi_inven_tipo]
           ,[IdNumMovi]
           ,[IdUsuario]
           ,[IdEmpresa_ct]
           ,[IdTipoCbte_ct]
           ,[IdCbteCble_ct])
			select cab.IdEmpresa, CAB.IdSucursal, CAB.IdBodega, cab.IdMovi_inven_tipo, cab.IdNumMovi,@IdUsuario,
					ct.IdEmpresa_ct, ct.IdTipoCbte_ct,ct.IdCbteCble_ct		
			 from in_movi_inve_detalle mov
			inner join in_movi_inve cab on
			cab.IdEmpresa = mov.IdEmpresa
			and cab.IdSucursal = mov.IdSucursal
			and cab.IdBodega = mov.IdBodega
			and cab.IdMovi_inven_tipo = mov.IdMovi_inven_tipo
			and cab.IdNumMovi = mov.IdNumMovi
			left join in_movi_inve_detalle_x_ct_cbtecble_det ct
			on mov.IdEmpresa = ct.IdEmpresa_inv
			and mov.IdSucursal = ct.IdSucursal_inv
			and mov.IdBodega = ct.IdBodega_inv
			and mov.IdMovi_inven_tipo = ct.IdMovi_inven_tipo_inv
			and mov.IdNumMovi = ct.IdNumMovi_inv
			and mov.Secuencia = ct.Secuencia_inv
			where not exists(
				select * from in_Ing_Egr_Inven_det DET 
				where DET.IdEmpresa_inv = mov.IdEmpresa
				and DET.IdSucursal_inv = mov.IdSucursal
				and DET.IdBodega_inv = mov.IdBodega
				and DET.IdMovi_inven_tipo_inv = mov.IdMovi_inven_tipo
				and DET.IdNumMovi_inv = mov.IdNumMovi
				and DET.secuencia_inv = mov.Secuencia
			)
			group by cab.IdEmpresa, CAB.IdSucursal, CAB.IdBodega, cab.IdMovi_inven_tipo, cab.IdNumMovi,
				ct.IdEmpresa_ct, ct.IdTipoCbte_ct,ct.IdCbteCble_ct

			BEGIN --TABLAS DE RELACION INVE - CONTA
				DELETE in_movi_inve_detalle_x_ct_cbtecble_det 
				WHERE EXISTS(
				SELECT eli.IdEmpresa FROM [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] eli
				WHERE in_movi_inve_detalle_x_ct_cbtecble_det.IdEmpresa_ct = ELI.IdEmpresa_ct
				and in_movi_inve_detalle_x_ct_cbtecble_det.IdTipoCbte_ct = eli.IdTipoCbte_ct
				and in_movi_inve_detalle_x_ct_cbtecble_det.IdCbteCble_ct = eli.IdCbteCble_ct
				and eli.IdUsuario = @IdUsuario
				)

				DELETE in_movi_inve_x_ct_cbteCble 
				WHERE EXISTS(
				SELECT eli.IdEmpresa FROM [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] eli
				WHERE in_movi_inve_x_ct_cbteCble.IdEmpresa_ct = ELI.IdEmpresa_ct
				and in_movi_inve_x_ct_cbteCble.IdTipoCbte = eli.IdTipoCbte_ct
				and in_movi_inve_x_ct_cbteCble.IdCbteCble = eli.IdCbteCble_ct
				and eli.IdUsuario = @IdUsuario
				)
			END

			BEGIN --INVENTARIO
				DELETE in_movi_inve_detalle
				WHERE not exists(				
				select * from in_Ing_Egr_Inven_det DET 
				where DET.IdEmpresa_inv = in_movi_inve_detalle.IdEmpresa
				and DET.IdSucursal_inv = in_movi_inve_detalle.IdSucursal
				and DET.IdBodega_inv = in_movi_inve_detalle.IdBodega
				and DET.IdMovi_inven_tipo_inv = in_movi_inve_detalle.IdMovi_inven_tipo
				and DET.IdNumMovi_inv = in_movi_inve_detalle.IdNumMovi
				and DET.secuencia_inv = in_movi_inve_detalle.Secuencia
				)

				delete in_movi_inve
		where exists(
		select * from in_movi_inve cab		
		where not exists(
			select * from in_movi_inve_detalle det
			where cab.IdEmpresa = det.IdEmpresa
			and cab.IdSucursal = det.IdSucursal
			and cab.IdBodega = det.IdBodega
			and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
			and cab.IdNumMovi = det.IdNumMovi
		) 
		and in_movi_inve.IdEmpresa = cab.IdEmpresa
		and in_movi_inve.IdSucursal = cab.IdSucursal
		and in_movi_inve.IdBodega = cab.IdBodega
		and in_movi_inve.IdMovi_inven_tipo = cab.IdMovi_inven_tipo
		and in_movi_inve.IdNumMovi = cab.IdNumMovi
		)
				
			END

			BEGIN --REVERSOS CONTABLES
			UPDATE [dbo].[tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion]
				   SET [IdEmpresa_anu] = A.IdEmpresa_Anu
					  ,[IdTipoCbte_anu] = A.IdTipoCbte_Anu
					  ,[IdCbteCble_anu] = A.IdCbteCble_Anu
					FROM(
				SELECT IdEmpresa,IdTipoCbte,IdCbteCble IdEmpresa_Anu, IdTipoCbte_Anu, IdCbteCble_Anu  
				FROM ct_cbtecble_Reversado					
					) A WHERE A.IdEmpresa = [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion].IdEmpresa_ct
					and a.IdTipoCbte = [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion].IdTipoCbte_ct
					and a.IdCbteCble_Anu = [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion].IdCbteCble_ct
					and [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion].IdUsuario = @IdUsuario

				delete ct_cbtecble_Reversado WHERE EXISTS(
				SELECT eli.IdEmpresa FROM [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] eli
				WHERE ct_cbtecble_Reversado.IdEmpresa = ELI.IdEmpresa_ct
				and ct_cbtecble_Reversado.IdTipoCbte = eli.IdTipoCbte_ct
				and ct_cbtecble_Reversado.IdCbteCble = eli.IdCbteCble_ct
				and eli.IdUsuario = @IdUsuario
				)
			delete ct_cbtecble_det WHERE EXISTS(
			SELECT eli.IdEmpresa FROM [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] eli
			WHERE ct_cbtecble_det.IdEmpresa = ELI.[IdEmpresa_anu]
			and ct_cbtecble_det.IdTipoCbte = eli.[IdTipoCbte_anu]
			and ct_cbtecble_det.IdCbteCble = eli.[IdCbteCble_anu]
			and eli.IdUsuario = @IdUsuario
			)
			delete ct_cbtecble WHERE EXISTS(
			SELECT eli.IdEmpresa FROM [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] eli
			WHERE ct_cbtecble.IdEmpresa = ELI.[IdEmpresa_anu]
			and ct_cbtecble.IdTipoCbte = eli.[IdTipoCbte_anu]
			and ct_cbtecble.IdCbteCble = eli.[IdCbteCble_anu]
			and eli.IdUsuario = @IdUsuario
			)
				
			END

			BEGIN --DIARIOS
			delete ct_cbtecble_det WHERE EXISTS(
			SELECT eli.IdEmpresa FROM [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] eli
			WHERE ct_cbtecble_det.IdEmpresa = ELI.IdEmpresa_ct
			and ct_cbtecble_det.IdTipoCbte = eli.IdTipoCbte_ct
			and ct_cbtecble_det.IdCbteCble = eli.IdCbteCble_ct
			and eli.IdUsuario = @IdUsuario
			)
			delete ct_cbtecble WHERE EXISTS(
			SELECT eli.IdEmpresa FROM [tb_spSys_in_consulta_errores_frecuentes_eliminar_movimientos_sin_relacion] eli
			WHERE ct_cbtecble.IdEmpresa = ELI.IdEmpresa_ct
			and ct_cbtecble.IdTipoCbte = eli.IdTipoCbte_ct
			and ct_cbtecble.IdCbteCble = eli.IdCbteCble_ct
			and eli.IdUsuario = @IdUsuario
			)
			END

			select 'CORRECCION', cab.IdEmpresa, CAB.IdSucursal, CAB.IdBodega, cab.IdMovi_inven_tipo, cab.IdNumMovi,
				ct.IdEmpresa_ct, ct.IdTipoCbte_ct,ct.IdCbteCble_ct		
		 from in_movi_inve_detalle mov
		inner join in_movi_inve cab on
		cab.IdEmpresa = mov.IdEmpresa
		and cab.IdSucursal = mov.IdSucursal
		and cab.IdBodega = mov.IdBodega
		and cab.IdMovi_inven_tipo = mov.IdMovi_inven_tipo
		and cab.IdNumMovi = mov.IdNumMovi
		left join in_movi_inve_detalle_x_ct_cbtecble_det ct
		on mov.IdEmpresa = ct.IdEmpresa_inv
		and mov.IdSucursal = ct.IdSucursal_inv
		and mov.IdBodega = ct.IdBodega_inv
		and mov.IdMovi_inven_tipo = ct.IdMovi_inven_tipo_inv
		and mov.IdNumMovi = ct.IdNumMovi_inv
		and mov.Secuencia = ct.Secuencia_inv
		where not exists(
			select * from in_Ing_Egr_Inven_det DET 
			where DET.IdEmpresa_inv = mov.IdEmpresa
			and DET.IdSucursal_inv = mov.IdSucursal
			and DET.IdBodega_inv = mov.IdBodega
			and DET.IdMovi_inven_tipo_inv = mov.IdMovi_inven_tipo
			and DET.IdNumMovi_inv = mov.IdNumMovi
			and DET.secuencia_inv = mov.Secuencia
		)
		END

END

BEGIN --CABECERAS SIN DETALLE --in_movi_inve
SELECT 'CABECERAS SIN DETALLE --in_movi_inve'
select cab.* from in_movi_inve cab
left join in_movi_inve_x_ct_cbteCble ct
on cab.IdEmpresa = ct.IdEmpresa
and cab.IdSucursal = ct.IdSucursal
and cab.IdBodega = ct.IdBodega
and cab.IdMovi_inven_tipo = ct.IdMovi_inven_tipo
and cab.IdNumMovi = ct.IdNumMovi
where not exists(
	select * from in_movi_inve_detalle det
	where cab.IdEmpresa = det.IdEmpresa
	and cab.IdSucursal = det.IdSucursal
	and cab.IdBodega = det.IdBodega
	and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
	and cab.IdNumMovi = det.IdNumMovi
) 

		IF(@Corregir = 1)
		BEGIN
		delete in_movi_inve
		where exists(
		select * from in_movi_inve cab		
		where not exists(
			select * from in_movi_inve_detalle det
			where cab.IdEmpresa = det.IdEmpresa
			and cab.IdSucursal = det.IdSucursal
			and cab.IdBodega = det.IdBodega
			and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
			and cab.IdNumMovi = det.IdNumMovi
		) 
		and in_movi_inve.IdEmpresa = cab.IdEmpresa
		and in_movi_inve.IdSucursal = cab.IdSucursal
		and in_movi_inve.IdBodega = cab.IdBodega
		and in_movi_inve.IdMovi_inven_tipo = cab.IdMovi_inven_tipo
		and in_movi_inve.IdNumMovi = cab.IdNumMovi
		)
		END
END

BEGIN --CABECERAS DE MOVI SIN DETALLE CON CABECERAS DE DIARIOS --in_movi_inve_x_ct_cbteCble
SELECT 'CABECERAS DE MOVI SIN DETALLE CON CABECERAS DE DIARIOS --in_movi_inve_x_ct_cbteCble'
		SELECT * FROM in_movi_inve_x_ct_cbteCble ct
		where exists(
				select * from in_movi_inve cab
				where not exists(
					select * from in_movi_inve_detalle det
					where cab.IdEmpresa = det.IdEmpresa
					and cab.IdSucursal = det.IdSucursal
					and cab.IdBodega = det.IdBodega
					and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
					and cab.IdNumMovi = det.IdNumMovi
				)and ct.IdEmpresa = cab.IdEmpresa
				and ct.IdSucursal = cab.IdSucursal
				and ct.IdBodega = cab.IdBodega
				and ct.IdMovi_inven_tipo = cab.IdMovi_inven_tipo
				and ct.IdNumMovi = cab.IdNumMovi		 
)
END

BEGIN --REGISTROS DE SOLO CABECERAS DE IN_MOVI_INVE QUE TIENEN RELACION CON DIARIOS
		SELECT 'REGISTROS DE SOLO CABECERAS DE IN_MOVI_INVE QUE TIENEN RELACION CON DIARIOS'
		SELECT * FROM in_movi_inve_detalle_x_ct_cbtecble_det ct
		where exists(
				select * from in_movi_inve cab
				where not exists(
					select * from in_movi_inve_detalle det
					where cab.IdEmpresa = det.IdEmpresa
					and cab.IdSucursal = det.IdSucursal
					and cab.IdBodega = det.IdBodega
					and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo
					and cab.IdNumMovi = det.IdNumMovi
				)and ct.IdEmpresa_inv = cab.IdEmpresa
				and ct.IdSucursal_inv = cab.IdSucursal
				and ct.IdBodega_inv = cab.IdBodega
				and ct.IdMovi_inven_tipo_inv = cab.IdMovi_inven_tipo
				and ct.IdNumMovi_inv = cab.IdNumMovi		 
		)
END

BEGIN --MOVIMIENTOS NO CONTABILIZADO
SELECT 'MOVIMIENTOS NO CONTABILIZADO'
SELECT        in_Ing_Egr_Inven_det.*
FROM            in_movi_inve_detalle INNER JOIN
                         in_movi_inve ON in_movi_inve_detalle.IdEmpresa = in_movi_inve.IdEmpresa AND in_movi_inve_detalle.IdSucursal = in_movi_inve.IdSucursal AND in_movi_inve_detalle.IdBodega = in_movi_inve.IdBodega AND
                          in_movi_inve_detalle.IdMovi_inven_tipo = in_movi_inve.IdMovi_inven_tipo AND in_movi_inve_detalle.IdNumMovi = in_movi_inve.IdNumMovi INNER JOIN
                         in_movi_inven_tipo ON in_movi_inve.IdEmpresa = in_movi_inven_tipo.IdEmpresa AND in_movi_inve.IdMovi_inven_tipo = in_movi_inven_tipo.IdMovi_inven_tipo
						 inner join in_Ing_Egr_Inven_det ON in_Ing_Egr_Inven_det.IdEmpresa_inv = in_movi_inve_detalle.IdEmpresa
						 and in_Ing_Egr_Inven_det.IdSucursal_inv = in_movi_inve_detalle.IdSucursal
						 and in_Ing_Egr_Inven_det.IdBodega_inv = in_movi_inve_detalle.IdBodega
						 and in_Ing_Egr_Inven_det.IdMovi_inven_tipo_inv = in_movi_inve_detalle.IdMovi_inven_tipo
						 and in_Ing_Egr_Inven_det.IdNumMovi_inv = in_movi_inve_detalle.IdNumMovi
						 and in_Ing_Egr_Inven_det.secuencia_inv = in_movi_inve_detalle.Secuencia
where in_movi_inven_tipo.Genera_Diario_Contable = 1 and not exists(
	select IdEmpresa_inv from in_movi_inve_detalle_x_ct_cbtecble_det ct
	where ct.IdEmpresa_inv = in_movi_inve_detalle.IdEmpresa
	and ct.IdSucursal_inv = in_movi_inve_detalle.IdSucursal
	and ct.IdBodega_inv = in_movi_inve_detalle.IdBodega
	and ct.IdMovi_inven_tipo_inv = in_movi_inve_detalle.IdMovi_inven_tipo
	and ct.IdNumMovi_inv = in_movi_inve_detalle.IdNumMovi
)
END

BEGIN --DIARIOS QUE NO TIENEN RELACION CON INVENTARIO PERO SI DEBERIAN -- in_movi_inve_detalle_x_ct_cbtecble_det
SELECT 'DIARIOS QUE NO TIENEN RELACION CON INVENTARIO PERO SI DEBERIAN -- in_movi_inve_detalle_x_ct_cbtecble_det'
SELECT * FROM ct_cbtecble_det det
WHERE EXISTS(
		SELECT tipo.IdEmpresa FROM in_movi_inven_tipo TIPO
		where det.IdEmpresa = tipo.IdEmpresa and det.IdTipoCbte = tipo.IdTipoCbte
) and not exists(
		select * from in_movi_inve_detalle_x_ct_cbtecble_det conta
		where conta.IdEmpresa_ct = det.IdEmpresa
		and conta.IdTipoCbte_ct = det.IdTipoCbte
		and conta.IdCbteCble_ct = det.IdCbteCble
		and conta.secuencia_ct = det.secuencia
		)
		AND det.dc_Observacion like 'Ing/Egr#%'
END

END