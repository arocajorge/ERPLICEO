CREATE VIEW dbo.ro_periodo_x_ro_Nomina_TipoLiquiDisponibles
AS
SELECT        IdEmpresa, IdPeriodo, pe_anio, pe_mes, pe_FechaIni, pe_FechaFin, pe_estado, Fecha_Transac, Fecha_UltMod, IdUsuarioUltMod, FechaHoraAnul, IdUsuarioUltAnu, MotivoAnulacion, Cod_region, 
                         Carga_Todos_Empleados
FROM            dbo.ro_periodo AS ro
WHERE        (IdPeriodo NOT IN
                             (SELECT        IdPeriodo
                               FROM            dbo.ro_periodo_x_ro_Nomina_TipoLiqui
                               WHERE        (IdEmpresa = ro.IdEmpresa)))