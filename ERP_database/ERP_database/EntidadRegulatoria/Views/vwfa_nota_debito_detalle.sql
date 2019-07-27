
CREATE VIEW [EntidadRegulatoria].[vwfa_nota_debito_detalle]
AS
SELECT        dbo.fa_notaCreDeb_det.IdEmpresa, dbo.fa_notaCreDeb_det.IdSucursal, dbo.fa_notaCreDeb_det.IdBodega, dbo.fa_notaCreDeb_det.IdNota, dbo.fa_notaCreDeb_det.Secuencia, dbo.fa_notaCreDeb_det.IdProducto, 
                         dbo.fa_notaCreDeb_det.sc_cantidad, dbo.fa_notaCreDeb_det.sc_Precio, dbo.fa_notaCreDeb_det.sc_descUni, dbo.fa_notaCreDeb_det.sc_PordescUni, dbo.fa_notaCreDeb_det.sc_precioFinal, dbo.fa_notaCreDeb_det.sc_subtotal, 
                         dbo.fa_notaCreDeb_det.sc_iva, dbo.fa_notaCreDeb_det.sc_total, dbo.fa_notaCreDeb_det.sc_costo, dbo.fa_notaCreDeb_det.sc_observacion, dbo.fa_notaCreDeb_det.vt_por_iva, dbo.in_Producto.pr_codigo, 
                         dbo.in_Producto.pr_codigo2, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_descripcion_2
FROM            dbo.fa_notaCreDeb_det INNER JOIN
                         dbo.in_Producto ON dbo.fa_notaCreDeb_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.fa_notaCreDeb_det.IdProducto = dbo.in_Producto.IdProducto