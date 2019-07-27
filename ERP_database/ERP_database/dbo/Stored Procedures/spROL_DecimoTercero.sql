
CREATE PROCEDURE [dbo].[spROL_DecimoTercero]
       (
       @IdEmpresa int,      
       @IdPeriodo int,
       @Region varchar(10),
       @IdUsuario varchar(50),
       @observacion varchar(200),
       @IdRol int,
	   @IdSucursalInicio int,
	   @IdSucursalFin int,
	   @IdNomina int
       )
       as
BEGIN


--DECLARE
--       @IdEmpresa int,
--       @IdPeriodo int,
--       @Region varchar(10),
--       @IdUsuario varchar(50),
--       @observacion varchar(200),
--	   @IdRol int,
--	   @IdNomina int,
--	   @IdSucursalInicio int,
--       @IdSucursalFin int
--       set @IdEmpresa =1
--       set @IdPeriodo =2019
--       set @Region ='COSTA'
--       set @IdUsuario ='admin'
--       set @observacion= '...'
--	   set @IdRol=0
--	   SET @IdSucursalInicio=0
--	   set @IdSucursalFin=1
--	   set @IdNomina=1





declare
@IdRubro_calculado varchar(50),
@IdRubro_Provision varchar(50),
@Fi date,
@Ff date


if(@Region='COSTA')
select @fi=convert(varchar(4), (@IdPeriodo-1))+'-'+'12'+'-'+'01'
select @ff=convert(varchar(4), (@IdPeriodo))+'-'+'11'+'-'+'30' 

SET @IdPeriodo=concat( @IdPeriodo,'12','11' ) 
if((select  COUNT(IdPeriodo) from ro_periodo where IdEmpresa=@IdEmpresa and IdPeriodo=@IdPeriodo)=0)
insert into ro_periodo (
IdEmpresa,                        IdPeriodo,                 pe_FechaIni,                             pe_FechaFin,                             pe_estado                  ,Fecha_Transac
)
values
(@IdEmpresa,					  @IdPeriodo,     @Fi,                                        @Ff,                                            'A',                       GETDATE())



if((select  COUNT(IdPeriodo) from ro_periodo_x_ro_Nomina_TipoLiqui where IdEmpresa=@IdEmpresa and IdNomina_Tipo=@IdNomina and IdNomina_TipoLiqui=3 and IdPeriodo=@IdPeriodo)=0)
insert into ro_periodo_x_ro_Nomina_TipoLiqui(
IdEmpresa,                        IdNomina_Tipo,                    IdNomina_TipoLiqui,                             IdPeriodo,                               Cerrado                    ,Procesado,                     Contabilizado
)
values
(@IdEmpresa,               @IdNomina,                                       3,                                               @IdPeriodo,                      'N',          'S',                           'N')




if((select  COUNT(IdPeriodo) from ro_rol where IdEmpresa=@IdEmpresa and IdRol=@IdRol and IdNominaTipo=@IdNomina and IdNominaTipoLiqui=3)=0)
insert into ro_rol
(IdEmpresa,   IdRol, IdSucursal, IdNominaTipo,        IdNominaTipoLiqui,         IdPeriodo,                  Descripcion,                      Observacion,                      Cerrado,                    FechaIngresa,
UsuarioIngresa,      FechaModifica,             UsuarioModifica,           FechaAnula,                 UsuarioAnula,                     MotivoAnula,                      UsuarioCierre,              FechaCierre,
IdCentroCosto)
values
(@IdEmpresa   , @IdRol ,case when @IdSucursalInicio=0then NULL else @IdSucursalInicio end     ,@IdNomina                                ,3       ,@IdPeriodo                ,@observacion                     ,@observacion                     ,'ABIERTO'                       ,GETDATE()
,@IdUsuario          ,null                      ,null                             ,null                       ,null                                    ,null                                    ,null                           ,null
,null)




delete ro_rol_detalle where IdEmpresa=@IdEmpresa and IdRol= @IdRol 

----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando decimo cuarto sueldo-------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_DIII, @IdRubro_Provision=IdRubro_prov_DIII from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,                       IdRol,                                             IdEmpleado,                IdRubro,                   Orden,               Valor
,rub_visible_reporte,      Observacion,IdSucursal)


select


@IdEmpresa,                       @IdRol,                                                                            emp.IdEmpleado,             @IdRubro_calculado, '1',                ROUND( SUM(acum.Valor),2),
1,                                       'Decimo tercer sueldo', acum.IdSucursal

FROM            dbo.ro_rol_detalle_x_rubro_acumulado AS acum INNER JOIN
                         dbo.ro_empleado AS emp ON acum.IdEmpresa = emp.IdEmpresa AND acum.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rol ON acum.IdEmpresa = dbo.ro_rol.IdEmpresa AND acum.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON dbo.ro_rol.IdEmpresa = pe_x_nom.IdEmpresa AND dbo.ro_rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo
and acum.IdEmpleado=emp.IdEmpleado    
and acum.Estado='PEN'
AND emp.em_status='EST_ACT'
AND acum.IdRubro=@IdRubro_Provision
AND acum.IdSucursal>=@IdSucursalInicio
AND acum.IdSucursal<=@IdSucursalFin
AND acum.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and period.pe_FechaIni between @Fi and @Ff
and period.pe_FechaFin between @Fi and @Ff
and cont.IdNomina=@IdNomina
group by emp.IdEmpleado , acum.IdSucursal, emp.IdEmpresa






select @IdRubro_calculado= IdRubro_tot_pagar, @IdRubro_Provision=IdRubro_prov_DIII from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,                       IdRol,                                             IdEmpleado,                IdRubro,                   Orden,               Valor
,rub_visible_reporte,      Observacion,IdSucursal)


select


@IdEmpresa,                       @IdRol,                                                                            emp.IdEmpleado,             @IdRubro_calculado, '1',                ROUND( SUM(acum.Valor),2),
1,                                       'Liqudo a recibir', acum.IdSucursal

FROM            dbo.ro_rol_detalle_x_rubro_acumulado AS acum INNER JOIN
                         dbo.ro_empleado AS emp ON acum.IdEmpresa = emp.IdEmpresa AND acum.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_rol ON acum.IdEmpresa = dbo.ro_rol.IdEmpresa AND acum.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON dbo.ro_rol.IdEmpresa = pe_x_nom.IdEmpresa AND dbo.ro_rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo
and acum.IdEmpleado=emp.IdEmpleado    
and acum.Estado='PEN'
AND emp.em_status='EST_ACT'
AND acum.IdRubro=@IdRubro_Provision
AND acum.IdSucursal>=@IdSucursalInicio
AND acum.IdSucursal<=@IdSucursalFin
AND acum.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and period.pe_FechaIni between @Fi and @Ff
and period.pe_FechaFin between @Fi and @Ff
and cont.IdNomina=@IdNomina
group by emp.IdEmpleado , acum.IdSucursal, emp.IdEmpresa
       
 end


