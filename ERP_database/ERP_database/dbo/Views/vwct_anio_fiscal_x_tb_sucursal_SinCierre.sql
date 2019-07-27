CREATE VIEW vwct_anio_fiscal_x_tb_sucursal_SinCierre
AS
SELECT ct_anio_fiscal_x_cuenta_utilidad.IdEmpresa, ct_anio_fiscal_x_cuenta_utilidad.IdanioFiscal, tb_sucursal.IdSucursal
FROM     ct_anio_fiscal_x_cuenta_utilidad INNER JOIN
                  tb_sucursal ON ct_anio_fiscal_x_cuenta_utilidad.IdEmpresa = tb_sucursal.IdEmpresa LEFT OUTER JOIN
                  ct_anio_fiscal_x_tb_sucursal ON tb_sucursal.IdEmpresa = ct_anio_fiscal_x_tb_sucursal.IdEmpresa AND tb_sucursal.IdSucursal = ct_anio_fiscal_x_tb_sucursal.IdSucursal AND 
                  ct_anio_fiscal_x_cuenta_utilidad.IdEmpresa = ct_anio_fiscal_x_tb_sucursal.IdEmpresa AND ct_anio_fiscal_x_cuenta_utilidad.IdanioFiscal = ct_anio_fiscal_x_tb_sucursal.IdanioFiscal
where ct_anio_fiscal_x_tb_sucursal.IdEmpresa is null