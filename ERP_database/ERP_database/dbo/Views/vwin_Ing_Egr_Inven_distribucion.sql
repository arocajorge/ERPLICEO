CREATE view [dbo].[vwin_Ing_Egr_Inven_distribucion]
as
SELECT ISNULL(ROW_NUMBER() OVER (ORDER BY x_distribuir.IdEmpresa), 0) IdRow,x_distribuir.IdEmpresa, x_distribuir.IdSucursal, x_distribuir.IdMovi_inven_tipo, x_distribuir.IdNumMovi, x_distribuir.IdBodega, ISNULL(ABS(x_distribuir.dm_cantidad), 0) AS can_total, ABS(ISNULL(dis.dm_cantidad, 0)) AS can_distribuida, 
ISNULL(ABS(x_distribuir.dm_cantidad), 0) - ABS(ISNULL(dis.dm_cantidad, 0)) AS can_x_distribuir,
                  dbo.in_Ing_Egr_Inven.signo, dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.cm_fecha, dbo.in_movi_inven_tipo.tm_descripcion, dbo.tb_sucursal.Su_Descripcion, dbo.fa_factura.vt_NumFactura, 
                  dbo.tb_persona.pe_nombreCompleto
FROM     dbo.in_Ing_Egr_Inven_distribucion RIGHT OUTER JOIN
                  dbo.fa_factura INNER JOIN
                  dbo.fa_factura_x_in_Ing_Egr_Inven ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_x_in_Ing_Egr_Inven.IdEmpresa_fa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_x_in_Ing_Egr_Inven.IdSucursal_fa AND 
                  dbo.fa_factura.IdBodega = dbo.fa_factura_x_in_Ing_Egr_Inven.IdBodega_fa AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_x_in_Ing_Egr_Inven.IdCbteVta_fa INNER JOIN
                  dbo.fa_cliente ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                  dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona RIGHT OUTER JOIN
                  dbo.in_Producto INNER JOIN
                  dbo.in_Ing_Egr_Inven_det AS x_distribuir ON dbo.in_Producto.IdEmpresa = x_distribuir.IdEmpresa AND dbo.in_Producto.IdProducto = x_distribuir.IdProducto INNER JOIN
                  dbo.in_Ing_Egr_Inven ON x_distribuir.IdNumMovi = dbo.in_Ing_Egr_Inven.IdNumMovi AND x_distribuir.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo AND x_distribuir.IdSucursal = dbo.in_Ing_Egr_Inven.IdSucursal AND 
                  x_distribuir.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa INNER JOIN
                  dbo.in_movi_inven_tipo ON x_distribuir.IdEmpresa = dbo.in_movi_inven_tipo.IdEmpresa AND x_distribuir.IdMovi_inven_tipo = dbo.in_movi_inven_tipo.IdMovi_inven_tipo INNER JOIN
                  dbo.tb_sucursal ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.tb_sucursal.IdSucursal ON 
                  dbo.fa_factura_x_in_Ing_Egr_Inven.IdEmpresa_in_eg_x_inv = dbo.in_Ing_Egr_Inven.IdEmpresa AND dbo.fa_factura_x_in_Ing_Egr_Inven.IdSucursal_in_eg_x_inv = dbo.in_Ing_Egr_Inven.IdSucursal AND 
                  dbo.fa_factura_x_in_Ing_Egr_Inven.IdMovi_inven_tipo_in_eg_x_inv = dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo AND dbo.fa_factura_x_in_Ing_Egr_Inven.IdNumMovi_in_eg_x_inv = dbo.in_Ing_Egr_Inven.IdNumMovi ON 
                  dbo.in_Ing_Egr_Inven_distribucion.IdNumMovi = dbo.in_Ing_Egr_Inven.IdNumMovi AND dbo.in_Ing_Egr_Inven_distribucion.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo AND 
                  dbo.in_Ing_Egr_Inven_distribucion.IdSucursal = dbo.in_Ing_Egr_Inven.IdSucursal AND dbo.in_Ing_Egr_Inven_distribucion.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa LEFT OUTER JOIN
                      (SELECT a.IdEmpresa, a.IdSucursal, a.IdMovi_inven_tipo, a.IdNumMovi, SUM(c.dm_cantidad) AS dm_cantidad
                       FROM      dbo.in_Ing_Egr_Inven_det AS c INNER JOIN
                                         dbo.in_Ing_Egr_Inven AS b ON c.IdEmpresa = b.IdEmpresa AND c.IdSucursal = b.IdSucursal AND c.IdMovi_inven_tipo = b.IdMovi_inven_tipo AND c.IdNumMovi = b.IdNumMovi INNER JOIN
                                         dbo.in_Ing_Egr_Inven_distribucion AS a ON b.IdEmpresa = a.IdEmpresa_dis AND b.IdSucursal = a.IdSucursal_dis AND b.IdMovi_inven_tipo = a.IdMovi_inven_tipo_dis AND b.IdNumMovi = a.IdNumMovi_dis INNER JOIN
                                         dbo.in_Ing_Egr_Inven AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdSucursal = d.IdSucursal AND a.IdMovi_inven_tipo = d.IdMovi_inven_tipo AND a.IdNumMovi = d.IdNumMovi AND a.signo <> d.signo
                       GROUP BY a.IdEmpresa, a.IdSucursal, a.IdMovi_inven_tipo, a.IdNumMovi) AS dis ON x_distribuir.IdEmpresa = dis.IdEmpresa AND x_distribuir.IdSucursal = dis.IdSucursal AND x_distribuir.IdMovi_inven_tipo = dis.IdMovi_inven_tipo AND 
                  x_distribuir.IdNumMovi = dis.IdNumMovi
WHERE  (dbo.in_Producto.se_distribuye = 1) AND (dbo.in_Ing_Egr_Inven.Estado = 'A') AND (NOT EXISTS
                      (SELECT IdEmpresa_dis
                       FROM      dbo.in_Ing_Egr_Inven_distribucion AS distri
                       WHERE   (IdEmpresa_dis = x_distribuir.IdEmpresa) AND (IdSucursal_dis = x_distribuir.IdSucursal) AND (IdMovi_inven_tipo_dis = x_distribuir.IdMovi_inven_tipo) AND (IdNumMovi_dis = x_distribuir.IdNumMovi)))
GROUP BY x_distribuir.IdEmpresa, x_distribuir.IdSucursal, x_distribuir.IdMovi_inven_tipo, x_distribuir.IdNumMovi, x_distribuir.dm_cantidad, dbo.in_Ing_Egr_Inven.signo, dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.cm_fecha, 
                  dis.dm_cantidad, dbo.in_movi_inven_tipo.tm_descripcion, x_distribuir.IdBodega, dbo.tb_sucursal.Su_Descripcion, dbo.fa_factura.vt_NumFactura, dbo.tb_persona.pe_nombreCompleto
