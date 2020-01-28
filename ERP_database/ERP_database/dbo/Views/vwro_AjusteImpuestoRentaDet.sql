create view vwro_AjusteImpuestoRentaDet as
SELECT dbo.ro_AjusteImpuestoRentaDet.IdEmpresa, dbo.ro_AjusteImpuestoRentaDet.IdAjuste, dbo.ro_AjusteImpuestoRentaDet.Secuencia, dbo.ro_AjusteImpuestoRentaDet.IdEmpleado, dbo.ro_AjusteImpuestoRentaDet.SueldoFechaCorte, 
                  dbo.ro_AjusteImpuestoRentaDet.SueldoProyectado, dbo.ro_AjusteImpuestoRentaDet.OtrosIngresos, dbo.ro_AjusteImpuestoRentaDet.IngresosLiquidos, dbo.ro_AjusteImpuestoRentaDet.GastosPersonales, 
                  dbo.ro_AjusteImpuestoRentaDet.AporteFechaCorte, dbo.ro_AjusteImpuestoRentaDet.BaseImponible, dbo.ro_AjusteImpuestoRentaDet.FraccionBasica, dbo.ro_AjusteImpuestoRentaDet.Excedente, 
                  dbo.ro_AjusteImpuestoRentaDet.ImpuestoFraccionBasica, dbo.ro_AjusteImpuestoRentaDet.ImpuestoRentaCausado, dbo.ro_AjusteImpuestoRentaDet.DescontadoFechaCorte, dbo.ro_AjusteImpuestoRentaDet.LiquidacionFinal, 
                  dbo.tb_persona.pe_nombreCompleto
FROM     dbo.ro_AjusteImpuestoRentaDet INNER JOIN
                  dbo.ro_empleado ON dbo.ro_AjusteImpuestoRentaDet.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_AjusteImpuestoRentaDet.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                  dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
GO



GO




