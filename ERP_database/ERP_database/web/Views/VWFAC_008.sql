CREATE VIEW [web].[VWFAC_008]
AS
SELECT fa_notaCreDeb_det.IdEmpresa, fa_notaCreDeb_det.IdSucursal, fa_notaCreDeb_det.IdBodega, fa_notaCreDeb_det.IdNota, fa_notaCreDeb_det.Secuencia, fa_notaCreDeb_det.IdProducto, in_Producto.pr_descripcion, 
                  in_presentacion.nom_presentacion, in_Producto.lote_num_lote, in_Producto.lote_fecha_vcto, fa_notaCreDeb_det.sc_cantidad, fa_notaCreDeb_det.sc_Precio, fa_notaCreDeb_det.sc_descUni, fa_notaCreDeb_det.sc_PordescUni, 
                  fa_notaCreDeb_det.sc_precioFinal, fa_notaCreDeb_det.sc_descUni * fa_notaCreDeb_det.sc_cantidad AS DescTotal, fa_notaCreDeb_det.sc_subtotal, iif(fa_notaCreDeb_det.vt_por_iva > 0, fa_notaCreDeb_det.sc_subtotal, 0) 
                  AS sc_subtotalIVA, iif(fa_notaCreDeb_det.vt_por_iva = 0, fa_notaCreDeb_det.sc_subtotal, 0) AS sc_subtotal0, fa_notaCreDeb_det.sc_iva, fa_notaCreDeb_det.vt_por_iva, fa_cliente_contactos.Nombres, Su_Descripcion, 
                  fa_notaCreDeb.Serie1 + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa AS NumNota_Impresa, fa_notaCreDeb.no_fecha, fa_notaCreDeb.no_fecha_venc, fa_notaCreDeb.CreDeb, fa_notaCreDeb.sc_observacion,
				  fa_notaCreDeb_det.sc_total, tipo.No_Descripcion
FROM     in_presentacion INNER JOIN
                  in_Producto ON in_presentacion.IdEmpresa = in_Producto.IdEmpresa AND in_presentacion.IdPresentacion = in_Producto.IdPresentacion INNER JOIN
                  fa_notaCreDeb INNER JOIN
                  fa_notaCreDeb_det ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND fa_notaCreDeb.IdSucursal = fa_notaCreDeb_det.IdSucursal AND fa_notaCreDeb.IdBodega = fa_notaCreDeb_det.IdBodega AND 
                  fa_notaCreDeb.IdNota = fa_notaCreDeb_det.IdNota ON in_Producto.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND in_Producto.IdProducto = fa_notaCreDeb_det.IdProducto INNER JOIN
                  fa_cliente_contactos ON fa_notaCreDeb.IdEmpresa = fa_cliente_contactos.IdEmpresa AND fa_notaCreDeb.IdCliente = fa_cliente_contactos.IdCliente  INNER JOIN
                  tb_sucursal ON fa_notaCreDeb.IdEmpresa = tb_sucursal.IdEmpresa AND fa_notaCreDeb.IdSucursal = tb_sucursal.IdSucursal inner join fa_TipoNota as tipo on fa_notaCreDeb.IdTipoNota = tipo.IdTipoNota