CREATE VIEW vwcp_ConciliacionAnticipo
AS
SELECT cp_ConciliacionAnticipo.IdEmpresa, cp_ConciliacionAnticipo.IdConciliacion, cp_ConciliacionAnticipo.IdSucursal, cp_ConciliacionAnticipo.IdProveedor, cp_ConciliacionAnticipo.Fecha, cp_ConciliacionAnticipo.Observacion, 
                  cp_ConciliacionAnticipo.Estado, cp_ConciliacionAnticipo.IdCbteCble, tb_persona.pe_nombreCompleto, tb_sucursal.Su_Descripcion
FROM     cp_ConciliacionAnticipo INNER JOIN
                  tb_sucursal ON cp_ConciliacionAnticipo.IdEmpresa = tb_sucursal.IdEmpresa AND cp_ConciliacionAnticipo.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                  cp_proveedor ON cp_ConciliacionAnticipo.IdEmpresa = cp_proveedor.IdEmpresa AND cp_ConciliacionAnticipo.IdProveedor = cp_proveedor.IdProveedor INNER JOIN
                  tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona