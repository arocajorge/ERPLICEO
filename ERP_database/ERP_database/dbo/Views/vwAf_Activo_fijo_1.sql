CREATE VIEW vwAf_Activo_fijo
AS
SELECT Af_Activo_fijo.IdEmpresa, Af_Activo_fijo.IdActivoFijo, Af_Activo_fijo.Af_Nombre, Af_Activo_fijo.Estado, Af_Catalogo.Descripcion AS EstadoProceso, Af_Activo_fijo_Categoria.Descripcion AS NomCategoria, 
                  Af_Activo_fijo_tipo.Af_Descripcion AS NomTipo, Af_Area.Descripcion AS NomArea, Af_Departamento.Descripcion AS NomDepartamento, p_enc.pe_nombreCompleto AS NomEncargado, p_cus.pe_nombreCompleto AS NomCustodio, 
                  Af_Activo_fijo.FechaEntrega, Af_Activo_fijo.Cantidad, Af_Activo_fijo.Estado_Proceso
FROM     tb_persona AS p_enc INNER JOIN
                  Af_Catalogo INNER JOIN
                  Af_Activo_fijo ON Af_Catalogo.IdCatalogo = Af_Activo_fijo.Estado_Proceso ON p_enc.IdPersona = Af_Activo_fijo.IdEmpleadoEncargado INNER JOIN
                  tb_persona AS p_cus ON Af_Activo_fijo.IdEmpleadoCustodio = p_cus.IdPersona LEFT OUTER JOIN
                  Af_Area INNER JOIN
                  Af_Departamento ON Af_Area.IdEmpresa = Af_Departamento.IdEmpresa AND Af_Area.IdArea = Af_Departamento.IdArea ON Af_Activo_fijo.IdEmpresa = Af_Departamento.IdEmpresa AND 
                  Af_Activo_fijo.IdDepartamento = Af_Departamento.IdDepartamento LEFT OUTER JOIN
                  Af_Activo_fijo_tipo INNER JOIN
                  Af_Activo_fijo_Categoria ON Af_Activo_fijo_tipo.IdEmpresa = Af_Activo_fijo_Categoria.IdEmpresa AND Af_Activo_fijo_tipo.IdActivoFijoTipo = Af_Activo_fijo_Categoria.IdActivoFijoTipo ON 
                  Af_Activo_fijo.IdEmpresa = Af_Activo_fijo_Categoria.IdEmpresa AND Af_Activo_fijo.IdCategoriaAF = Af_Activo_fijo_Categoria.IdCategoriaAF