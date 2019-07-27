
 CREATE function [dbo].[base_impuesto_renta]
 (
  @IdEmpresa int,
  @IdNomina int,
  @IdNominaTipoLiqui int,
  @IdPeriodo int,
  @IdEmpleado int,
  @Anio int
 )
 returns float 
 as
 begin 
   declare 
   @base float,
   @ingresos float,
   @deducibles float,
   @FraccionBasica float,
   @Por_ImpFraccion_Exce float,
   @ImpFraccionBasica float,
   @ExcesoHasta float,
   @IESS float,
   @PorAportePers float



set @deducibles= isnull((
SELECT    CAST(  ISNULL(SUM(valor),0) as numeric(10,2))
FROM            dbo.ro_empleado_proyeccion_gastos_det AS gast_det INNER JOIN
                         dbo.ro_empleado_proyeccion_gastos AS gast ON gast_det.IdEmpresa = gast.IdEmpresa AND gast_det.IdTransaccion = gast.IdTransaccion
          where gast.IdEmpresa=@IdEmpresa
		  and gast.IdEmpleado=@IdEmpleado
		  and gast.AnioFiscal=@Anio
		  group by gast.IdEmpresa, gast.IdEmpleado, gast.AnioFiscal
		  ),0)


		  select @ingresos=ISNULL( SUM(Valor),0)
		  FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS ro_det ON rol.IdEmpresa = ro_det.IdEmpresa AND rol.IdRol = ro_det.IdRol INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON ro_det.IdEmpresa = rub.IdEmpresa AND ro_det.IdRubro = rub.IdRubro

		  where ro_det.IdEmpresa=@IdEmpresa
		  and ro_det.IdEmpleado=@IdEmpleado
		  and rol.IdPeriodo=@IdPeriodo
		  and rol.IdEmpresa=@IdEmpresa
		  and rol.IdNominaTipo=IdNominaTipo
		  and rol.IdNominaTipoLiqui=@IdNominaTipoLiqui
		  and rol.IdPeriodo=@IdPeriodo
		  and rub.rub_aplicaIR=1
		   and rub.ru_tipo='I'
		  group by ro_det.IdEmpresa, ro_det.IdEmpleado


		 select @IESS=ISNULL( SUM(Valor),0)
		 FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS ro_det ON rol.IdEmpresa = ro_det.IdEmpresa AND rol.IdRol = ro_det.IdRol
		  where ro_det.IdEmpresa=@IdEmpresa
		  and ro_det.IdEmpleado=@IdEmpleado
		  and rol.IdPeriodo=@IdPeriodo
		  and rol.IdEmpresa=@IdEmpresa
		  and rol.IdNominaTipo=IdNominaTipo
		  and rol.IdNominaTipoLiqui=@IdNominaTipoLiqui
		  and rol.IdPeriodo=@IdPeriodo
		  and ro_det.IdRubro=6
		  group by ro_det.IdEmpresa, ro_det.IdEmpleado




    select @PorAportePers= Porcentaje_aporte_pers from ro_Parametros where IdEmpresa=@IdEmpresa
    set @ingresos=@ingresos*12
	set @iess=@ingresos*@PorAportePers
    set @base=(@ingresos-@deducibles-@iess)

	select @ImpFraccionBasica=CAST( ImpFraccionBasica as numeric(10,2)), @Por_ImpFraccion_Exce=CAST( Por_ImpFraccion_Exce as numeric(10,2)), @FraccionBasica=CAST( FraccionBasica as numeric(10,2)),@ExcesoHasta=CAST( ExcesoHasta as numeric(10,2))  from
	 (
	select ROW_NUMBER() over(partition by FraccionBasica order by AnioFiscal) as IdRow,FraccionBasica, Por_ImpFraccion_Exce, ImpFraccionBasica, ExcesoHasta  from ro_tabla_Impu_Renta	
	 where AnioFiscal=@Anio	
	 ) 
	 a where 
	 @base >= a.FraccionBasica 
	 and @base <=a.ExcesoHasta
	-- and a.IdRow=1
	set @base=(((@base-@FraccionBasica)*( (@Por_ImpFraccion_Exce/100)))+@ImpFraccionBasica)/12
   return @base
   end