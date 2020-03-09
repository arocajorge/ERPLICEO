CREATE VIEW Academico.VWACA_009
AS
SELECT a.IdEmpresa, a.IdMatricula, a.Secuencia, b.IdAlumno, c.Codigo, d.pe_nombreCompleto, d.pe_cedulaRuc, cast(a.FechaCreacion as date) FechaCreacion, a.Observacion,
a.IdAnio, e.Descripcion, a.IdUsuarioCreacion
FROM aca_MatriculaCambios as a inner join 
aca_Matricula as b on a.IdEmpresa = b.IdEmpresa and a.IdMatricula = b.IdMatricula inner join
aca_Alumno as c on b.IdEmpresa = c.IdEmpresa and b.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona inner join
aca_AnioLectivo as e on a.IdEmpresa = e.IdEmpresa and a.IdAnio = e.IdAnio