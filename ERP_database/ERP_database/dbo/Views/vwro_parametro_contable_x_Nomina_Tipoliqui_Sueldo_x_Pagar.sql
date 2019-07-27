create view vwro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar as 

SELECT        dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdEmpresa, dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdNomina, 
                         dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdNominaTipo, dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdCtaCble, 
                         dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.Observacion, dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdTipoFlujo, dbo.ro_Nomina_Tipo.Descripcion, dbo.ct_plancta.pc_Cuenta
FROM            dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar INNER JOIN
                         dbo.ro_Nomina_Tipo ON dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND 
                         dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdNomina = dbo.ro_Nomina_Tipo.IdNomina_Tipo INNER JOIN
                         dbo.ct_plancta ON dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdEmpresa = dbo.ct_plancta.IdEmpresa AND 
                         dbo.ro_parametro_contable_x_Nomina_Tipoliqui_Sueldo_x_Pagar.IdCtaCble = dbo.ct_plancta.IdCtaCble