--EXEC web.SPFAC_019 2,0,999999,0,99999,'2019/07/02',0,0,'admin'
CREATE PROCEDURE [web].[SPFAC_019]
(
@IdEmpresa int,
@IdClienteIni numeric,
@IdClienteFin numeric,
@IdVendedorIni int,
@IdVendedorFin int,
@FechaCorte date,
@MostrarSoloVencido bit,
@MostrarSaldo0 bit,
@IdUsuario varchar(50)
)
AS

SELECT fa_factura.IdEmpresa, fa_factura.IdSucursal, fa_factura.IdBodega, fa_factura.IdCbteVta, fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura AS NumDocumento, fa_factura.vt_fecha, fa_factura.vt_fech_venc, 
                  tb_sucursal.Su_Descripcion, fa_factura.IdCliente, tb_persona.pe_nombreCompleto, tb_persona.pe_cedulaRuc, fa_factura.IdVendedor, fa_Vendedor.Ve_Vendedor, fa_factura_resumen.Total, CASE WHEN DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) < 0 THEN 0 ELSE DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) END DiasVencido,
				  isnull(COBRO.dc_ValorPago,0) TotalCobrado, round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) as Saldo, ISNULL(CobroRet.dc_ValorPago,0) AS ValorRetencion
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
				  on c.IdEmpresa = d.IdEmpresa and c.IdSucursal = d.IdSucursal and c.IdCobro = d.IdCobro inner join web.tb_FiltroReportes as f on c.IdEmpresa = f.IdEmpresa and c.IdSucursal = f.IdSucursal and f.IdUsuario = @IdUsuario
				  where d.IdEmpresa = @IdEmpresa and c.cr_fecha <= @FechaCorte and c.cr_estado = 'A'
				  GROUP BY d.IdEmpresa, d.IdSucursal, d.IdBodega_Cbte, d.IdCbte_vta_nota, d.dc_TipoDocumento
				  ) as Cobro on fa_factura.IdEmpresa = COBRO.IdEmpresa AND fa_factura.IdSucursal = COBRO.IdSucursal AND fa_factura.IdBodega = COBRO.IdBodega_Cbte AND fa_factura.IdCbteVta = COBRO.IdCbte_vta_nota AND fa_factura.vt_tipoDoc = COBRO.dc_TipoDocumento
				  left join
				  (
				  select d.IdEmpresa, d.IdSucursal, d.IdBodega_Cbte, d.IdCbte_vta_nota, d.dc_TipoDocumento, SUM(D.dc_ValorPago)dc_ValorPago
				  from cxc_cobro_det as d inner join cxc_cobro as c
				  on c.IdEmpresa = d.IdEmpresa and c.IdSucursal = d.IdSucursal and c.IdCobro = d.IdCobro inner join web.tb_FiltroReportes as f on c.IdEmpresa = f.IdEmpresa and c.IdSucursal = f.IdSucursal and f.IdUsuario = @IdUsuario inner join
				  cxc_cobro_tipo as t on t.IdCobro_tipo = c.IdCobro_tipo
				  where d.IdEmpresa = @IdEmpresa and c.cr_fecha <= @FechaCorte and c.cr_estado = 'A' and t.IdMotivo_tipo_cobro = 'RET'
				  GROUP BY d.IdEmpresa, d.IdSucursal, d.IdBodega_Cbte, d.IdCbte_vta_nota, d.dc_TipoDocumento
				  ) as CobroRet on fa_factura.IdEmpresa = CobroRet.IdEmpresa AND fa_factura.IdSucursal = CobroRet.IdSucursal AND fa_factura.IdBodega = CobroRet.IdBodega_Cbte AND fa_factura.IdCbteVta = CobroRet.IdCbte_vta_nota AND fa_factura.vt_tipoDoc = CobroRet.dc_TipoDocumento
				  inner join web.tb_FiltroReportes as f on fa_factura.IdEmpresa = f.IdEmpresa and fa_factura.IdSucursal = f.IdSucursal and f.IdUsuario = @IdUsuario
where fa_factura.IdEmpresa = @IdEmpresa and fa_factura.IdCliente between @IdClienteIni and @IdClienteFin and fa_factura.IdVendedor between @IdVendedorIni and @IdVendedorFin
AND fa_factura.Estado = 'A' AND CASE WHEN DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) < 0 THEN 0 ELSE DATEDIFF(DAY,fa_factura.vt_fech_venc,@FechaCorte) END > case when @MostrarSoloVencido = 1 then 0 else -1 end 
and round(fa_factura_resumen.Total - isnull(COBRO.dc_ValorPago,0),2) > case when @MostrarSaldo0 = 1 then -1 else 0 end