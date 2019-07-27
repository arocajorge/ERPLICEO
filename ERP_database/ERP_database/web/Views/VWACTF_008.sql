CREATE VIEW web.VWACTF_008
AS
SELECT af.IdEmpresa, af.IdActivoFijo, af.Af_Nombre, af.Af_observacion, af.Estado, af.Af_fecha_compra, af.Af_costo_compra, af.Af_Vida_Util, af.IdEmpleadoEncargado, af.IdEmpleadoCustodio, 
                  ecnper.pe_nombreCompleto AS EmpleadoEncargado, cusper.pe_nombreCompleto AS EmpleadoCustodio, af.IdDepartamento, af.IdSucursal, s.Su_Descripcion, dep.Descripcion AS NomDepartamento, af.Cantidad, 
                  cat.Descripcion AS NomCategoria, tipo.Af_Descripcion AS NomTipo
FROM     ro_empleado AS cus INNER JOIN
                  tb_persona AS cusper ON cus.IdPersona = cusper.IdPersona RIGHT OUTER JOIN
                  tb_sucursal AS s INNER JOIN
                  Af_Activo_fijo AS af INNER JOIN
                  Af_Activo_fijo_Categoria AS cat ON af.IdEmpresa = cat.IdEmpresa AND af.IdCategoriaAF = cat.IdCategoriaAF INNER JOIN
                  Af_Activo_fijo_tipo AS tipo ON cat.IdActivoFijoTipo = tipo.IdActivoFijoTipo AND cat.IdEmpresa = tipo.IdEmpresa INNER JOIN
                  Af_Departamento AS dep ON af.IdDepartamento = dep.IdDepartamento AND af.IdEmpresa = dep.IdEmpresa ON s.IdEmpresa = af.IdEmpresa AND s.IdSucursal = af.IdSucursal ON cus.IdEmpleado = af.IdEmpleadoCustodio AND 
                  cus.IdEmpresa = af.IdEmpresa LEFT OUTER JOIN
                  tb_persona AS ecnper INNER JOIN
                  ro_empleado AS enc ON ecnper.IdPersona = enc.IdPersona ON af.IdEmpresa = enc.IdEmpresa AND af.IdEmpleadoEncargado = enc.IdEmpleado