CREATE PROCEDURE [EntidadRegulatoria].[GenerarRDEP]
	@IdEmpresa int,
	@IdErdp int,
	@IdPeriodo int,
	@IdNomina int,
	@IdSucursalInicio int,
	@IdSucursalFin int,
  @IdEmpleadoInicio numeric,
	@IdEmpleadoFin numeric, 
	@Observacion varchar(1000),
	@IdUsuario varchar(50)
AS

BEGIN
/*
exec [EntidadRegulatoria].[GenerarRDEP]
  @IdEmpresa =1,
	  @IdErdp=1,
	  @IdPeriodo =2019,
	  @IdNomina =1,
	  @IdSucursalInicio =1,
	  @IdSucursalFin =9999,
	  @IdEmpleadoInicio=1,
	  @IdEmpleadoFin=99999999,
	  @Observacion='',
	  @IdUsuario=''
	  select *from EntidadRegulatoria.ro_rdep_det where idempleado = 40
	*/
	/*
	
	declare
	@IdEmpresa int,
	@IdErdp int,
	@IdPeriodo int,
	@IdNomina int,
	@IdSucursalInicio int,
	@IdSucursalFin int,
	@IdEmpleadoInicio numeric,
    @IdEmpleadoFin numeric, 
    @Observacion varchar(1000),
    @IdUsuario varchar(50)


    set @IdEmpresa =1
	set @IdErdp=1
	set @IdPeriodo =2019
	set @IdNomina =1
	set @IdSucursalInicio =1
	set @IdSucursalFin =9999
	set @IdEmpleadoInicio=1
	set @IdEmpleadoFin=9999999999
	set @Observacion=''
	set @IdUsuario=''
	 */
	declare 
	@FechaInicio date = datefrompartS(@IdPeriodo,1,1),
	@FechaFin date= datefrompartS(@IdPeriodo,12,31),
	@PorAporteIndividual float

	select @PorAporteIndividual = Porcentaje_aporte_pers from ro_Parametros where IdEmpresa = @IdEmpresa

	
	DELETE EntidadRegulatoria.ro_rdep_det  where EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep_det.Id_Rdep>=@IdErdp
	
if((select  COUNT(Id_Rdep) from EntidadRegulatoria.ro_rdep where IdEmpresa=@IdEmpresa and Id_Rdep=@IdErdp)>0)
update EntidadRegulatoria.ro_rdep set Observacion=@Observacion, IdUsuarioUltMod=@IdUsuario, Fecha_UltMod=GETDATE() where IdEmpresa=@IdEmpresa and Id_Rdep=@IdErdp
else
insert into EntidadRegulatoria.ro_rdep
(IdEmpresa,			Id_Rdep,		IdSucursal,				pe_anio,		IdNomina_Tipo,		Su_CodigoEstablecimiento,			Observacion,					
Estado,				IdUsuario,	Fecha_Transac			
)
select
 @IdEmpresa,	  @IdErdp,		case when @IdSucursalInicio=0then NULL else @IdSucursalInicio end			,@IdPEriodo,	@IdNomina,   '001'		,@observacion
,1,				  @IdUsuario, GETDATE()



	insert into EntidadRegulatoria.ro_rdep_det
	(IdEmpresa,IdEmpleado,  pe_nombre,pe_apellido, pe_cedulaRuc ,Secuencia, Id_Rdep)
	
     SELECT  ro_empleado.IdEmpresa,ro_empleado.IdEmpleado,    tb_persona.pe_nombre, tb_persona.pe_apellido, tb_persona.pe_cedulaRuc, 
	  ROW_NUMBER() OVER(ORDER BY ro_empleado.idempresa ASC) AS Secuencia, @IdErdp
FROM            dbo.ro_empleado INNER JOIN
                         dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona AND dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona

						 WHERE ro_empleado.IdEmpresa=@IdEmpresa
						 and ro_contrato.IdNomina=@IdNomina
						 and ro_empleado.em_estado='A'
						 and case when isnull(ro_empleado.em_fechaSalida,@fechainicio) between @FechaInicio and @FechaFin then 'EST_ACT' else ro_empleado.em_status end = 'EST_ACT'
						 and ro_empleado.IdEmpleado between @IdEmpleadoInicio and @IdEmpleadoFin
						 and ro_empleado.em_fechaIngaRol <= @FechaFin
						 AND ro_empleado.IdSucursal BETWEEN @IdSucursalInicio AND @IdSucursalFin
						 group by ro_empleado.IdEmpresa,ro_empleado.IdEmpleado,    tb_persona.pe_nombre, tb_persona.pe_apellido, tb_persona.pe_cedulaRuc

						 -- ACTUALIZANDO INGRESOS  QUE NO SON BENEFICIOS SOCIALES
  update EntidadRegulatoria.ro_rdep_det set EntidadRegulatoria.ro_rdep_det.Sueldo= b.Valor
   from 
  (
 SELECT        rol_det.IdEmpresa, rol_det.IdSucursal, rol.IdNominaTipo, rol_det.IdEmpleado, @IdPeriodo AS IdPEriodo, ROUND(SUM(rol_det.Valor), 2) AS Valor, EntidadRegulatoria.ro_rdep_det.Id_Rdep
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rubro_tipo AS ru_tipo ON rol_det.IdEmpresa = ru_tipo.IdEmpresa AND rol_det.IdRubro = ru_tipo.IdRubro INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo AND pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						 where rol_det.IdRubro not in(ro_rubros_calculados.IdRubro_DIII,ro_rubros_calculados.IdRubro_DIV,ro_rubros_calculados.IdRubro_fondo_reserva,ro_rubros_calculados.IdRubro_prov_DIV)
						 and EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep.IdSucursal>=@IdSucursalInicio
						 and EntidadRegulatoria.ro_rdep.IdSucursal<=@IdSucursalFin
						  and EntidadRegulatoria.ro_rdep.pe_anio>=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.IdNomina_Tipo=@IdNomina
						 and emp.IdEmpleado>=@IdEmpleadoInicio
						 and emp.IdEmpleado<=@IdEmpleadoFin
						 and rol.IdNominaTipoLiqui=2						
						 and YEAR( period.pe_FechaFin)=@IdPeriodo
						 and ru_tipo.ru_tipo='I'
						 and ru_tipo.rub_aplica_IESS = 1
GROUP BY rol_det.IdEmpresa, rol_det.IdEmpleado, rol_det.IdSucursal, rol.IdNominaTipo,EntidadRegulatoria.ro_rdep_det.Id_Rdep
)b  inner join
EntidadRegulatoria.ro_rdep INNER JOIN
 EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep
on b.IdEmpresa=EntidadRegulatoria.ro_rdep.IdEmpresa
and  b.IdSucursal=EntidadRegulatoria.ro_rdep.IdSucursal
and  b.IdNominaTipo=EntidadRegulatoria.ro_rdep.IdNomina_Tipo
and  b.IdPeriodo=EntidadRegulatoria.ro_rdep.pe_anio
and b.Id_Rdep=EntidadRegulatoria.ro_rdep.Id_Rdep
and b.IdEmpleado=EntidadRegulatoria.ro_rdep_det.IdEmpleado
and EntidadRegulatoria.ro_rdep_det.IdEmpresa = @IdEmpresa
and EntidadRegulatoria.ro_rdep_det.Id_Rdep = @IdErdp

				 -- ACTUALIZANDO INGRESOS  VARIOS
  update EntidadRegulatoria.ro_rdep_det set EntidadRegulatoria.ro_rdep_det.IngresoVarios= b.Valor
   from 
  (
 SELECT        rol_det.IdEmpresa, rol_det.IdSucursal, rol.IdNominaTipo, rol_det.IdEmpleado, @IdPeriodo AS IdPEriodo, SUM(rol_det.Valor) AS Valor, ro_rdep_det.Id_Rdep
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rubro_tipo AS ru_tipo ON rol_det.IdEmpresa = ru_tipo.IdEmpresa AND rol_det.IdRubro = ru_tipo.IdRubro INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo AND pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						 and rol_det.IdRubro not in(ro_rubros_calculados.IdRubro_DIII,ro_rubros_calculados.IdRubro_DIV,ro_rubros_calculados.IdRubro_fondo_reserva)
						 WHERE EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep.IdSucursal>=@IdSucursalInicio
						 and EntidadRegulatoria.ro_rdep.IdSucursal<=@IdSucursalFin
						  and EntidadRegulatoria.ro_rdep.pe_anio>=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.IdNomina_Tipo=@IdNomina
						 and emp.IdEmpleado>=@IdEmpleadoInicio
						 and emp.IdEmpleado<=@IdEmpleadoFin
						 and rol.IdNominaTipoLiqui=6
						
						 and YEAR( period.pe_FechaFin)=@IdPeriodo
						 and ru_tipo.ru_tipo='I'
GROUP BY rol_det.IdEmpresa, rol_det.IdEmpleado, rol_det.IdSucursal, rol.IdNominaTipo,EntidadRegulatoria.ro_rdep_det.Id_Rdep
)b  inner join
EntidadRegulatoria.ro_rdep INNER JOIN
 EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep
on b.IdEmpresa=EntidadRegulatoria.ro_rdep.IdEmpresa
and  b.IdSucursal=EntidadRegulatoria.ro_rdep.IdSucursal
and  b.IdNominaTipo=EntidadRegulatoria.ro_rdep.IdNomina_Tipo
and  b.IdPeriodo=EntidadRegulatoria.ro_rdep.pe_anio
and b.Id_Rdep=EntidadRegulatoria.ro_rdep.Id_Rdep
and b.IdEmpleado=EntidadRegulatoria.ro_rdep_det.IdEmpleado
and EntidadRegulatoria.ro_rdep_det.IdEmpresa = @IdEmpresa
and EntidadRegulatoria.ro_rdep_det.Id_Rdep = @IdErdp



-- ACTUALIZANDO fondo de reserva
  update EntidadRegulatoria.ro_rdep_det set EntidadRegulatoria.ro_rdep_det.FondosReserva= dbo.BankersRounding(Sueldo * 0.0833,2)
  where IdEmpresa = @IdEmpresa
  and Id_Rdep = @IdErdp
  /*
  b.Valor
   from 
  (
  SELECT A.IdEmpresa, A.IdSucursal, A.IdNominaTipo, A.IdEmpleado, A.IdPEriodo, ROUND (SUM(A.Valor),2) Valor, A.Id_Rdep FROM (
  select rol_det.IdEmpresa, rol_det.IdSucursal, rol.IdNominaTipo, rol_det.IdEmpleado, @IdPeriodo IdPEriodo, ROUND (SUM(rol_det.Valor),2) Valor, ro_rdep_det.Id_Rdep
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rubro_tipo AS ru_tipo ON rol_det.IdEmpresa = ru_tipo.IdEmpresa AND rol_det.IdRubro = ru_tipo.IdRubro INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo AND pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND rol_det.IdRubro  = (ro_rubros_calculados.IdRubro_fondo_reserva) INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						 and EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep.IdSucursal>=@IdSucursalInicio
						 and EntidadRegulatoria.ro_rdep.IdSucursal<=@IdSucursalFin
						  and EntidadRegulatoria.ro_rdep.pe_anio>=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.IdNomina_Tipo<=@IdNomina
						 and emp.IdEmpleado>=@IdEmpleadoInicio
						 and emp.IdEmpleado<=@IdEmpleadoFin
						
						 and YEAR( period.pe_FechaFin)=@IdPeriodo
						 and ru_tipo.ru_tipo='I'
GROUP BY rol_det.IdEmpresa, rol_det.IdEmpleado, rol_det.IdSucursal, rol.IdNominaTipo,EntidadRegulatoria.ro_rdep_det.Id_Rdep
UNION ALL
  select rol_det.IdEmpresa, rol_det.IdSucursal, rol.IdNominaTipo, rol_det.IdEmpleado, @IdPeriodo IdPEriodo, ROUND (SUM(rol_det.Valor),2) Valor, ro_rdep_det.Id_Rdep
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle_x_rubro_acumulado AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rubro_tipo AS ru_tipo ON rol_det.IdEmpresa = ru_tipo.IdEmpresa AND rol_det.IdRubro = ru_tipo.IdRubro INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo AND pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND rol_det.IdRubro  = (ro_rubros_calculados.IdRubro_prov_FR) INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						 and EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep.IdSucursal>=@IdSucursalInicio
						 and EntidadRegulatoria.ro_rdep.IdSucursal<=@IdSucursalFin
						  and EntidadRegulatoria.ro_rdep.pe_anio>=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.IdNomina_Tipo<=@IdNomina
						 and emp.IdEmpleado>=@IdEmpleadoInicio
						 and emp.IdEmpleado<=@IdEmpleadoFin
						
						 and YEAR( period.pe_FechaFin)=@IdPeriodo
						 --and ru_tipo.ru_tipo='I'
GROUP BY rol_det.IdEmpresa, rol_det.IdEmpleado, rol_det.IdSucursal, rol.IdNominaTipo,EntidadRegulatoria.ro_rdep_det.Id_Rdep
) A
GROUP BY A.IdEmpresa, A.IdEmpleado, A.IdSucursal, A.IdNominaTipo,A.Id_Rdep,A.IdPEriodo
)b  inner join
EntidadRegulatoria.ro_rdep INNER JOIN
 EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep
on b.IdEmpresa=EntidadRegulatoria.ro_rdep.IdEmpresa
and  b.IdSucursal=EntidadRegulatoria.ro_rdep.IdSucursal
and  b.IdNominaTipo=EntidadRegulatoria.ro_rdep.IdNomina_Tipo
and  b.IdPeriodo=EntidadRegulatoria.ro_rdep.pe_anio
and b.Id_Rdep=EntidadRegulatoria.ro_rdep.Id_Rdep
and b.IdEmpleado=EntidadRegulatoria.ro_rdep_det.IdEmpleado
*/


-- ACTUALIZANDO DECIMO CUARTO SUELDO
  update EntidadRegulatoria.ro_rdep_det set EntidadRegulatoria.ro_rdep_det.DecimoCuartoSueldo= b.Valor
   from 
  (
  select a.IdEmpresa, a.IdSucursal, a.IdNominaTipo, a.IdEmpleado, a.IdPEriodo, sum(a.Valor) Valor, a.Id_Rdep
  from(
  select rol_det.IdEmpresa, rol_det.IdSucursal, rol.IdNominaTipo, rol_det.IdEmpleado, @IdPeriodo IdPEriodo, dbo.BankersRounding(SUM(rol_det.Valor),2) Valor, ro_rdep_det.Id_Rdep
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rubro_tipo AS ru_tipo ON rol_det.IdEmpresa = ru_tipo.IdEmpresa AND rol_det.IdRubro = ru_tipo.IdRubro INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo AND pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND rol_det.IdRubro  = (ro_rubros_calculados.IdRubro_DIV) INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						 and EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep.IdSucursal>=@IdSucursalInicio
						 and EntidadRegulatoria.ro_rdep.IdSucursal<=@IdSucursalFin
						  and EntidadRegulatoria.ro_rdep.pe_anio=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.IdNomina_Tipo=@IdNomina
						 AND rol.IdNominaTipoLiqui != 4
						 and emp.IdEmpleado>=@IdEmpleadoInicio
						 and emp.IdEmpleado<=@IdEmpleadoFin
						
						 and YEAR( period.pe_FechaFin)=@IdPeriodo
						 and ru_tipo.ru_tipo='I'
GROUP BY rol_det.IdEmpresa, rol_det.IdEmpleado, rol_det.IdSucursal, rol.IdNominaTipo,EntidadRegulatoria.ro_rdep_det.Id_Rdep
union all
  select rol_det.IdEmpresa, rol_det.IdSucursal, rol.IdNominaTipo, rol_det.IdEmpleado, @IdPeriodo IdPEriodo, dbo.BankersRounding(SUM(rol_det.Valor),2) Valor, ro_rdep_det.Id_Rdep
FROM            ro_rol AS rol INNER JOIN
                         ro_rol_detalle_x_rubro_acumulado AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         ro_rubro_tipo AS ru_tipo ON rol_det.IdEmpresa = ru_tipo.IdEmpresa AND rol_det.IdRubro = ru_tipo.IdRubro INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo AND pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         ro_rubros_calculados ON rol_det.IdRubro = ro_rubros_calculados.IdRubro_prov_DIV AND rol_det.IdEmpresa = ro_rubros_calculados.IdEmpresa INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						 where EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep.IdSucursal>=@IdSucursalInicio
						 and EntidadRegulatoria.ro_rdep.IdSucursal<=@IdSucursalFin
						  and EntidadRegulatoria.ro_rdep.pe_anio=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.IdNomina_Tipo=@IdNomina
						 and emp.IdEmpleado>=@IdEmpleadoInicio
						 and emp.IdEmpleado<=@IdEmpleadoFin
						 and YEAR( period.pe_FechaFin)=@IdPeriodo
GROUP BY rol_det.IdEmpresa, rol_det.IdEmpleado, rol_det.IdSucursal, rol.IdNominaTipo,EntidadRegulatoria.ro_rdep_det.Id_Rdep
) a
group by a.IdEmpresa, a.IdSucursal, a.IdNominaTipo, a.IdEmpleado, a.IdPEriodo, a.Id_Rdep
)b  inner join
EntidadRegulatoria.ro_rdep INNER JOIN
 EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep
on b.IdEmpresa=EntidadRegulatoria.ro_rdep.IdEmpresa
and  b.IdSucursal=EntidadRegulatoria.ro_rdep.IdSucursal
and  b.IdNominaTipo=EntidadRegulatoria.ro_rdep.IdNomina_Tipo
and  b.IdPeriodo=EntidadRegulatoria.ro_rdep.pe_anio
and b.Id_Rdep=EntidadRegulatoria.ro_rdep.Id_Rdep
and b.IdEmpleado=EntidadRegulatoria.ro_rdep_det.IdEmpleado
and EntidadRegulatoria.ro_rdep_det.IdEmpresa = @IdEmpresa
and EntidadRegulatoria.ro_rdep_det.Id_Rdep = @IdErdp


-- ACTUALIZANDO DECIMO TERCERO SUELDO
  update EntidadRegulatoria.ro_rdep_det set EntidadRegulatoria.ro_rdep_det.DecimoTercerSueldo= dbo.BankersRounding(sueldo/12,2)
  where IdEmpresa = @IdEmpresa
  and Id_Rdep = @IdErdp

-- ACTUALIZANDO IESS PERSONAL
  update EntidadRegulatoria.ro_rdep_det set EntidadRegulatoria.ro_rdep_det.AportePErsonal= dbo.BankersRounding(Sueldo * @PorAporteIndividual,2)
  where IdEmpresa = @IdEmpresa
  and Id_Rdep = @IdErdp
  
  /* b.Valor
   from 
  (
  select rol_det.IdEmpresa, rol_det.IdSucursal, rol.IdNominaTipo, rol_det.IdEmpleado, @IdPeriodo IdPEriodo, ROUND (SUM(rol_det.Valor),2) Valor, ro_rdep_det.Id_Rdep
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rubro_tipo AS ru_tipo ON rol_det.IdEmpresa = ru_tipo.IdEmpresa AND rol_det.IdRubro = ru_tipo.IdRubro INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo AND pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND rol_det.IdRubro  = (ro_rubros_calculados.IdRubro_iess_perso) INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						 and EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep.IdSucursal>=@IdSucursalInicio
						 and EntidadRegulatoria.ro_rdep.IdSucursal<=@IdSucursalFin
						  and EntidadRegulatoria.ro_rdep.pe_anio>=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.IdNomina_Tipo<=@IdNomina
						 and emp.IdEmpleado>=@IdEmpleadoInicio
						 and emp.IdEmpleado<=@IdEmpleadoFin
						
						 and YEAR( period.pe_FechaFin)=@IdPeriodo
						 and ru_tipo.ru_tipo='E'
GROUP BY rol_det.IdEmpresa, rol_det.IdEmpleado, rol_det.IdSucursal, rol.IdNominaTipo,EntidadRegulatoria.ro_rdep_det.Id_Rdep
)b  inner join
EntidadRegulatoria.ro_rdep INNER JOIN
 EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep
on b.IdEmpresa=EntidadRegulatoria.ro_rdep.IdEmpresa
and  b.IdSucursal=EntidadRegulatoria.ro_rdep.IdSucursal
and  b.IdNominaTipo=EntidadRegulatoria.ro_rdep.IdNomina_Tipo
and  b.IdPeriodo=EntidadRegulatoria.ro_rdep.pe_anio
and b.Id_Rdep=EntidadRegulatoria.ro_rdep.Id_Rdep
and b.IdEmpleado=EntidadRegulatoria.ro_rdep_det.IdEmpleado
*/


--  ACTUALIZANDO gastos personales

  update EntidadRegulatoria.ro_rdep_det set EntidadRegulatoria.ro_rdep_det.GastoVivienda= b.VIVIENDA,  
  EntidadRegulatoria.ro_rdep_det.GastoSalud=b.SALUD,
   EntidadRegulatoria.ro_rdep_det.GastoAlimentacion=b.ALIMENTACION, 
   EntidadRegulatoria.ro_rdep_det.GastoVestimenta=b.VESTIMENTA, 
   EntidadRegulatoria.ro_rdep_det.GastoEucacion=b.EDUCACION
   from 
  (
   SELECT IdEmpresa,IdEmpleado,IdSucursal,IdNomina,AnioFiscal ,ISNULL( [ALI],0)ALIMENTACION,ISNULL([EDU],0)EDUCACION,ISNULL([EMPREN],0)EMPRENDIMINTO,ISNULL([SA],0)SALUD,ISNULL([VES],0)VESTIMENTA,ISNULL([VIV],0)VIVIENDA
FROM (
    SELECT ro_empleado_proyeccion_gastos.IdEmpresa, ro_empleado_proyeccion_gastos.IdEmpleado,dbo.ro_empleado_proyeccion_gastos_det.IdTipoGasto,dbo.ro_empleado_proyeccion_gastos_det.Valor,
	ro_empleado.IdSucursal,ro_contrato.IdNomina, ro_empleado_proyeccion_gastos.AnioFiscal
   FROM            dbo.ro_empleado INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON dbo.ro_empleado.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND dbo.ro_empleado.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         dbo.ro_empleado.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND dbo.ro_empleado.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         dbo.ro_empleado_proyeccion_gastos_det INNER JOIN
                         dbo.ro_empleado_proyeccion_gastos ON dbo.ro_empleado_proyeccion_gastos_det.IdEmpresa = dbo.ro_empleado_proyeccion_gastos.IdEmpresa AND 
                         dbo.ro_empleado_proyeccion_gastos_det.IdTransaccion = dbo.ro_empleado_proyeccion_gastos.IdTransaccion ON dbo.ro_empleado.IdEmpresa = dbo.ro_empleado_proyeccion_gastos.IdEmpresa AND 
                         dbo.ro_empleado.IdEmpleado = dbo.ro_empleado_proyeccion_gastos.IdEmpleado INNER JOIN
                         dbo.ro_tipo_gastos_personales ON dbo.ro_empleado_proyeccion_gastos_det.IdTipoGasto = dbo.ro_tipo_gastos_personales.IdTipoGasto INNER JOIN
                         dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						  WHERE ro_empleado_proyeccion_gastos.IdEmpresa=@IdEmpresa
						 and ro_empleado_proyeccion_gastos.AnioFiscal=@IdPeriodo
						 and ro_contrato.IdNomina=@IdNomina
						 and ro_empleado.IdSucursal>=@IdSucursalInicio
						 and ro_empleado.IdSucursal<=@IdSucursalFin
						 and ro_empleado.IdEmpleado>=@IdEmpleadoInicio
						 and ro_empleado.IdEmpleado<=@IdEmpleadoFin
						 ) as s
PIVOT
(
   max([VALOR])
    FOR [IdTipoGasto] IN ([ALI],[EDU],[EMPREN],[SA],[VES],[VIV])
)AS pvt
)b  inner join
 EntidadRegulatoria.ro_rdep INNER JOIN
 EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep
on b.IdEmpresa=EntidadRegulatoria.ro_rdep_det.IdEmpresa
and  b.IdEmpleado=EntidadRegulatoria.ro_rdep_det.IdEmpleado
and  b.IdSucursal=EntidadRegulatoria.ro_rdep.IdSucursal
and  b.IdNomina=EntidadRegulatoria.ro_rdep.IdNomina_Tipo
and  b.AnioFiscal=EntidadRegulatoria.ro_rdep.pe_anio
and EntidadRegulatoria.ro_rdep_det.IdEmpresa = @IdEmpresa
and EntidadRegulatoria.ro_rdep_det.Id_Rdep = @IdErdp

-- ACTUALIZANDO ValorImpuestoRetenidoTrabajador
  update EntidadRegulatoria.ro_rdep_det set EntidadRegulatoria.ro_rdep_det.ValorImpuestoRetenidoTrabajador= b.Valor
   from 
  (
  select rol_det.IdEmpresa, rol_det.IdSucursal, rol.IdNominaTipo, rol_det.IdEmpleado, @IdPeriodo IdPEriodo, ROUND (SUM(rol_det.Valor),2) Valor, ro_rdep_det.Id_Rdep
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rubro_tipo AS ru_tipo ON rol_det.IdEmpresa = ru_tipo.IdEmpresa AND rol_det.IdRubro = ru_tipo.IdRubro INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado AND 
                         emp.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND emp.IdEmpleado = EntidadRegulatoria.ro_rdep_det.IdEmpleado INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo AND pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON ru_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND rol_det.IdRubro  = (ro_rubros_calculados.IdRubro_IR) INNER JOIN
                         EntidadRegulatoria.ro_rdep ON EntidadRegulatoria.ro_rdep_det.IdEmpresa = EntidadRegulatoria.ro_rdep.IdEmpresa AND EntidadRegulatoria.ro_rdep_det.Id_Rdep = EntidadRegulatoria.ro_rdep.Id_Rdep
						 where EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa
						 and EntidadRegulatoria.ro_rdep.IdSucursal>=@IdSucursalInicio
						 and EntidadRegulatoria.ro_rdep.IdSucursal<=@IdSucursalFin
						  and EntidadRegulatoria.ro_rdep.pe_anio>=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.IdNomina_Tipo<=@IdNomina
						 and emp.IdEmpleado>=@IdEmpleadoInicio
						 and emp.IdEmpleado<=@IdEmpleadoFin
						
						 and YEAR( period.pe_FechaFin)=@IdPeriodo
						 --and ru_tipo.ru_tipo='I'
GROUP BY rol_det.IdEmpresa, rol_det.IdEmpleado, rol_det.IdSucursal, rol.IdNominaTipo,EntidadRegulatoria.ro_rdep_det.Id_Rdep
)b  inner join
EntidadRegulatoria.ro_rdep INNER JOIN
 EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep
on b.IdEmpresa=EntidadRegulatoria.ro_rdep.IdEmpresa
and  b.IdSucursal=EntidadRegulatoria.ro_rdep.IdSucursal
and  b.IdNominaTipo=EntidadRegulatoria.ro_rdep.IdNomina_Tipo
and  b.IdPeriodo=EntidadRegulatoria.ro_rdep.pe_anio
and b.Id_Rdep=EntidadRegulatoria.ro_rdep.Id_Rdep
and b.IdEmpleado=EntidadRegulatoria.ro_rdep_det.IdEmpleado
and EntidadRegulatoria.ro_rdep_det.IdEmpresa = @IdEmpresa
and EntidadRegulatoria.ro_rdep_det.Id_Rdep = @IdErdp

--  ACTUALIZANDO BASE IMPONIBLE

UPDATE EntidadRegulatoria.ro_rdep_det set BaseImponibleGravada=(ISNULL( Sueldo,0)+isnull(IngresoVarios,0)+isnull(IngresoPorOtrosEmpleaodres,0))-(isnull(AportePErsonal,0)+isnull(IessPorOtrosEmpleadores,0)+isnull(GastoVivienda,0)+isnull(GastoAlimentacion,0)+isnull(GastoVestimenta,0)+isnull(GastoEucacion,0)+isnull(GastoSalud,0)+isnull(ExoneraionPorDiscapacidad,0)+isnull(ExoneracionPorTerceraEdad,0))+(isnull(ImpuestoRentaCausado,0))
where EntidadRegulatoria.ro_rdep_det.Id_Rdep=@IdErdp and EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa

-- actualizando impuesto renta causado
update EntidadRegulatoria.ro_rdep_det set ImpuestoRentaCausado=
--faccion basica+((excedente/100)*(base imponible - exceso hasta))
(
	ro_tabla_Impu_Renta.ImpFraccionBasica+
	(
		(EntidadRegulatoria.ro_rdep_det.BaseImponibleGravada-ro_tabla_Impu_Renta.FraccionBasica)*
		(ro_tabla_Impu_Renta.Por_ImpFraccion_Exce/100)
	)
)
FROM            EntidadRegulatoria.ro_rdep INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep INNER JOIN
                         dbo.ro_tabla_Impu_Renta ON EntidadRegulatoria.ro_rdep.pe_anio = dbo.ro_tabla_Impu_Renta.AnioFiscal
						 and ro_tabla_Impu_Renta.AnioFiscal=@IdPeriodo
						 and EntidadRegulatoria.ro_rdep.Id_Rdep=@IdErdp
						 and EntidadRegulatoria.ro_rdep.IdEmpresa=@IdEmpresa

						 where  EntidadRegulatoria.ro_rdep_det.BaseImponibleGravada >= ro_tabla_Impu_Renta.FraccionBasica 
							and EntidadRegulatoria.ro_rdep_det.BaseImponibleGravada <=ro_tabla_Impu_Renta.ExcesoHasta




--  ACTUALIZANDO INGRESOS GRAVADOS POR ESTE EMPLEADOR

UPDATE EntidadRegulatoria.ro_rdep_det set IngresosGravadorPorEsteEmpleador=(ISNULL( Sueldo,0)+isnull(IngresoVarios,0)+isnull(Utilidades,0) + isnull(ImpuestoRentaAsumidoPorEsteEmpleador,0))
where EntidadRegulatoria.ro_rdep_det.Id_Rdep=@IdErdp and EntidadRegulatoria.ro_rdep_det.IdEmpresa=@IdEmpresa


end