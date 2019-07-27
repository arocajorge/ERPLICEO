CREATE VIEW [web].[vwcp_proveedor_consulta]
AS
SELECT cp_proveedor.IdEmpresa, cp_proveedor.IdProveedor, cp_proveedor.pr_codigo, cp_proveedor.IdPersona, tb_persona.pe_nombreCompleto, cp_proveedor.IdClaseProveedor, tb_persona.pe_cedulaRuc, cp_proveedor.pr_estado
FROM     cp_proveedor INNER JOIN
                  tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona
