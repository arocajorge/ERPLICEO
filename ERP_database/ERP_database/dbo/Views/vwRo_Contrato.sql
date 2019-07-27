
CREATE view [dbo].[vwRo_Contrato] as
SELECT        dbo.ro_contrato.IdEmpresa, dbo.ro_contrato.IdEmpleado, dbo.ro_contrato.IdContrato, dbo.ro_contrato.IdContrato_Tipo, dbo.ro_contrato.NumDocumento, 
                         dbo.ro_contrato.FechaInicio, dbo.ro_contrato.FechaFin, dbo.ro_contrato.Estado, dbo.ro_contrato.Observacion, dbo.ro_contrato.MotiAnula, 
                         dbo.ro_catalogo.ca_descripcion, dbo.tb_persona.pe_nombreCompleto, dbo.ro_contrato.EstadoContrato, dbo.ro_catalogo.IdTipoCatalogo, dbo.tb_persona.pe_apellido, 
                         dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_cedulaRuc, dbo.ro_empleado.em_codigo
FROM            dbo.ro_contrato INNER JOIN
                         dbo.ro_empleado ON dbo.ro_contrato.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_contrato.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_catalogo ON dbo.ro_contrato.IdContrato_Tipo = dbo.ro_catalogo.CodCatalogo
WHERE        (dbo.ro_catalogo.IdTipoCatalogo = 2)