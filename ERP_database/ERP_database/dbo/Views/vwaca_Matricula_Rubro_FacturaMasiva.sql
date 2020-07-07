CREATE VIEW [dbo].[vwaca_Matricula_Rubro_FacturaMasiva]
AS
select a.IdEmpresa, a.IdAnio, a.IdPeriodo, a.IdSucursal, a.IdBodega, a.IdCbteVta, b.IdAlumno, d.pe_nombreCompleto, a.Total, f.Correo, e.vt_autorizacion
from aca_Matricula_Rubro as a inner join
aca_Matricula as b on a.IdEmpresa = b.IdEmpresa and a.IdMatricula = b.IdMatricula inner join
aca_Alumno as c on b.IdEmpresa = c.IdEmpresa and b.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona inner join
fa_factura as e on e.IdEmpresa = a.IdEmpresa and e.IdSucursal = a.IdSucursal and e.IdBodega = a.IdBodega and e.IdCbteVta = a.IdCbteVta inner join
fa_cliente_contactos as f on e.IdEmpresa = f.IdEmpresa and e.IdCliente = f.IdCliente
where e.AplicacionMasiva = 1 and not exists(
select z.IdEmpresa 
from aca_AlumnoRetiro as z
where a.IdEmpresa = z.IdEmpresa
and a.IdMatricula = z.IdMatricula
)