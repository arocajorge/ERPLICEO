 CREATE function [dbo].[calcular_horas_nocturnas]
 (
	@idempresa int,
	@idnomina_tipo int,
	@idnomina_Tipo_liq int,
	@idperiodo int,
	-- campos especificos
	@idrubro int,
	@idempleado int
 )
 -- returns float 
 returns varchar(10)
 as

 begin 

   declare @ho varchar(10)
   declare @horas varchar(10)
   Select @ho= isnull(novd.CantidadHoras,0) from ro_rol as r (nolock)
--inner join ro_empleado_Novedad nov on nov.IdEmpresa=novd.IdEmpresa and nov.IdNovedad=novd.IdNovedad
Inner join ro_rol_detalle rd(nolock) on r.IdEmpresa=rd.IdEmpresa and r.IdRol=rd.IdRol
Inner join ro_rol_x_empleado_novedades novemp(nolock) on novemp.IdEmpresa=rd.IdEmpresa and novemp.IdRol=rd.IdRol
Inner join ro_empleado_Novedad nov(nolock) on novemp.IdEmpresa=nov.IdEmpresa and nov.IdNovedad=novemp.IdNovedad and nov.IdEmpleado=rd.IdEmpleado
Inner join ro_empleado_novedad_det novd(nolock) on nov.IdEmpresa=novd.IdEmpresa and novd.IdNovedad=nov.IdNovedad AND novd.IdRubro =rd.IdRubro
where r.IdEmpresa=@idempresa
 and r.IdNominaTipo=@idnomina_tipo and r.IdNominaTipoLiqui=@idnomina_Tipo_liq and r.IdPeriodo=@idperiodo AND rd.Valor>0 -- and r.IdRol=74
 and novd.IdRubro=@idrubro and nov.IdEmpleado=@idempleado

 Set @horas=CAST( convert(varchar(10), isnull(@ho,'')) as varchar)

   return isnull(@horas,'-')
 end;

 /*

 select dbo.calcular_horas_nocturnas(2,1,2,201908,119,25)

 */