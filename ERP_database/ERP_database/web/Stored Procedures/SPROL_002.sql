

CREATE  PROCEDURE [web].[SPROL_002]  
	@idempresa int,
	@idnomina_tipo int,
	@idnomina_Tipo_liq int,
	@idperiodo int
AS

BEGIN

--declare
--    @idempresa int,
--	@idnomina_tipo int,
--	@idnomina_Tipo_liq int,
--	@idperiodo int


--	set @idempresa =1
--	set @idnomina_tipo =1
--	set @idnomina_Tipo_liq =2
--	set @idperiodo =201901
	
	declare
	@IdRubroTotalPagar int,
	@IdRubroVerper varchar(50),
	@IdRubroMatutina varchar(50),
	@IdRubroDiasTrabajados varchar(50),
	@IdRubroBrigadas varchar(50)


	select @IdRubroVerper=IdRubro_horas_vespertina,@IdRubroMatutina=IdRubro_horas_matutina, @IdRubroBrigadas=IdRubro_horas_brigadas  from ro_rubros_calculados where IdEmpresa=@idempresa
	select @IdRubroTotalPagar=IdRubro_tot_pagar from ro_rubros_calculados where IdEmpresa=@idempresa
	select @IdRubroDiasTrabajados=IdRubro_dias_trabajados from ro_rubros_calculados where IdEmpresa=@idempresa
	delete web.ro_SPROL_002 where IdEmpresa=@idempresa --and IdPeriodo=@idperiodo






	  	  -- inserto dias trabajados 
	insert  into web.ro_SPROL_002(IdEmpresa,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,IdEmpleado,IdRubro,Valor,Idsucursal, IdArea,ru_descripcion)	
	select ro.IdEmpresa,ro.IdNominaTipo,ro.IdNominaTipoLiqui,ro.IdPeriodo,D.IdEmpleado,D.IdRubro,Valor , D.IdSucursal,emp.IdArea, r.ru_descripcion

	FROM            dbo.ro_rol_detalle AS D INNER JOIN
                         dbo.ro_empleado AS emp ON D.IdEmpresa = emp.IdEmpresa AND D.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_rol AS Ro ON D.IdEmpresa = Ro.IdEmpresa AND D.IdRol = Ro.IdRol INNER JOIN
                         dbo.ro_rubro_tipo AS R ON D.IdEmpresa = R.IdEmpresa AND D.IdRubro = R.IdRubro

	  where D.IdEmpresa=@idempresa
	  and IdNominaTipo=@idnomina_tipo
	  and IdNominaTipoLiqui=@idnomina_Tipo_liq
	  and IdPeriodo=@idperiodo
	  and R.IdEmpresa=D.IdEmpresa
	  and D.IdRubro=R.IdRubro
	  and ro.IdEmpresa=D.IdEmpresa
	  and ro.IdRol=D.IdRol
	  And R.ru_tipo='A'
	  and D.IdRubro=@IdRubroTotalPagar


--insertando egresos
	insert  into web.ro_SPROL_002(IdEmpresa,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,IdEmpleado,IdRubro,Valor ,Idsucursal, IdArea,ru_descripcion)	
	select ro.IdEmpresa,ro.IdNominaTipo,ro.IdNominaTipoLiqui,ro.IdPeriodo,D.IdEmpleado,D.IdRubro,Valor*-1 ,D.IdSucursal,emp.IdArea,R.ru_descripcion
	
FROM            dbo.ro_rol_detalle AS D INNER JOIN
                         dbo.ro_empleado AS emp ON D.IdEmpresa = emp.IdEmpresa AND D.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_rol AS Ro ON D.IdEmpresa = Ro.IdEmpresa AND D.IdRol = Ro.IdRol INNER JOIN
                         dbo.ro_rubro_tipo AS R ON D.IdEmpresa = R.IdEmpresa AND D.IdRubro = R.IdRubro

	  where D.IdEmpresa=@idempresa
	  and IdNominaTipo=@idnomina_tipo
	  and IdNominaTipoLiqui=@idnomina_Tipo_liq
	  and IdPeriodo=@idperiodo
	  and R.IdEmpresa=D.IdEmpresa
	  and D.IdRubro=R.IdRubro
	   and ro.IdEmpresa=D.IdEmpresa
	   and ro.IdRol=D.IdRol
	  And R.ru_tipo='E'
	  and D.Valor>0
	  -- insertando ingresos 
	  	insert  into web.ro_SPROL_002(IdEmpresa,IdNominaTipo,IdNominaTipoLiqui,IdPeriodo,IdEmpleado,IdRubro,Valor ,Idsucursal, IdArea,ru_descripcion)	
	select ro.IdEmpresa,ro.IdNominaTipo,ro.IdNominaTipoLiqui,ro.IdPeriodo,D.IdEmpleado,D.IdRubro,Valor ,D.IdSucursal,emp.IdArea,R.ru_descripcion
	
FROM            dbo.ro_rol_detalle AS D INNER JOIN
                         dbo.ro_empleado AS emp ON D.IdEmpresa = emp.IdEmpresa AND D.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_rol AS Ro ON D.IdEmpresa = Ro.IdEmpresa AND D.IdRol = Ro.IdRol INNER JOIN
                         dbo.ro_rubro_tipo AS R ON D.IdEmpresa = R.IdEmpresa AND D.IdRubro = R.IdRubro

	  where D.IdEmpresa=@idempresa
	  and IdNominaTipo=@idnomina_tipo
	  and IdNominaTipoLiqui=@idnomina_Tipo_liq
	  and IdPeriodo=@idperiodo
	  and R.IdEmpresa=D.IdEmpresa
	  and D.IdRubro=R.IdRubro
	   and ro.IdEmpresa=D.IdEmpresa
	   and ro.IdRol=D.IdRol
	  And R.ru_tipo='I'
	  and D.Valor>0

	  -- actualizando rubros vespertina matutina

update     web.ro_SPROL_002 set ru_descripcion  = ru_descripcion+  ' (' +CAST( convert(decimal(10, 2), ro_HorasProfesores_det.NumHoras) as varchar)+')' 
FROM            dbo.ro_HorasProfesores_det INNER JOIN
                         dbo.ro_HorasProfesores ON dbo.ro_HorasProfesores_det.IdEmpresa = dbo.ro_HorasProfesores.IdEmpresa AND dbo.ro_HorasProfesores_det.IdCarga = dbo.ro_HorasProfesores.IdCarga INNER JOIN
                         web.ro_SPROL_002 ON dbo.ro_HorasProfesores_det.IdEmpresa = web.ro_SPROL_002.IdEmpresa AND dbo.ro_HorasProfesores_det.IdRubro = web.ro_SPROL_002.IdRubro AND 
                         dbo.ro_HorasProfesores_det.IdEmpleado = web.ro_SPROL_002.IdEmpleado
			where ro_HorasProfesores_det.IdRubro in(@IdRubroMatutina,@IdRubroVerper,@IdRubroBrigadas)
			AND  ro_HorasProfesores_det.IdEmpresa=@idempresa
			and ro_HorasProfesores.IdNomina=@idnomina_tipo
			and ro_HorasProfesores.IdNominaTipo=@idnomina_Tipo_liq
			and ro_HorasProfesores.IdPeriodo=@idperiodo

  -- actualizando rubros horas extras

			update     web.ro_SPROL_002 set ru_descripcion  = ro_rol_detalle.Observacion 
FROM            dbo.ro_rol_detalle INNER JOIN
                         dbo.ro_rol ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_rol_detalle.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         web.ro_SPROL_002 ON dbo.ro_rol.IdEmpresa = web.ro_SPROL_002.IdEmpresa AND dbo.ro_rol.IdPeriodo = web.ro_SPROL_002.IdPeriodo AND dbo.ro_rol.IdNominaTipo = web.ro_SPROL_002.IdNominaTipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = web.ro_SPROL_002.IdNominaTipoLiqui AND dbo.ro_rol_detalle.IdEmpresa = web.ro_SPROL_002.IdEmpresa AND dbo.ro_rol_detalle.IdEmpleado = web.ro_SPROL_002.IdEmpleado AND 
                         dbo.ro_rol_detalle.IdRubro = web.ro_SPROL_002.IdRubro
			where ro_rol_detalle.IdRubro in(7,8,9)
			AND  ro_rol_detalle.IdEmpresa=@idempresa
			and ro_rol.IdNominaTipo=@idnomina_tipo
			and ro_rol.IdNominaTipoLiqui=@idnomina_Tipo_liq
			and ro_rol.IdPeriodo=@idperiodo
END
