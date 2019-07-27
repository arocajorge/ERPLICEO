CREATE VIEW vwseg_usuario_x_tb_sucursal
AS
SELECT seg_usuario_x_tb_sucursal.IdUsuario, seg_usuario_x_tb_sucursal.IdEmpresa, seg_usuario_x_tb_sucursal.IdSucursal, tb_sucursal.Su_Descripcion, tb_empresa.em_nombre
FROM     seg_usuario_x_tb_sucursal INNER JOIN
                  tb_sucursal ON seg_usuario_x_tb_sucursal.IdEmpresa = tb_sucursal.IdEmpresa AND seg_usuario_x_tb_sucursal.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                  tb_empresa ON tb_sucursal.IdEmpresa = tb_empresa.IdEmpresa