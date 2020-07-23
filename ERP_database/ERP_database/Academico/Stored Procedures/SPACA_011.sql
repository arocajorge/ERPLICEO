﻿CREATE PROCEDURE [Academico].[SPACA_011]
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
                  END END END AS PromedioQuimestral, MC.ExamenSupletorio, MC.ExamenRemedial, MC.ExamenGracia, MC.PromedioFinal
FROM     dbo.aca_MatriculaCalificacion AS MC INNER JOIN
                  dbo.aca_Matricula AS M ON MC.IdEmpresa = M.IdEmpresa AND MC.IdMatricula = M.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS A ON M.IdEmpresa = A.IdEmpresa AND M.IdAlumno = A.IdAlumno INNER JOIN
                  dbo.tb_persona AS P ON A.IdPersona = P.IdPersona INNER JOIN
                  dbo.aca_Familia AS F ON M.IdEmpresa = F.IdEmpresa AND M.IdAlumno = F.IdAlumno AND M.IdPersonaR = F.IdPersona AND F.EsRepresentante = 1 INNER JOIN
                  dbo.aca_Catalogo AS C ON F.IdCatalogoPAREN = C.IdCatalogo INNER JOIN
                  dbo.tb_persona AS P_1 ON M.IdPersonaR = P_1.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS CM ON M.IdEmpresa = CM.IdEmpresa AND M.IdAnio = CM.IdAnio AND M.IdSede = CM.IdSede AND M.IdNivel = CM.IdNivel AND M.IdJornada = CM.IdJornada AND M.IdCurso = CM.IdCurso AND 
                  MC.IdMateria = CM.IdMateria AND MC.IdEmpresa = CM.IdEmpresa
WHERE  MC.IdEmpresa=@IdEmpresa
and M.IdAnio = @IdAnio
and M.IdSede = @IdSede 
and M.IdNivel = @IdNivel
and M.IdJornada =  @IdJornada
and M.IdCurso = @IdCurso
and M.IdParalelo = @IdParalelo