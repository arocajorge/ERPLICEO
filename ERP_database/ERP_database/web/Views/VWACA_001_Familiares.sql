CREATE VIEW [web].[VWACA_001_Familiares]
AS
SELECT aca_Familia.IdEmpresa, aca_Familia.IdAlumno, aca_Familia.Secuencia, aca_Familia.IdCatalogoPAREN, aca_CatalogoFicha.NomCatalogoFicha AS NomInstruccion, aca_Catalogo.NomCatalogo, tb_persona.pe_nombreCompleto, 
                  tb_persona.pe_cedulaRuc, CatEstadoCivil.ca_descripcion AS NomEstadoCivil, aca_Familia.Direccion, aca_Familia.EmpresaTrabajo, tb_profesion.Descripcion AS NomProfesion, aca_Familia.Correo, aca_Familia.Celular, 
                  aca_SocioEconomico.SueldoPadre, CASE WHEN aca_Familia.IdCatalogoPAREN = 10 THEN aca_SocioEconomico.SueldoPadre ELSE aca_SocioEconomico.SueldoMadre END AS Sueldo, 
                  CASE WHEN aca_Familia.IdCatalogoPAREN = 10 THEN aca_SocioEconomico.OtroIngresoPadre ELSE aca_SocioEconomico.OtroIngresoMadre END AS OtrosIngresos, 
                  CASE WHEN aca_Familia.VehiculoPropio = 1 THEN 'SI' ELSE 'NO' END AS VehiculoPropio, aca_Familia.Marca, aca_Familia.Modelo, aca_Familia.AniosServicio,aca_Familia.AnioVehiculo,
				  'DATOS DE' + (CASE WHEN aca_Familia.IdCatalogoPAREN = 10 THEN 'L PADRE ' ELSE ' LA MADRE ' END) + (CASE WHEN aca_Familia.EsRepresentante = 1 THEN '-- REPRESENTANTE DE LA FAMILIA 'ELSE '' END)+ (CASE WHEN aca_Familia.SeFactura = 1 THEN '-- EMITE FACTURA' ELSE '' END) AS Titulo
FROM     aca_Familia INNER JOIN
                  tb_persona ON aca_Familia.IdPersona = tb_persona.IdPersona INNER JOIN
                  tb_Catalogo AS CatEstadoCivil ON tb_persona.IdEstadoCivil = CatEstadoCivil.CodCatalogo INNER JOIN
                  aca_CatalogoFicha ON aca_Familia.IdCatalogoFichaInst = aca_CatalogoFicha.IdCatalogoFicha INNER JOIN
                  aca_Catalogo ON aca_Familia.IdCatalogoPAREN = aca_Catalogo.IdCatalogo LEFT OUTER JOIN
                  aca_SocioEconomico ON aca_Familia.IdEmpresa = aca_SocioEconomico.IdEmpresa AND aca_Familia.IdAlumno = aca_SocioEconomico.IdAlumno LEFT OUTER JOIN
                  tb_profesion ON tb_persona.IdProfesion = tb_profesion.IdProfesion
WHERE aca_Familia.IdCatalogoPAREN IN (10,11)