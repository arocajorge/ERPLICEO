
CREATE view [dbo].[vwcxc_cobros_x_ND_x_RetIva_x_Cliente_x_AnioMes] as

SELECT        vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa, tb_persona.pe_cedulaRuc, SUM(vwcxc_cobros_x_vta_nota_x_RetIVA.dc_ValorPago) AS Valor_ret, YEAR(vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu) 
                         AS IdAnioRT, MONTH(vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu) AS IdMes
FROM            vwcxc_cobros_x_vta_nota_x_RetIVA INNER JOIN
                         fa_notaCreDeb INNER JOIN
                         tb_persona INNER JOIN
                         fa_cliente ON tb_persona.IdPersona = fa_cliente.IdPersona ON fa_notaCreDeb.IdEmpresa = fa_cliente.IdEmpresa AND fa_notaCreDeb.IdCliente = fa_cliente.IdCliente ON 
                         vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa = fa_notaCreDeb.IdEmpresa AND vwcxc_cobros_x_vta_nota_x_RetIVA.IdSucursal = fa_notaCreDeb.IdSucursal AND 
                         vwcxc_cobros_x_vta_nota_x_RetIVA.IdBodega_Cbte = fa_notaCreDeb.IdBodega AND vwcxc_cobros_x_vta_nota_x_RetIVA.IdCbte_vta_nota = fa_notaCreDeb.IdNota
WHERE        (vwcxc_cobros_x_vta_nota_x_RetIVA.dc_TipoDocumento = 'NTDB')
GROUP BY vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa, tb_persona.pe_cedulaRuc, YEAR(vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu), MONTH(vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu)