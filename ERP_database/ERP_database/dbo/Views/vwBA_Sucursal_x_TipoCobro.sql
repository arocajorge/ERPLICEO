create view [dbo].[vwBA_Sucursal_x_TipoCobro]
as
SELECT     tb_sucursal.IdEmpresa, tb_sucursal.IdSucursal, tb_sucursal.Su_Descripcion, cxc_cobro_tipo.IdCobro_tipo, cxc_cobro_tipo.tc_descripcion
FROM         cxc_cobro_tipo CROSS JOIN
                      tb_sucursal