create view vwfa_notaCreDeb_x_Valores_Aplicados
as
SELECT        IdEmpresa_nt, IdSucursal_nt, IdBodega_nt, IdNota_nt, secuencia, SUM(Valor_Aplicado) AS Valor_Aplicado
FROM            fa_notaCreDeb_x_fa_factura_NotaDeb
GROUP BY IdEmpresa_nt, IdSucursal_nt, IdBodega_nt, IdNota_nt, secuencia