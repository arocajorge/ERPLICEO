create view web.VWROL_016 as 
SELECT        dbo.ro_Acta_Finiquito.IdEmpresa, dbo.ro_Acta_Finiquito.IdActaFiniquito, dbo.ro_Acta_Finiquito.IdEmpleado, dbo.ro_Acta_Finiquito.IdCausaTerminacion, dbo.ro_Acta_Finiquito.FechaIngreso, dbo.ro_Acta_Finiquito.FechaSalida, 
                         dbo.ro_Acta_Finiquito.UltimaRemuneracion, dbo.ro_Acta_Finiquito.Observacion, dbo.ro_Acta_Finiquito.Ingresos, dbo.ro_Acta_Finiquito.Egresos, dbo.ro_Acta_Finiquito.Ingresos + dbo.ro_Acta_Finiquito.Egresos AS Liquido, 
                         dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, dbo.ro_cargo.ca_descripcion, ro_catalogo_1.ca_descripcion AS Contrato, ro_catalogo_1.IdTipoCatalogo
FROM            dbo.ro_Acta_Finiquito INNER JOIN
                         dbo.ro_empleado ON dbo.ro_Acta_Finiquito.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_Acta_Finiquito.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_catalogo AS ro_catalogo_1 ON dbo.ro_Acta_Finiquito.IdContrato = ro_catalogo_1.IdCatalogo LEFT OUTER JOIN
                         dbo.ro_cargo ON dbo.ro_Acta_Finiquito.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_Acta_Finiquito.IdCargo = dbo.ro_cargo.IdCargo
						where ro_catalogo_1.IdTipoCatalogo=2