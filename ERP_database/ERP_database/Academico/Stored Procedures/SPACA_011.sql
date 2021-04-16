CREATE PROCEDURE [Academico].[SPACA_011]
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
/*DECLARE 
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int

SET @IdEmpresa =1
SET @IdAnio =1
SET @IdSede =1
SET @IdNivel =3
SET @IdJornada =1
SET @IdCurso =4
SET @IdParalelo =1
*/
SELECT  ROW_NUMBER() OVER (PARTITION BY M.IdMatricula
ORDER BY CM.OrdenMateria ASC) AS RowNumber,
MC.IdEmpresa, MC.IdMatricula, M.IdAlumno, P.pe_nombreCompleto, A.LugarNacimiento, P.pe_fechaNacimiento, YEAR(GETDATE()) - YEAR(P.pe_fechaNacimiento) AS Edad, M.IdPersonaR, C.NomCatalogo AS Parentezco, 
                  P_1.pe_nombreCompleto AS Representante, F.Direccion, F.Celular, M.IdAnio, M.IdSede, M.IdNivel, M.IdJornada, M.IdCurso, M.IdParalelo, MC.IdMateria, CM.NomMateria, CM.OrdenMateria, MC.IdProfesor, MC.CalificacionP1, 
                  MC.CalificacionP2, MC.CalificacionP3, MC.PromedioQ1, MC.ExamenQ1, MC.PromedioFinalQ1, MC.CalificacionP4, MC.CalificacionP5, MC.CalificacionP6, MC.PromedioQ2, MC.ExamenQ2, MC.PromedioFinalQ2, MC.ExamenMejoramiento, 
                  MC.CampoMejoramiento, CASE WHEN (MC.CampoMejoramiento IS NULL OR
                  MC.CampoMejoramiento = '') THEN ROUND(((MC.PromedioFinalQ1 + MC.PromedioFinalQ2) / 2), 2) 
                  ELSE CASE WHEN (MC.CampoMejoramiento = 'Q1') THEN ROUND(((MC.ExamenMejoramiento + MC.PromedioFinalQ2) / 2), 2) 
                  ELSE CASE WHEN (MC.CampoMejoramiento = 'Q2') THEN ROUND(((MC.PromedioFinalQ1 + MC.ExamenMejoramiento) / 2), 2) 
                  END END END AS PromedioQuimestral, MC.ExamenSupletorio, MC.ExamenRemedial, MC.ExamenGracia, MC.PromedioFinal,
				  ( ISNULL(ma.FJustificadaP1,0) +ISNULL(ma.FJustificadaP2,0) +ISNULL(ma.FJustificadaP3,0) +ISNULL(ma.FJustificadaP4,0) +ISNULL(ma.FJustificadaP5,0) +ISNULL(ma.FJustificadaP6,0)) FaltaJustificada,
				  ( ISNULL(ma.FInjustificadaP1,0) +ISNULL(ma.FInjustificadaP2,0) +ISNULL(ma.FInjustificadaP3,0) +ISNULL(ma.FInjustificadaP4,0) +ISNULL(ma.FInjustificadaP5,0) +ISNULL(ma.FInjustificadaP6,0)) FaltaInjustificada,
				  q1.Letra CondQ1, q2.Letra CondQ2, pf.Letra CondFinal
FROM     dbo.aca_MatriculaCalificacion AS MC with (nolock) INNER JOIN
                  dbo.aca_Matricula AS M with (nolock) ON MC.IdEmpresa = M.IdEmpresa AND MC.IdMatricula = M.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS A with (nolock) ON M.IdEmpresa = A.IdEmpresa AND M.IdAlumno = A.IdAlumno INNER JOIN
                  dbo.tb_persona AS P with (nolock) ON A.IdPersona = P.IdPersona INNER JOIN
                  dbo.aca_Familia AS F with (nolock) ON M.IdEmpresa = F.IdEmpresa AND M.IdAlumno = F.IdAlumno AND M.IdPersonaR = F.IdPersona AND F.EsRepresentante = 1 INNER JOIN
                  dbo.aca_Catalogo AS C with (nolock) ON F.IdCatalogoPAREN = C.IdCatalogo INNER JOIN
                  dbo.tb_persona AS P_1 with (nolock) ON M.IdPersonaR = P_1.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS CM with (nolock) ON M.IdEmpresa = CM.IdEmpresa AND M.IdAnio = CM.IdAnio AND M.IdSede = CM.IdSede AND M.IdNivel = CM.IdNivel AND M.IdJornada = CM.IdJornada AND M.IdCurso = CM.IdCurso AND 
                  MC.IdMateria = CM.IdMateria AND MC.IdEmpresa = CM.IdEmpresa
				  left outer join aca_MatriculaAsistencia ma on ma.IdEmpresa= m.IdEmpresa and ma.IdMatricula=m.IdMatricula
				  left outer join aca_MatriculaConducta mco on mco.IdEmpresa= m.IdEmpresa and mco.IdMatricula=m.IdMatricula
				left outer join aca_AnioLectivoConductaEquivalencia q1 on q1.IdEmpresa = m.IdEmpresa and q1.IdAnio=m.IdAnio and q1.Secuencia=mco.SecuenciaPromedioFinalQ1 
				left outer join aca_AnioLectivoConductaEquivalencia q2 on q2.IdEmpresa = m.IdEmpresa and q2.IdAnio=m.IdAnio and q2.Secuencia=mco.SecuenciaPromedioFinalQ2 
				left outer join aca_AnioLectivoConductaEquivalencia pf on pf.IdEmpresa = m.IdEmpresa and pf.IdAnio=m.IdAnio and pf.Secuencia=mco.SecuenciaPromedioFinal 
WHERE  MC.IdEmpresa=@IdEmpresa
and M.IdAnio = @IdAnio
and M.IdSede = @IdSede 
and M.IdNivel = @IdNivel
and M.IdJornada =  @IdJornada
and M.IdCurso = @IdCurso
and M.IdParalelo = @IdParalelo