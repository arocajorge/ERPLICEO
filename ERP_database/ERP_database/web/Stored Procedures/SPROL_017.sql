
--  exec [web].[SPROL_017] 1,'2018-01-01','2018-12-12'


CREATE procedure [web].[SPROL_017]

(
@IdEmpresa int,
@FechaInicio date,
@FechaFin date
)

as
begin

select listaMarc.IdEmpresa,listaMarc.IdEmpleado,listaMarc.Entrada1, listaMarc.Entrada2, listaMarc.Salida1, listaMarc.Salida2, listaMarc.SalidaLounch, listaMarc.RegresoLounch, listaMarc.pe_nombreCompleto, listaMarc.pe_cedulaRuc, listaMarc.es_fechaRegistro
from 
(


select marcaciones.Entrada1, marcaciones.Entrada2,marcaciones.Salida1,marcaciones.Salida2,marcaciones.SalidaLounch,marcaciones.RegresoLounch, empleado.*
from (
SELECT        emp.IdEmpresa, emp.IdEmpleado, per.pe_nombreCompleto, per.pe_cedulaRuc, marc.es_fechaRegistro, marc.es_Hora, m_tipo.ma_descripcion
FROM            dbo.ro_marcaciones_tipo AS m_tipo INNER JOIN
                         dbo.ro_marcaciones_x_empleado AS marc ON m_tipo.IdTipoMarcaciones = marc.IdTipoMarcaciones INNER JOIN
                         dbo.ro_empleado AS emp ON marc.IdEmpresa = emp.IdEmpresa AND marc.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.tb_persona AS per ON emp.IdPersona = per.IdPersona

) empleado
inner join (
(
 SELECT  IdEmpresa,IdEmpleado,IdCalendadrio, es_fechaRegistro, ISNULL( [IN1],'00:00:00')Entrada1 ,ISNULL([IN2],'00:00:00')Entrada2,
 ISNULL([OUT1],'00:00:00')Salida1,ISNULL([OUT2],'00:00:00')Salida2, ISNULL([SLUNCH],'00:00:00')SalidaLounch,ISNULL([RLUNCH],'00:00:00')RegresoLounch
FROM (
    SELECT 
        IdEmpresa,IdEmpleado,IdCalendadrio,es_fechaRegistro,IdTipoMarcaciones, es_Hora
    FROM ro_marcaciones_x_empleado
) as s
PIVOT
(
   max([es_Hora])
    FOR [IdTipoMarcaciones] IN ([IN1],[IN2],[OUT1],[OUT2],[RLUNCH],[SLUNCH])
)AS pvt)
) marcaciones

on
 empleado.IdEmpresa=marcaciones.IdEmpresa
 and empleado.IdEmpleado=marcaciones.IdEmpleado
 ) listaMarc

 group by listaMarc.IdEmpresa,listaMarc.IdEmpleado,listaMarc.Entrada1, listaMarc.Entrada2, listaMarc.Salida1, listaMarc.Salida2, listaMarc.SalidaLounch, listaMarc.RegresoLounch, listaMarc.pe_nombreCompleto, listaMarc.pe_cedulaRuc, listaMarc.es_fechaRegistro
 end

 