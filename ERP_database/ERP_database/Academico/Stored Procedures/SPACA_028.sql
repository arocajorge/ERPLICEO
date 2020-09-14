CREATE PROCEDURE [Academico].[SPACA_028]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int
)
AS
SELECT cp.IdEmpresa, cp.IdAnio, a.Descripcion, cp.IdSede, s.NomSede, cp.IdNivel, n.NomNivel, cp.IdJornada, j.NomJornada, cp.IdCurso, c.NomCurso, cp.IdParalelo, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, 
                  cp.IdProfesorInspector, tut.IdPersona AS IdPersonaTutor, insp.IdPersona AS IdPersonaInpector, pt.pe_nombreCompleto AS NomTutor, pi.pe_nombreCompleto AS NomInspector, n.Orden AS OrdenNivel, j.OrdenJornada, 
                  c.OrdenCurso
FROM     dbo.tb_persona AS pi INNER JOIN
                  dbo.aca_Profesor AS insp ON pi.IdPersona = insp.IdPersona RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp INNER JOIN
                  dbo.aca_AnioLectivo AS a ON cp.IdEmpresa = a.IdEmpresa AND cp.IdAnio = a.IdAnio INNER JOIN
                  dbo.aca_Sede AS s ON cp.IdEmpresa = s.IdEmpresa AND cp.IdSede = s.IdSede INNER JOIN
                  dbo.aca_Jornada AS j ON cp.IdEmpresa = j.IdEmpresa AND cp.IdJornada = j.IdJornada INNER JOIN
                  dbo.aca_Curso AS c ON cp.IdEmpresa = c.IdEmpresa AND cp.IdCurso = c.IdCurso INNER JOIN
                  dbo.aca_NivelAcademico AS n ON cp.IdEmpresa = n.IdEmpresa AND cp.IdNivel = n.IdNivel ON insp.IdEmpresa = cp.IdEmpresa AND insp.IdProfesor = cp.IdProfesorInspector LEFT OUTER JOIN
                  dbo.tb_persona AS pt INNER JOIN
                  dbo.aca_Profesor AS tut ON pt.IdPersona = tut.IdPersona ON cp.IdEmpresa = tut.IdEmpresa AND cp.IdProfesorTutor = tut.IdProfesor

where cp.IdEmpresa = @IdEmpresa 
and cp.IdAnio = @IdAnio
and cp.IdSede = @IdSede
and cp.IdNivel = @IdNivel
and cp.IdJornada = @IdJornada
and cp.IdCurso = @IdCurso
and cp.IdParalelo = @IdParalelo
and a.Estado = 1