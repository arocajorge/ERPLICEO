
CREATE PROCEDURE [dbo].[spRo_LiquidarEmpleado]
	@IdEmpresa int,
	@IdActaFiniquito numeric(18)
	
AS

declare
@IdEmplado numeric(18,2)
select @IdEmplado=IdEmpleado from ro_Acta_Finiquito where IdEmpresa=@IdEmpresa and IdActaFiniquito=@IdActaFiniquito
BEGIN
update ro_empleado_novedad_det set EstadoCobro='CAN'
from ro_empleado_Novedad c, ro_empleado_novedad_det d

where c.IdEmpresa=d.IdEmpresa
and c.IdNovedad=d.IdNovedad

and c.IdEmpresa=@IdEmpresa and IdEmpleado=@IdEmplado 

 update ro_empleado set  em_status='EST_LIQ'
where IdEmpresa=@IdEmpresa and IdEmpleado=@IdEmplado 

update ro_prestamo_detalle  set  EstadoPago='CAN'
from ro_prestamo p, ro_prestamo_detalle pd
where  p.IdEmpresa=@IdEmpresa 
and p.IdEmpleado=@IdEmplado 
and p.IdEmpresa=pd.IdEmpresa
and p.IdPrestamo=pd.IdPrestamo


 update ro_rol_detalle_x_rubro_acumulado set  Estado='CAN'
where IdEmpresa=@IdEmpresa and IdEmpleado=@IdEmplado 


 update ro_contrato set  EstadoContrato='ECT_LIQ'
where IdEmpresa=@IdEmpresa and IdEmpleado=@IdEmplado 


END
