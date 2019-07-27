CREATE view vwro_rubros_acumulados_x_empleados as
SELECT        Acumulados.IdEmpresa, Acumulados.IdEmpleado, Acumulados.IdRubro, Decimo_cuarto.ru_codRolGen AS RUBRO
FROM            dbo.ro_empleado_x_rubro_acumulado AS Acumulados INNER JOIN
                         dbo.ro_rubro_tipo AS Decimo_cuarto ON Acumulados.IdRubro = Decimo_cuarto.IdRubro AND Acumulados.IdEmpresa = Decimo_cuarto.IdEmpresa