CREATE VIEW [dbo].[vwro_empleado_x_jornada]
AS
SELECT IdEmpresa, IdEmpleado, Empleado, em_status, IdNomina, IdSucursal, Pago_por_horas, Valor_horas_matutino, Valor_horas_vespertina, Valor_horas_brigada, Valor_hora_control_salida AS Valor_hora_control_salida, Valor_hora_adicionales, 
                  pe_cedulaRuc
FROM     dbo.vwro_empleado_combo
WHERE  (Pago_por_horas = 1)