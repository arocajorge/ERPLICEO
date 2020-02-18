--exec [Academico].[SPACA_005] 1, 1
CREATE PROCEDURE [Academico].[SPACA_005]
(
@IdEmpresa int,
@IdAlumno numeric
)
AS

SELECT dbo.aca_SocioEconomico.IdEmpresa, dbo.aca_SocioEconomico.IdSocioEconomico, dbo.aca_SocioEconomico.IdAlumno, dbo.aca_Alumno.Codigo AS CodigoAlumno, PersonaAlumno.pe_nombreCompleto AS NombreAlumno, 
                  PersonaAlumno.pe_fechaNacimiento AS FechaNacAlumno, dbo.tb_provincia.Descripcion_Prov AS ProvinciaAlumno, dbo.tb_ciudad.Descripcion_Ciudad AS CiudadAlumno, CatalogoSexo.ca_descripcion AS SexoAlumno, 
                  PersonaAlumno.pe_cedulaRuc AS CedulaAlumno, dbo.aca_Alumno.Direccion AS DireccionAlumno, dbo.aca_Alumno.Sector AS SectorAlumno, PersonaAlumno.CodCatalogoCONADIS AS TieneDiscapacidadAlumno, 
                  CatalogoDiscapacidad.ca_descripcion AS DiscapacidadAlumno, dbo.aca_Alumno.LugarNacimiento, dbo.tb_parroquia.nom_parroquia AS ParroquiaAlumno, iif((dbo.aca_SocioEconomico.TieneElectricidad = 1), 'SI', 'NO') TieneElectricidad, 
                  iif((dbo.aca_SocioEconomico.TieneHermanos = 1), 'SI', 'NO') TieneHermanos, dbo.aca_SocioEconomico.CantidadHermanos, TipoVivienda.NomCatalogoFicha AS TipoVivienda, TenenciaVivienda.NomCatalogoFicha AS TenenciaVivienda, 
                  Agua.NomCatalogoFicha AS Agua, dbo.aca_SocioEconomico.SueldoPadre, dbo.aca_SocioEconomico.SueldoMadre, dbo.aca_SocioEconomico.OtroIngresoPadre, dbo.aca_SocioEconomico.OtroIngresoMadre, 
                  dbo.aca_SocioEconomico.GastoAlimentacion, dbo.aca_SocioEconomico.GastoEducacion, dbo.aca_SocioEconomico.GastoServicioBasico, dbo.aca_SocioEconomico.GastoSalud, dbo.aca_SocioEconomico.GastoArriendo, 
                  dbo.aca_SocioEconomico.GastoPrestamo, dbo.aca_SocioEconomico.OtroGasto, MotivoIngreso.NomCatalogoFicha AS MotivoIngreso, dbo.aca_SocioEconomico.OtroMotivoIngreso, 
                  InformacionInstitucion.NomCatalogoFicha AS InformacionInstitucion, dbo.aca_SocioEconomico.OtroInformacionInst, FinanciaEstudios.NomCatalogoFicha AS FinanciaEstudios, dbo.aca_SocioEconomico.OtroFinanciamiento, 
                  ViveCon.NomCatalogoFicha AS AlumnoViveCon,
				  NomPadre,DireccionPadre,NomEstadoCivilPadre,CelularPadre,ProfesionPadre, NomInstruccionPadre,CorreoPadre,EmpresaTrabajoPadre,DireccionTrabajoPadre,TelefonoTrabajoPadre,CargoTrabajoPadre,AniosServicioPadre,
				  IngresoMensualPadre,VehiculoPropioPadre,MarcaPadre,ModeloPadre, AnioVehiculoPadre,
				  NomMadre,DireccionMadre,NomEstadoCivilMadre,CelularMadre,ProfesionMadre, NomInstruccionMadre,CorreoMadre,EmpresaTrabajoMadre,DireccionTrabajoMadre,TelefonoTrabajoMadre,CargoTrabajoMadre,AniosServicioMadre,
				  IngresoMensualMadre,VehiculoPropioMadre,MarcaMadre,ModeloMadre, AnioVehiculoMadre,
				  NomRepresentante,DireccionRepresentante,NomEstadoCivilRepresentante,CelularRepresentante,ProfesionRepresentante, NomInstruccionRepresentante,CorreoRepresentante,EmpresaTrabajoRepresentante,DireccionTrabajoRepresentante,TelefonoTrabajoRepresentante,CargoTrabajoRepresentante,AniosServicioRepresentante,
				  IngresoMensualRepresentante,VehiculoPropioRepresentante,MarcaRepresentante,ModeloRepresentante, AnioVehiculoRepresentante, Calificacion.Conducta, Calificacion.Promedio
FROM     dbo.aca_SocioEconomico INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_SocioEconomico.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_SocioEconomico.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona AS PersonaAlumno ON dbo.aca_Alumno.IdPersona = PersonaAlumno.IdPersona LEFT OUTER JOIN
                  dbo.tb_provincia ON dbo.aca_Alumno.IdProvincia = dbo.tb_provincia.IdProvincia LEFT OUTER JOIN
                  dbo.tb_ciudad ON dbo.aca_Alumno.IdCiudad = dbo.tb_ciudad.IdCiudad LEFT OUTER JOIN
                  dbo.tb_Catalogo AS CatalogoSexo ON PersonaAlumno.pe_sexo = CatalogoSexo.CodCatalogo LEFT OUTER JOIN
                  dbo.tb_parroquia ON dbo.aca_Alumno.IdParroquia = dbo.tb_parroquia.IdParroquia LEFT OUTER JOIN
                  dbo.aca_CatalogoFicha AS ViveCon ON dbo.aca_SocioEconomico.IdCatalogoFichaVive = ViveCon.IdCatalogoFicha LEFT OUTER JOIN
                  dbo.aca_CatalogoFicha AS FinanciaEstudios ON dbo.aca_SocioEconomico.IdCatalogoFichaFin = FinanciaEstudios.IdCatalogoFicha LEFT OUTER JOIN
                  dbo.aca_CatalogoFicha AS InformacionInstitucion ON dbo.aca_SocioEconomico.IdCatalogoFichaIns = InformacionInstitucion.IdCatalogoFicha LEFT OUTER JOIN
                  dbo.aca_CatalogoFicha AS MotivoIngreso ON dbo.aca_SocioEconomico.IdCatalogoFichaMot = MotivoIngreso.IdCatalogoFicha LEFT OUTER JOIN
                  dbo.aca_CatalogoFicha AS Agua ON dbo.aca_SocioEconomico.IdCatalogoFichaAg = Agua.IdCatalogoFicha LEFT OUTER JOIN
                  dbo.aca_CatalogoFicha AS TenenciaVivienda ON dbo.aca_SocioEconomico.IdCatalogoFichaTVi = TenenciaVivienda.IdCatalogoFicha LEFT OUTER JOIN
                  dbo.aca_CatalogoFicha AS TipoVivienda ON dbo.aca_SocioEconomico.IdCatalogoFichaVi = TipoVivienda.IdCatalogoFicha LEFT OUTER JOIN
                  dbo.tb_Catalogo AS CatalogoDiscapacidad ON PersonaAlumno.CodCatalogoCONADIS = CatalogoSexo.CodCatalogo
				  ------PADRE------
				  left join
				  (
					SELECT f.IdEmpresa, f.IdAlumno, f.Secuencia, 
					'DATOS DE LA PADRE' AS TituloPadre, p.pe_nombreCompleto AS NomPadre, dbo.tb_Catalogo.ca_descripcion AS NomEstadoCivilPadre, f.Direccion as DireccionPadre, dbo.aca_CatalogoFicha.NomCatalogoFicha AS NomInstruccionPadre, 
					f.EmpresaTrabajo as EmpresaTrabajoPadre, f.DireccionTrabajo DireccionTrabajoPadre, f.TelefonoTrabajo TelefonoTrabajoPadre, f.CargoTrabajo CargoTrabajoPadre, f.AniosServicio AniosServicioPadre,
					f.Correo as CorreoPadre, CASE WHEN f.VehiculoPropio = 1 THEN 'SI' ELSE 'NO' END AS VehiculoPropioPadre, f.Marca as MarcaPadre, f.Modelo as ModeloPadre, p.pe_cedulaRuc as pe_cedulaRucPadre, 
					dbo.tb_profesion.Descripcion as ProfesionPadre, f.Celular as CelularPadre, f.IngresoMensual IngresoMensualPadre, f.AnioVehiculo as AnioVehiculoPadre, 
					ISNULL(dbo.aca_SocioEconomico.OtroIngresoPadre,0) AS OtrosIngresosPadre, tb_profesion.Descripcion as NomProfesionPadre
					FROM     dbo.aca_Familia AS f INNER JOIN
					dbo.tb_persona AS p ON f.IdPersona = p.IdPersona LEFT JOIN
					dbo.aca_SocioEconomico ON f.IdEmpresa = dbo.aca_SocioEconomico.IdEmpresa AND f.IdAlumno = dbo.aca_SocioEconomico.IdAlumno LEFT OUTER JOIN
					dbo.aca_Catalogo AS c ON f.IdCatalogoPAREN = c.IdCatalogo LEFT OUTER JOIN
					dbo.tb_Catalogo ON p.IdEstadoCivil = dbo.tb_Catalogo.CodCatalogo LEFT OUTER JOIN
					dbo.tb_profesion ON p.IdProfesion = dbo.tb_profesion.IdProfesion LEFT OUTER JOIN
					dbo.aca_CatalogoFicha ON f.IdCatalogoFichaInst = dbo.aca_CatalogoFicha.IdCatalogoFicha
					WHERE f.IdEmpresa = @IdEmpresa and f.IdAlumno = @IdAlumno
					--f.IdEmpresa = 1 and f.IdAlumno = 4605 and f.IdCatalogoPAREN=10
				  ) 
				  Padre on aca_Alumno.IdEmpresa = Padre.IdEmpresa and aca_Alumno.IdAlumno = Padre.IdAlumno
				  -----MADRE------
				  				  left join
				  (
					SELECT f.IdEmpresa, f.IdAlumno, f.Secuencia, 
					'DATOS DE LA MADRE' AS TituloMadre, p.pe_nombreCompleto AS NomMadre, dbo.tb_Catalogo.ca_descripcion AS NomEstadoCivilMadre, f.Direccion as DireccionMadre, dbo.aca_CatalogoFicha.NomCatalogoFicha AS NomInstruccionMadre, 
					f.EmpresaTrabajo as EmpresaTrabajoMadre, f.DireccionTrabajo DireccionTrabajoMadre, f.TelefonoTrabajo TelefonoTrabajoMadre, f.CargoTrabajo CargoTrabajoMadre, f.AniosServicio AniosServicioMadre,
					f.Correo as CorreoMadre, CASE WHEN f.VehiculoPropio = 1 THEN 'SI' ELSE 'NO' END AS VehiculoPropioMadre, f.Marca as MarcaMadre, f.Modelo as ModeloMadre, p.pe_cedulaRuc as pe_cedulaRucMadre, 
					dbo.tb_profesion.Descripcion as ProfesionMadre, f.Celular as CelularMadre, f.IngresoMensual IngresoMensualMadre, f.AnioVehiculo as AnioVehiculoMadre, 
					ISNULL(dbo.aca_SocioEconomico.OtroIngresoMadre,0) AS OtrosIngresosMadre, tb_profesion.Descripcion as NomProfesionMadre
					FROM     dbo.aca_Familia AS f INNER JOIN
					dbo.tb_persona AS p ON f.IdPersona = p.IdPersona LEFT JOIN
					dbo.aca_SocioEconomico ON f.IdEmpresa = dbo.aca_SocioEconomico.IdEmpresa AND f.IdAlumno = dbo.aca_SocioEconomico.IdAlumno LEFT OUTER JOIN
					dbo.aca_Catalogo AS c ON f.IdCatalogoPAREN = c.IdCatalogo LEFT OUTER JOIN
					dbo.tb_Catalogo ON p.IdEstadoCivil = dbo.tb_Catalogo.CodCatalogo LEFT OUTER JOIN
					dbo.tb_profesion ON p.IdProfesion = dbo.tb_profesion.IdProfesion LEFT OUTER JOIN
					dbo.aca_CatalogoFicha ON f.IdCatalogoFichaInst = dbo.aca_CatalogoFicha.IdCatalogoFicha
					WHERE f.IdEmpresa = @IdEmpresa and f.IdAlumno = @IdAlumno
					--f.IdEmpresa = 1 and f.IdAlumno = 4605 and f.IdCatalogoPAREN=11
				  ) 
				  Madre on aca_Alumno.IdEmpresa = Madre.IdEmpresa and aca_Alumno.IdAlumno = Madre.IdAlumno
				  -----REPRESENTANTE---------
				left join
				  (
					SELECT f.IdEmpresa, f.IdAlumno, f.Secuencia, 
					'DATOS DEL REPRESENTANTE' AS TituloRepresentante, p.pe_nombreCompleto AS NomRepresentante, dbo.tb_Catalogo.ca_descripcion AS NomEstadoCivilRepresentante, f.Direccion as DireccionRepresentante, dbo.aca_CatalogoFicha.NomCatalogoFicha AS NomInstruccionRepresentante, 
					f.EmpresaTrabajo as EmpresaTrabajoRepresentante, f.DireccionTrabajo DireccionTrabajoRepresentante, f.TelefonoTrabajo TelefonoTrabajoRepresentante, f.CargoTrabajo CargoTrabajoRepresentante, f.AniosServicio AniosServicioRepresentante,
					f.Correo as CorreoRepresentante, CASE WHEN f.VehiculoPropio = 1 THEN 'SI' ELSE 'NO' END AS VehiculoPropioRepresentante, f.Marca as MarcaRepresentante, f.Modelo as ModeloRepresentante, p.pe_cedulaRuc as pe_cedulaRucRepresentante, 
					dbo.tb_profesion.Descripcion as ProfesionRepresentante, f.Celular as CelularRepresentante, f.IngresoMensual IngresoMensualRepresentante, f.AnioVehiculo as AnioVehiculoRepresentante, 
					CASE WHEN F.IdCatalogoPAREN = 11 THEN  ISNULL(dbo.aca_SocioEconomico.OtroIngresoPadre,0) WHEN F.IdCatalogoPAREN = 10 THEN ISNULL(dbo.aca_SocioEconomico.OtroIngresoMadre,0) ELSE 0 END AS OtrosIngresosRepresentante, tb_profesion.Descripcion as NomProfesionRepresentante
					FROM     dbo.aca_Familia AS f INNER JOIN
					dbo.tb_persona AS p ON f.IdPersona = p.IdPersona LEFT JOIN
					dbo.aca_SocioEconomico ON f.IdEmpresa = dbo.aca_SocioEconomico.IdEmpresa AND f.IdAlumno = dbo.aca_SocioEconomico.IdAlumno LEFT OUTER JOIN
					dbo.aca_Catalogo AS c ON f.IdCatalogoPAREN = c.IdCatalogo LEFT OUTER JOIN
					dbo.tb_Catalogo ON p.IdEstadoCivil = dbo.tb_Catalogo.CodCatalogo LEFT OUTER JOIN
					dbo.tb_profesion ON p.IdProfesion = dbo.tb_profesion.IdProfesion LEFT OUTER JOIN
					dbo.aca_CatalogoFicha ON f.IdCatalogoFichaInst = dbo.aca_CatalogoFicha.IdCatalogoFicha
					WHERE f.IdEmpresa = @IdEmpresa and f.IdAlumno = @IdAlumno AND F.EsRepresentante = 1
					--f.IdEmpresa = 1 and f.IdAlumno = 4605 and f.EsRepresentante=1
				  ) 
				  Representante on aca_Alumno.IdEmpresa = Representante.IdEmpresa and aca_Alumno.IdAlumno = Representante.IdAlumno left join
				  (
					  select a.IdEmpresa, a.IdAlumno, a.IdAnio, a.Conducta, a.Promedio
					  from aca_AnioLectivoCalificacionHistorico as a inner join aca_AnioLectivo as b
					  on a.IdEmpresa = b.IdEmpresa and a.IdAnio = b.IdAnio -1
					  where a.IdEmpresa = @IdEmpresa and a.IdAlumno = @IdAlumno and b.EnCurso = 1 
				  ) Calificacion on aca_Alumno.IdEmpresa = Calificacion.IdEmpresa and aca_Alumno.IdAlumno = Calificacion.IdAlumno 

where 
aca_SocioEconomico.IdEmpresa = @IdEmpresa AND aca_SocioEconomico.IdAlumno = @IdAlumno
--aca_SocioEconomico.IdEmpresa = 1 AND aca_SocioEconomico.IdAlumno = 4605