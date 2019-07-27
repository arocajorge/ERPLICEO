create view web.vwfa_factura_sin_guia as
SELECT        f.IdEmpresa, f.IdSucursal, f.IdBodega, f.IdCbteVta, f.CodCbteVta,f.IdCliente, f.vt_tipoDoc, f.vt_serie1, f.vt_serie2, f.vt_NumFactura, f.vt_Observacion,f.vt_fecha
FROM            dbo.fa_factura AS f INNER JOIN
                         dbo.fa_factura_det AS f_det ON f.IdEmpresa = f_det.IdEmpresa AND f.IdSucursal = f_det.IdSucursal AND f.IdBodega = f_det.IdBodega AND f.IdCbteVta = f_det.IdCbteVta
where not exists (select * from fa_guia_remision_det_x_factura
g 
where g.IdEmpresa_fact=f_det.IdEmpresa
and g.IdSucursal_fact=f.IdSucursal
and g.IdBodega_fact=f.IdBodega
and g.IdCbteVta_fact=f.IdCbteVta
and g.Secuencia_fact=f_det.Secuencia_pf)
and f.Estado='A'
group by 
 f.IdEmpresa, f.IdSucursal, f.IdBodega, f.IdCbteVta, f.CodCbteVta,f.IdCliente, f.vt_tipoDoc, f.vt_serie1, f.vt_serie2, f.vt_NumFactura, f.vt_Observacion,f.vt_fecha