CREATE VIEW vwaca_Matricula_AlumnosPorParalelo
AS
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, p.pe_nombreCompleto, p.pe_cedulaRuc, a.IdMatricula, a.Fecha
from aca_Matricula as a inner join 
aca_Alumno as b on a.IdEmpresa = b.IdEmpresa and a.IdAlumno = b.IdAlumno inner join
tb_persona as p on b.IdPersona = p.IdPersona
where b.Estado = 1