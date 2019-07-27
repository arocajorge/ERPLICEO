
create view ro_empleado_gatos_x_anio as 
SELECT        proye.IdEmpresa, proye.IdEmpleado, proye.AnioFiscal, SUM( pro_det.Valor)Valor
FROM            dbo.ro_empleado_proyeccion_gastos AS proye INNER JOIN
                         dbo.ro_empleado_proyeccion_gastos_det AS pro_det ON proye.IdEmpresa = pro_det.IdEmpresa AND proye.IdTransaccion = pro_det.IdTransaccion
GROUP BY proye.IdEmpresa, proye.IdTransaccion, proye.IdEmpleado, proye.AnioFiscal