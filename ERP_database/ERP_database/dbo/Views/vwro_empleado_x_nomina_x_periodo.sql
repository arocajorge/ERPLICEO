create view vwro_empleado_x_nomina_x_periodo as 
select ro_rol.IdEmpresa,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,IdEmpleado,IdPeriodo from ro_rol_detalle, ro_rol
group by ro_rol.IdEmpresa,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,IdEmpleado,IdPeriodo