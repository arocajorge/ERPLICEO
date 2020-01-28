create view web.VWROL_029 as
SELECT dbo.ro_AjusteImpuestoRenta.IdEmpresa, dbo.ro_AjusteImpuestoRenta.IdAjuste, dbo.ro_AjusteImpuestoRenta.IdAnio, dbo.ro_AjusteImpuestoRenta.Fecha, dbo.ro_AjusteImpuestoRenta.FechaCorte, 
                  dbo.ro_AjusteImpuestoRenta.IdSucursal, dbo.ro_AjusteImpuestoRenta.Observacion, dbo.ro_AjusteImpuestoRenta.Estado, dbo.ro_AjusteImpuestoRentaDet.Secuencia, dbo.ro_AjusteImpuestoRentaDet.IdEmpleado, 
                  dbo.ro_AjusteImpuestoRentaDet.SueldoFechaCorte, dbo.ro_AjusteImpuestoRentaDet.SueldoProyectado, dbo.ro_AjusteImpuestoRentaDet.OtrosIngresos, dbo.ro_AjusteImpuestoRentaDet.IngresosLiquidos, 
                  dbo.ro_AjusteImpuestoRentaDet.GastosPersonales, dbo.ro_AjusteImpuestoRentaDet.AporteFechaCorte, dbo.ro_AjusteImpuestoRentaDet.BaseImponible, dbo.ro_AjusteImpuestoRentaDet.FraccionBasica, 
                  dbo.ro_AjusteImpuestoRentaDet.Excedente, dbo.ro_AjusteImpuestoRentaDet.ImpuestoFraccionBasica, dbo.ro_AjusteImpuestoRentaDet.ImpuestoRentaCausado, dbo.ro_AjusteImpuestoRentaDet.DescontadoFechaCorte, 
                  dbo.ro_AjusteImpuestoRentaDet.LiquidacionFinal, dbo.ro_empleado.IdPersona, dbo.tb_persona.pe_nombreCompleto
FROM     dbo.ro_AjusteImpuestoRenta INNER JOIN
                  dbo.ro_AjusteImpuestoRentaDet ON dbo.ro_AjusteImpuestoRenta.IdEmpresa = dbo.ro_AjusteImpuestoRentaDet.IdEmpresa AND dbo.ro_AjusteImpuestoRenta.IdAjuste = dbo.ro_AjusteImpuestoRentaDet.IdAjuste INNER JOIN
                  dbo.ro_empleado ON dbo.ro_AjusteImpuestoRentaDet.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_AjusteImpuestoRentaDet.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                  dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
GO



GO



GO


