CREATE view [web].[VWACTF_008]
as
SELECT af.IdEmpresa, af.IdActivoFijo, af.Af_Nombre, af.Af_observacion, af.Estado, af.Af_fecha_compra, af.Af_costo_compra, af.Af_Vida_Util, af.IdEmpleadoEncargado, af.IdEmpleadoCustodio, 
                  enc.pe_nombreCompleto AS EmpleadoEncargado, cusper.pe_nombreCompleto AS EmpleadoCustodio, af.IdDepartamento, af.IdSucursal, s.Su_Descripcion, dep.Descripcion AS NomDepartamento, af.Cantidad, 
                  cat.Descripcion AS NomCategoria, tipo.Af_Descripcion AS NomTipo
FROM     tb_persona AS cus INNER JOIN
                  dbo.tb_persona AS cusper ON cus.IdPersona = cusper.IdPersona RIGHT OUTER JOIN
                  dbo.tb_sucursal AS s INNER JOIN
                  dbo.Af_Activo_fijo AS af INNER JOIN
                  dbo.Af_Activo_fijo_Categoria AS cat ON af.IdEmpresa = cat.IdEmpresa AND af.IdCategoriaAF = cat.IdCategoriaAF INNER JOIN
                  dbo.Af_Activo_fijo_tipo AS tipo ON cat.IdActivoFijoTipo = tipo.IdActivoFijoTipo AND cat.IdEmpresa = tipo.IdEmpresa INNER JOIN
                  dbo.Af_Departamento AS dep ON af.IdDepartamento = dep.IdDepartamento AND af.IdEmpresa = dep.IdEmpresa ON s.IdEmpresa = af.IdEmpresa AND s.IdSucursal = af.IdSucursal ON cus.IdPersona = af.IdEmpleadoCustodio LEFT OUTER JOIN
                  dbo.tb_persona AS enc ON enc.IdPersona = af.IdEmpleadoEncargado