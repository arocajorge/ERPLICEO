--EXEC web.SPCXC_009 2,23,'2019/07/02'
CREATE PROCEDURE web.SPCXC_009
(
@IdEmpresa int,
@IdCliente numeric,
@FechaCorte DATE
)
AS

SELECT fa_factura.IdEmpresa, fa_factura.IdSucursal, fa_factura.IdBodega, fa_factura.IdCbteVta,fa_factura.vt_tipoDoc, fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura AS NumDocumento, fa_factura.vt_fecha, fa_factura.vt_fech_venc, 
                  tb_sucursal.Su_Descripcion, fa_factura.IdCliente, tb_persona.pe_nombreCompleto, tb_persona.pe_cedulaRuc, fa_factura.IdVendedor, fa_Vendedor.Ve_Vendedor, fa_factura_resumen.Total, CASE WHEN DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) < 0 THEN 0 ELSE DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) END DiasVencido,
				  isnull(COBRO.dc_ValorPago,0) TotalCobrado, round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) as Saldo, fa_factura.vt_Observacion,
				  CASE WHEN DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) < 0  THEN round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) ELSE 0 END AS X_VENCER,
				  CASE WHEN DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) BETWEEN 1 AND 30  THEN round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO30, 
				  CASE WHEN DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) BETWEEN 31 AND 60 THEN round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO60,
				  CASE WHEN DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) BETWEEN 61 AND 90 THEN round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO90,
				  CASE WHEN DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) >= 91 THEN round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO90MAS
FROM     fa_factura INNER JOIN
                  fa_Vendedor ON fa_factura.IdEmpresa = fa_Vendedor.IdEmpresa AND fa_factura.IdVendedor = fa_Vendedor.IdVendedor INNER JOIN
                  fa_cliente ON fa_factura.IdEmpresa = fa_cliente.IdEmpresa AND fa_factura.IdCliente = fa_cliente.IdCliente INNER JOIN
                  tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona INNER JOIN
                  tb_sucursal ON fa_factura.IdEmpresa = tb_sucursal.IdEmpresa AND fa_factura.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
                  fa_factura_resumen ON fa_factura.IdEmpresa = fa_factura_resumen.IdEmpresa AND fa_factura.IdSucursal = fa_factura_resumen.IdSucursal AND fa_factura.IdBodega = fa_factura_resumen.IdBodega AND 
                  fa_factura.IdCbteVta = fa_factura_resumen.IdCbteVta left join
				  (
				  select d.IdEmpresa, d.IdSucursal, d.IdBodega_Cbte, d.IdCbte_vta_nota, d.dc_TipoDocumento, SUM(D.dc_ValorPago)dc_ValorPago
				  from cxc_cobro_det as d inner join cxc_cobro as c
				  on c.IdEmpresa = d.IdEmpresa and c.IdSucursal = d.IdSucursal and c.IdCobro = d.IdCobro
				  where d.IdEmpresa = @IdEmpresa and c.cr_fecha <= @FechaCorte and c.cr_estado = 'A'
				  GROUP BY d.IdEmpresa, d.IdSucursal, d.IdBodega_Cbte, d.IdCbte_vta_nota, d.dc_TipoDocumento
				  ) as Cobro on fa_factura.IdEmpresa = COBRO.IdEmpresa AND fa_factura.IdSucursal = COBRO.IdSucursal AND fa_factura.IdBodega = COBRO.IdBodega_Cbte AND fa_factura.IdCbteVta = COBRO.IdCbte_vta_nota AND fa_factura.vt_tipoDoc = COBRO.dc_TipoDocumento
where fa_factura.IdEmpresa = @IdEmpresa and fa_factura.IdCliente = @IdCliente 
AND fa_factura.Estado = 'A' 
and round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) >  0