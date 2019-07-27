--exec web.SPACTF_005 1,1,9999,1,9999,'2018/05/01',''
CREATE PROCEDURE web.SPACTF_005
(
@IdEmpresa int,
@IdActivoFijoTipo_ini int,
@IdActivoFijoTipo_fin int,
@IdCategoriaAF_ini int,
@IdCategoriaAF_fin int,
@Fecha_corte datetime,
@Estado_proceso varchar(20)
)
AS
BEGIN

SELECT af.IdEmpresa, af.IdActivoFijo,af.IdActivoFijoTipo, ti.Af_Descripcion as nom_tipo, ca.Descripcion as nom_categoria,
af.IdCategoriaAF, af.CodActivoFijo, af.Af_Nombre, af.Estado_Proceso, est.Descripcion as nom_estado_proceso, af.Af_fecha_compra,
af.Af_costo_compra, isnull(sum(abs(mej.Valor_Mej_Baj_Activo)),0) valor_mejora, isnull(sum(abs(baj.Valor_Mej_Baj_Activo)),0) valor_baja,
isnull(af.Af_Depreciacion_acum,0) + isnull(sum(dep.Valor_Depreciacion),0) as valor_depreciacion, isnull(sum(ven.Valor_Venta),0) as valor_venta,
af.Af_costo_compra + isnull(sum(abs(mej.Valor_Mej_Baj_Activo)),0) - isnull(sum(abs(baj.Valor_Mej_Baj_Activo)),0) -
(isnull(af.Af_Depreciacion_acum,0) + isnull(sum(dep.Valor_Depreciacion),0)) -  isnull(sum(ven.Valor_Venta),0) saldo
FROM Af_Activo_fijo AS AF 
inner join Af_Catalogo est on est.IdCatalogo = af.Estado_Proceso
left join Af_Mej_Baj_Activo as mej on mej.IdEmpresa = af.IdEmpresa and mej.IdActivoFijo = af.IdActivoFijo and mej.Id_Tipo = 'Mejo_Acti' and mej.Fecha_MejBaj <= @Fecha_corte
left join Af_Mej_Baj_Activo as baj on baj.IdEmpresa = af.IdEmpresa and baj.IdActivoFijo = af.IdActivoFijo and baj.Id_Tipo = 'Baja_Acti' and baj.Fecha_MejBaj <= @Fecha_corte
left join Af_Venta_Activo as ven on ven.IdEmpresa = af.IdEmpresa and ven.IdActivoFijo = af.IdActivoFijo and ven.Fecha_Venta <= @Fecha_corte
inner join Af_Activo_fijo_tipo as ti on ti.IdEmpresa = af.IdEmpresa and ti.IdActivoFijoTipo = af.IdActivoFijoTipo
inner join Af_Activo_fijo_Categoria as ca on ca.IdEmpresa = af.IdEmpresa and ca.IdCategoriaAF = af.IdCategoriaAF
left join(
select det.IdEmpresa, det.IdActivoFijo, det.Valor_Depreciacion
from Af_Depreciacion_Det as det inner join Af_Depreciacion as cab
on cab.IdEmpresa = det.IdEmpresa and cab.IdDepreciacion = det.IdDepreciacion
inner join Af_Activo_fijo as a on a.IdEmpresa = det.IdEmpresa
and a.IdActivoFijo = det.IdActivoFijo
where cab.Fecha_Depreciacion <= @Fecha_corte
and det.IdEmpresa = @IdEmpresa and a.IdActivoFijoTipo between @IdActivoFijoTipo_ini and @IdActivoFijoTipo_fin
and a.IdCategoriaAF between @IdCategoriaAF_ini and @IdCategoriaAF_fin
and a.Estado_Proceso like '%'+@Estado_proceso+'%'
) as dep on af.IdEmpresa = dep.IdEmpresa and dep.IdActivoFijo = af.IdActivoFijo
where af.IdEmpresa = @IdEmpresa and af.IdActivoFijoTipo between @IdActivoFijoTipo_ini and @IdActivoFijoTipo_fin
and af.IdCategoriaAF between @IdCategoriaAF_ini and @IdCategoriaAF_fin
and af.Estado_Proceso like '%'+@Estado_proceso+'%' and af.Af_fecha_compra <= @Fecha_corte
group by af.IdEmpresa, af.IdActivoFijo,af.IdActivoFijoTipo, ti.Af_Descripcion, af.IdCategoriaAF, ca.Descripcion, 
af.CodActivoFijo, af.Af_Nombre, af.Estado_Proceso, est.Descripcion, af.Af_fecha_compra,
af.Af_costo_compra, af.Af_Depreciacion_acum 
END
