--exec [Academico].[SPACA_001] 1,4699
CREATE PROCEDURE [Academico].[SPACA_001]
(
@IdEmpresa int,
@IdAlumno numeric
)
AS

DECLARE @IdMatricula numeric, @IdAnioAnterior numeric, @IdSocioEconomico int

select @IdMatricula = max(IdMatricula) from aca_Matricula with (nolock)
where IdEmpresa = @IdEmpresa and IdAlumno = @IdAlumno

select  @IdSocioEconomico = IdSocioEconomico 
from aca_SocioEconomico with (nolock)
where IdAlumno = @IdAlumno

set @IdMatricula = isnull(@IdMatricula,0)

select @IdAnioAnterior = max(IdAnio) from aca_Matricula with (nolock)
where IdEmpresa = @IdEmpresa and IdAlumno = @IdAlumno and IdMatricula != @IdMatricula

set @IdAnioAnterior = isnull(@IdAnioAnterior,0)



SELECT m.IdEmpresa, m.IdMatricula, m.Codigo AS CodigoMatricula, m.IdAlumno, SedeNivel.NomSede, SedeNivel.NomNivel, dbo.aca_AnioLectivo.Descripcion AS NomAnio, a.Codigo AS CodigoAlumno, p.pe_nombre, p.pe_apellido, 
                  CASE WHEN p.pe_sexo = 'SEXO_MAS' THEN 'MASCULINO' ELSE 'FEMENINO' END AS pe_sexo, Pais.Nacionalidad, a.Direccion, Jornada.NomJornada, Curso.NomCurso, Paralelo.NomParalelo, Paralelo.CodigoParalelo, 
                  p.pe_fechaNacimiento, a.LugarNacimiento, a.Celular, a.Correo, m.Fecha, dbo.aca_Plantilla.NomPlantilla, cali.AntiguaInstitucion,cali.Letra, cali.Conducta, cali.Promedio, 
				  case when isnull(doc.CantidadTotal,0) > 0 and isnull(doc.CantidadTotal,0) - isnull(doc.CantidadEntregados,0) = 0 then 'SI' ELSE 'NO' end DocumentosCompletos, sc.NomVivienda, sc.NomTipoVivienda, sc.NomAgua, sc.TieneElectricidad, sc.NomGrupoEtnico,
				  sc.TieneHermanos, sc.TieneConadis, sc.NomConadis, sc.NomViveCon, sc.CantidadHermanos, sc.TotalGastos, m.Observacion, PadreMadre.Titulo, PadreMadre.NomFamiliar, PadreMadre.NomEstadoCivil, PadreMadre.Direccion as DireccionFamiliar,
				  PadreMadre.NomInstruccion, PadreMadre.EmpresaTrabajo, PadreMadre.Correo as CorreoFamiliar, PadreMadre.VehiculoPropio, PadreMadre.Marca, PadreMadre.Modelo, PadreMadre.AnioVehiculo, PadreMadre.pe_cedulaRuc as CedulaRucFamiliar,
				  PadreMadre.NomProfesion, PadreMadre.Celular as CelularFamiliar, PadreMadre.IngresoMensual, PadreMadre.OtrosIngresos, m.IdUsuarioCreacion, cali.NivelProcedencia
FROM     dbo.aca_Plantilla with (nolock) INNER JOIN
                  dbo.aca_Alumno AS a with (nolock) INNER JOIN
                  dbo.tb_persona AS p ON a.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON a.IdEmpresa = m.IdEmpresa AND a.IdAlumno = m.IdAlumno INNER JOIN
                  dbo.aca_AnioLectivo with (nolock) ON m.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND m.IdAnio = dbo.aca_AnioLectivo.IdAnio ON dbo.aca_Plantilla.IdEmpresa = m.IdEmpresa AND dbo.aca_Plantilla.IdAnio = m.IdAnio AND 
                  dbo.aca_Plantilla.IdPlantilla = m.IdPlantilla LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS SedeNivel with (nolock) INNER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS Jornada with (nolock) ON SedeNivel.IdEmpresa = Jornada.IdEmpresa AND SedeNivel.IdAnio = Jornada.IdAnio AND SedeNivel.IdSede = Jornada.IdSede AND 
                  SedeNivel.IdNivel = Jornada.IdNivel INNER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS Curso with (nolock) ON Jornada.IdEmpresa = Curso.IdEmpresa AND Jornada.IdAnio = Curso.IdAnio AND Jornada.IdSede = Curso.IdSede AND Jornada.IdNivel = Curso.IdNivel AND 
                  Jornada.IdJornada = Curso.IdJornada INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS Paralelo with (nolock) ON Curso.IdEmpresa = Paralelo.IdEmpresa AND Curso.IdAnio = Paralelo.IdAnio AND Curso.IdSede = Paralelo.IdSede AND Curso.IdNivel = Paralelo.IdNivel AND 
                  Curso.IdJornada = Paralelo.IdJornada AND Curso.IdCurso = Paralelo.IdCurso ON m.IdEmpresa = Paralelo.IdEmpresa AND m.IdAnio = Paralelo.IdAnio AND m.IdSede = Paralelo.IdSede AND m.IdNivel = Paralelo.IdNivel AND 
                  m.IdJornada = Paralelo.IdJornada AND m.IdCurso = Paralelo.IdCurso AND m.IdParalelo = Paralelo.IdParalelo LEFT OUTER JOIN
                  dbo.tb_pais AS Pais with (nolock) ON a.IdPais = Pais.IdPais LEFT JOIN
				  (
					  select a.IdEmpresa, a.IdAlumno, a.AntiguaInstitucion, a.Conducta, CE.Letra, a.Promedio, b.NomNivel NivelProcedencia
					  from [dbo].[aca_AnioLectivoCalificacionHistorico] as a with (nolock) LEFT JOIN
					  aca_AnioLectivo_Sede_NivelAcademico AS B with (nolock) ON a.IdEmpresa = b.IdEmpresa
					  and a.IdAnio = b.IdAnio and a.IdNivel = b.IdNivel LEFT JOIN
					  aca_AnioLectivoConductaEquivalencia CE ON CE.IdEmpresa= a.IdEmpresa and CE.IdAnio=a.IdAnio and CE.Secuencia=a.SecuenciaConducta
					  where a.IdEmpresa = @IdEmpresa and a.IdAlumno = @IdAlumno and a.IdAnio = @IdAnioAnterior
					  group by a.IdEmpresa, a.IdAlumno, a.AntiguaInstitucion, a.Conducta, CE.Letra, a.Promedio, b.NomNivel
				  ) Cali on a.IdEmpresa = Cali.IdEmpresa and a.IdAlumno = Cali.IdAlumno LEFT JOIN
				  (
					select A.IdEmpresa, A.IdAlumno, SUM(A.CantidadEntregados) CantidadEntregados, SUM(A.CantidadTotal) CantidadTotal from(
						SELECT A.IdEmpresa, A.IdAlumno, CASE WHEN A.EnArchivo = 1 THEN 1 ELSE 0 END CantidadEntregados, 1 AS CantidadTotal 
						FROM aca_AlumnoDocumento A with (nolock)
						WHERE A.IdEmpresa = @IdEmpresa AND A.IdAlumno = @IdAlumno
					) A
					GROUP BY A.IdEmpresa, A.IdAlumno
				  ) Doc on a.IdEmpresa = doc.IdEmpresa and a.IdAlumno = doc.IdAlumno LEFT JOIN
				  (
						SELECT a.IdEmpresa, a.IdAlumno, Vi.NomCatalogoFicha AS NomVivienda, TVi.NomCatalogoFicha AS NomTipoVivienda, Ag.NomCatalogoFicha AS NomAgua, CASE WHEN sc.TieneElectricidad = 1 THEN 'SI' ELSE 'NO' END AS TieneElectricidad, 
						dbo.tb_GrupoEtnico.NomGrupoEtnico, CASE WHEN tb_persona.CodCatalogoCONADIS IS NULL THEN 'NO' ELSE 'SI' END AS TieneConadis, dbo.tb_Catalogo.ca_descripcion AS NomConadis, 
						dbo.aca_CatalogoFicha.NomCatalogoFicha AS NomViveCon, CASE WHEN sc.TieneHermanos = 1 THEN 'SI' ELSE 'NO' END AS TieneHermanos, sc.GastoAlimentacion + sc.GastoEducacion + sc.GastoServicioBasico + sc.GastoSalud + sc.GastoArriendo +
						sc.GastoPrestamo + sc.OtroGasto AS TotalGastos, CantidadHermanos
						FROM     dbo.tb_GrupoEtnico with (nolock) INNER JOIN
						dbo.tb_persona with (nolock) INNER JOIN
						dbo.aca_Alumno AS a with (nolock) ON dbo.tb_persona.IdPersona = a.IdPersona INNER JOIN
						dbo.aca_CatalogoFicha AS TVi with (nolock) INNER JOIN
						dbo.aca_CatalogoFicha AS Vi with (nolock) INNER JOIN
						dbo.aca_SocioEconomico AS sc with (nolock) ON Vi.IdCatalogoFicha = sc.IdCatalogoFichaVi ON TVi.IdCatalogoFicha = sc.IdCatalogoFichaTVi INNER JOIN
						dbo.aca_CatalogoFicha AS Ag with (nolock) ON sc.IdCatalogoFichaAg = Ag.IdCatalogoFicha ON a.IdEmpresa = sc.IdEmpresa AND a.IdAlumno = sc.IdAlumno ON dbo.tb_GrupoEtnico.IdGrupoEtnico = dbo.tb_persona.IdGrupoEtnico INNER JOIN
						dbo.aca_CatalogoFicha with (nolock) ON sc.IdCatalogoFichaVive = dbo.aca_CatalogoFicha.IdCatalogoFicha LEFT OUTER JOIN
						dbo.tb_Catalogo with (nolock) ON dbo.tb_persona.CodCatalogoCONADIS = dbo.tb_Catalogo.CodCatalogo
						where a.IdEmpresa = @IdEmpresa and a.IdAlumno = @IdAlumno
				  ) AS SC ON a.IdEmpresa = sc.IdEmpresa and a.IdAlumno = sc.IdAlumno left join
				  (
					SELECT f.IdEmpresa, f.IdAlumno, f.Secuencia, 
					'DATOS DE' + CASE WHEN F.IdCatalogoPAREN = 10 THEN 'L PADRE' WHEN F.IdCatalogoPAREN = 11 THEN ' LA MADRE' ELSE ' ' + C.NomCatalogo END + CASE WHEN F.EsRepresentante = 1 THEN ' -- REPRESENTANTE' ELSE '' END + CASE
					WHEN F.SeFactura = 1 THEN ' -- SE EMITE FACTURA' ELSE '' END AS Titulo, p.pe_nombreCompleto AS NomFamiliar, dbo.tb_Catalogo.ca_descripcion AS NomEstadoCivil, f.Direccion, dbo.aca_CatalogoFicha.NomCatalogoFicha AS NomInstruccion, 
					f.EmpresaTrabajo, f.Correo, CASE WHEN f.VehiculoPropio = 1 THEN 'SI' ELSE 'NO' END AS VehiculoPropio, f.Marca, f.Modelo, p.pe_cedulaRuc, dbo.tb_profesion.Descripcion, f.Celular, f.IngresoMensual, f.AnioVehiculo, 
					CASE WHEN F.IdCatalogoPAREN = 11 THEN  ISNULL(dbo.aca_SocioEconomico.OtroIngresoPadre,0) WHEN F.IdCatalogoPAREN = 10 THEN ISNULL(dbo.aca_SocioEconomico.OtroIngresoMadre,0) ELSE 0 END AS OtrosIngresos,
					tb_profesion.Descripcion as NomProfesion
					FROM     dbo.aca_Familia AS f with (nolock) INNER JOIN
					dbo.tb_persona AS p with (nolock) ON f.IdPersona = p.IdPersona LEFT JOIN
					dbo.aca_SocioEconomico with (nolock) ON f.IdEmpresa = dbo.aca_SocioEconomico.IdEmpresa AND f.IdAlumno = dbo.aca_SocioEconomico.IdAlumno LEFT OUTER JOIN
					dbo.aca_Catalogo AS c with (nolock) ON f.IdCatalogoPAREN = c.IdCatalogo LEFT OUTER JOIN
					dbo.tb_Catalogo with (nolock) ON p.IdEstadoCivil = dbo.tb_Catalogo.CodCatalogo LEFT OUTER JOIN
					dbo.tb_profesion with (nolock) ON p.IdProfesion = dbo.tb_profesion.IdProfesion LEFT OUTER JOIN
					dbo.aca_CatalogoFicha with (nolock) ON f.IdCatalogoFichaInst = dbo.aca_CatalogoFicha.IdCatalogoFicha
					WHERE  ((f.IdCatalogoPAREN IN (10, 11)) or (f.SeFactura = 1) OR (f.EsRepresentante = 1))
					and f.IdEmpresa = @IdEmpresa and f.IdAlumno = @IdAlumno and aca_SocioEconomico.IdSocioEconomico = @IdSocioEconomico
				  ) PadreMadre on a.IdEmpresa = PadreMadre.IdEmpresa and a.IdAlumno = PadreMadre.IdAlumno
where M.IdEmpresa = @IdEmpresa AND M.IdAlumno = @IdAlumno AND M.IdMatricula = @IdMatricula