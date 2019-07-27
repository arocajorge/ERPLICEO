CREATE VIEW [dbo].[vwFa_Documentos_Relacionados_NC_ND] AS
SELECT        carCobra.IdEmpresa, carCobra.IdSucursal, cobro.IdCobro, nota_x_cobro.IdBodega_nt AS IdBodega, carCobra.vt_tipoDoc, carCobra.vt_NunDocumento, 
                         carCobra.Referencia, carCobra.IdComprobante, carCobra.CodComprobante, carCobra.Su_Descripcion, carCobra.IdCliente, carCobra.vt_fecha, carCobra.vt_total, 
                         carCobra.Saldo AS Saldo, cobroDet.dc_ValorPago AS TotalxCobrado, carCobra.Bodega, carCobra.vt_Subtotal, carCobra.vt_iva, carCobra.vt_fech_venc, 
                         carCobra.dc_ValorRetFu, carCobra.dc_ValorRetIva, carCobra.CodCliente, carCobra.NomCliente, carCobra.em_nombre,
						 nota_x_cobro.IdEmpresa_nt, nota_x_cobro.IdSucursal_nt, nota_x_cobro.IdBodega_nt, nota_x_cobro.IdNota_nt
FROM            fa_notaCreDeb_x_cxc_cobro nota_x_cobro INNER JOIN
						 dbo.cxc_cobro cobro ON nota_x_cobro.IdEmpresa_cbr = cobro.IdEmpresa AND nota_x_cobro.IdSucursal_cbr = cobro.IdSucursal AND
						 nota_x_cobro.IdCobro_cbr = cobro.IdCobro INNER JOIN 
						 dbo.cxc_cobro_det AS cobroDet ON cobro.IdEmpresa = cobroDet.IdEmpresa AND cobro.IdSucursal = cobroDet.IdSucursal AND cobro.IdCobro = cobroDet.IdCobro INNER JOIN
						 dbo.vwFa_Documentos_x_Relacionar_NC_ND AS carCobra ON carCobra.IdEmpresa = cobroDet.IdEmpresa AND carCobra.IdSucursal = cobroDet.IdSucursal AND 
                         carCobra.IdBodega = nota_x_cobro.IdBodega_nt AND carCobra.IdComprobante = cobroDet.IdCbte_vta_nota AND carCobra.vt_tipoDoc = cobroDet.dc_TipoDocumento