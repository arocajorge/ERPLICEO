CREATE VIEW Academico.VWACA_006
AS
SELECT sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.OrdenCurso, jc.NomCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, p.pe_sexo, 
                  CASE WHEN p.pe_sexo = 'SEXO_MAS' THEN 'H' ELSE 'M' END AS NomSexo, 1 AS Cantidad, aca_Matricula.IdEmpresa, aca_Matricula.IdMatricula, aca_Matricula.IdAnio, aca_Matricula.IdSede, aca_Matricula.IdNivel, 
                  aca_Matricula.IdJornada, aca_Matricula.IdCurso, aca_Matricula.IdParalelo, aca_Matricula.Fecha
FROM     aca_AnioLectivo AS a INNER JOIN
                  aca_AnioLectivo_Sede_NivelAcademico AS sn ON a.IdEmpresa = sn.IdEmpresa AND a.IdAnio = sn.IdAnio INNER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel INNER JOIN
                  aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada INNER JOIN
                  aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso INNER JOIN
                  aca_Matricula ON cp.IdEmpresa = aca_Matricula.IdEmpresa AND cp.IdAnio = aca_Matricula.IdAnio AND cp.IdSede = aca_Matricula.IdSede AND cp.IdNivel = aca_Matricula.IdNivel AND cp.IdJornada = aca_Matricula.IdJornada AND 
                  cp.IdCurso = aca_Matricula.IdCurso AND cp.IdParalelo = aca_Matricula.IdParalelo INNER JOIN
                  aca_Alumno AS alu ON aca_Matricula.IdEmpresa = alu.IdEmpresa AND aca_Matricula.IdAlumno = alu.IdAlumno INNER JOIN
                  tb_persona AS p ON alu.IdPersona = p.IdPersona