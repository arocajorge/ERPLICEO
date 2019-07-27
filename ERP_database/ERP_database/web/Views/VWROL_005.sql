CREATE view web.VWROL_005 as
SELECT        dbo.ro_Acta_Finiquito.IdEmpresa, dbo.ro_Acta_Finiquito.IdActaFiniquito, dbo.ro_Acta_Finiquito.IdEmpleado, dbo.ro_Acta_Finiquito.IdCausaTerminacion, dbo.ro_Acta_Finiquito.IdContrato, dbo.ro_Acta_Finiquito.FechaIngreso, 
                         dbo.ro_Acta_Finiquito.FechaSalida, dbo.ro_Acta_Finiquito.UltimaRemuneracion, dbo.ro_Acta_Finiquito.Observacion, IIF(valor > 0, valor, 0.00) Ingresos, iif(valor < 1, valor, 0.00) Egresos, 
                         dbo.ro_Acta_Finiquito.EsMujerEmbarazada, dbo.ro_Acta_Finiquito.EsDirigenteSindical, dbo.ro_Acta_Finiquito.EsPorDiscapacidad, dbo.ro_Acta_Finiquito.EsPorEnfermedadNoProfesional, 
                         dbo.ro_Acta_Finiquito_Detalle.IdSecuencia, dbo.ro_Acta_Finiquito_Detalle.Observacion AS DescripcionDetalle, dbo.ro_Acta_Finiquito_Detalle.Valor, dbo.ro_Acta_Finiquito.IdCargo, dbo.ro_contrato.NumDocumento, 
                         dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.ro_cargo.ca_descripcion , dbo.ro_rubro_tipo.ru_descripcion, pe_apellido+' '+pe_nombre as NombreCompleto
,

dbo.ro_catalogo.ca_descripcion TipoTerminacion, dbo.tb_sucursal.Su_Descripcion
FROM            dbo.ro_Acta_Finiquito INNER JOIN
                         dbo.ro_Acta_Finiquito_Detalle ON dbo.ro_Acta_Finiquito.IdEmpresa = dbo.ro_Acta_Finiquito_Detalle.IdEmpresa AND dbo.ro_Acta_Finiquito.IdActaFiniquito = dbo.ro_Acta_Finiquito_Detalle.IdActaFiniquito AND 
                         dbo.ro_Acta_Finiquito.IdActaFiniquito = dbo.ro_Acta_Finiquito_Detalle.IdActaFiniquito INNER JOIN
                         dbo.ro_contrato ON dbo.ro_Acta_Finiquito.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_Acta_Finiquito.IdEmpleado = dbo.ro_contrato.IdEmpleado AND 
                         dbo.ro_Acta_Finiquito.IdContrato = dbo.ro_contrato.IdContrato INNER JOIN
                         dbo.ro_empleado ON dbo.ro_Acta_Finiquito.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_Acta_Finiquito.IdEmpleado = dbo.ro_empleado.IdEmpleado AND dbo.ro_contrato.IdEmpresa = dbo.ro_empleado.IdEmpresa AND 
                         dbo.ro_contrato.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_Acta_Finiquito_Detalle.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_Acta_Finiquito_Detalle.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                         dbo.ro_catalogo ON dbo.ro_Acta_Finiquito.IdCausaTerminacion = dbo.ro_catalogo.CodCatalogo INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_empleado.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_empleado.IdSucursal = dbo.tb_sucursal.IdSucursal