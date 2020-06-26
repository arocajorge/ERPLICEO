CREATE VIEW [dbo].[vwaca_Matricula_Rubro_PorFacturarMasiva]
AS
select a.IdEmpresa, a.IdMatricula, a.IdAnio, a.IdPeriodo, a.IdPlantilla, a.IdRubro, a.IdProducto, a.Subtotal, a.IdCod_Impuesto_Iva, a.Porcentaje, a.ValorIVA, a.Total, 

CASE WHEN j.AplicaProntoPago = 1 AND d.FechaProntoPago > CAST(getdate() 
AS date) THEN CAST(k.Subtotal AS float) - (CASE WHEN l.AplicaParaTodo = 1 THEN (CASE WHEN l.TipoDescuento = '$' THEN CAST(l.Valor AS FLOAT) 
ELSE ROUND(CAST(k.Subtotal AS float) * (l.Valor / 100), 2) END) 
ELSE (CASE WHEN k.TipoDescuento_descuentoDet = '$' THEN CAST(k.Valor_descuentoDet AS FLOAT) ELSE ROUND(CAST(k.Subtotal AS float) 
* (k.Valor_descuentoDet / 100), 2) END) END) ELSE CAST(k.Subtotal AS float) + CAST(k.ValorIVA AS FLOAT) 
END + k.ValorIVA AS ValorProntoPago, 
c.NomRubro +
case when c.NumeroCuotas > 1 then +' '+ cast(e.Secuencia as varchar) +'/'+ cast(c.NumeroCuotas as varchar) else '' end
 +' '+ f.smes +' '+cast(year(d.FechaDesde) as varchar) as Observacion, h.IdAlumno,d.FechaDesde, d.FechaProntoPago, b.IdTerminoPago, i.IdCliente,

 m.Codigo, n.pe_nombreCompleto  Alumno
from aca_Matricula_Rubro as a inner join
aca_MecanismoDePago as b on a.IdEmpresa = b.IdEmpresa and a.IdMecanismo = b.IdMecanismo left join
aca_AnioLectivo_Rubro as c on a.IdEmpresa = c.IdEmpresa and a.IdRubro = c.IdRubro and a.IdRubro = c.IdRubro left join
aca_AnioLectivo_Periodo as d on a.IdEmpresa = d.IdEmpresa and a.IdAnio = d.IdAnio and a.IdPeriodo = d.IdPeriodo left join
aca_AnioLectivo_Rubro_Periodo as e on a.IdEmpresa = e.IdEmpresa and a.IdRubro = e.IdRubro and a.IdPeriodo = e.IdPeriodo and a.IdAnio = e.IdAnio left join
tb_mes as f on d.IdMes = f.idMes left join
aca_AnioLectivo as g on a.IdEmpresa = g.IdEmpresa and a.IdAnio = g.IdAnio inner join
aca_Matricula as h on a.IdEmpresa = h.IdEmpresa and a.IdMatricula = h.IdMatricula left join
(
select IdEmpresa, IdPersona, max(IdCliente) IdCliente from fa_cliente
group by IdEmpresa, IdPersona
) as i on h.IdPersonaF = i.IdPersona and h.IdEmpresa = i.IdEmpresa left join
aca_AnioLectivo_Rubro as j on j.IdEmpresa = a.IdEmpresa and j.IdAnio = a.IdAnio and j.IdRubro = a.IdRubro left join
aca_Plantilla_Rubro as k on a.IdEmpresa = k.IdEmpresa and a.IdAnio = k.IdAnio and a.IdPlantilla = k.IdPlantilla and a.IdRubro = k.IdRubro left join
aca_Plantilla as l on l.IdEmpresa = k.IdEmpresa and l.IdAnio = k.IdAnio and l.IdPlantilla = k.IdPlantilla left join 
aca_Alumno as m on h.IdEmpresa = m.IdEmpresa and h.IdAlumno = m.IdAlumno left join
tb_persona as n on m.IdPersona = n.IdPersona


where a.FechaFacturacion is null and not exists(
select ret.IdEmpresa 
from aca_AlumnoRetiro as ret
where ret.IdEmpresa = a.IdEmpresa and ret.IdMatricula = a.IdMatricula and ret.Estado = 1
)