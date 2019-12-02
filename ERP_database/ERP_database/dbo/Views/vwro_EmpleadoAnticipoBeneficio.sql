CREATE VIEW vwro_EmpleadoAnticipoBeneficio
AS
SELECT ro_EmpleadoAnticipoBeneficio.IdEmpresa, ro_EmpleadoAnticipoBeneficio.IdAnticipo, ro_EmpleadoAnticipoBeneficio.FechaDesde, ro_EmpleadoAnticipoBeneficio.FechaHasta, ro_EmpleadoAnticipoBeneficio.FechaCreacion, 
                  ro_EmpleadoAnticipoBeneficio.Valor, ro_rubro_tipo.ru_descripcion, tb_persona.pe_nombreCompleto
FROM     ro_empleado INNER JOIN
                  ro_EmpleadoAnticipoBeneficio ON ro_empleado.IdEmpresa = ro_EmpleadoAnticipoBeneficio.IdEmpresa AND ro_empleado.IdEmpleado = ro_EmpleadoAnticipoBeneficio.IdEmpleado INNER JOIN
                  ro_rubro_tipo ON ro_EmpleadoAnticipoBeneficio.IdEmpresa = ro_rubro_tipo.IdEmpresa AND ro_EmpleadoAnticipoBeneficio.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
                  tb_persona ON ro_empleado.IdPersona = tb_persona.IdPersona