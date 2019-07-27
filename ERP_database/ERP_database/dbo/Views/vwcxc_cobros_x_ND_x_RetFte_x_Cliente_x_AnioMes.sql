


CREATE view [dbo].[vwcxc_cobros_x_ND_x_RetFte_x_Cliente_x_AnioMes] as

SELECT        vwcxc_cobros_x_vta_nota_x_RetFuente.IdEmpresa, tb_persona.pe_cedulaRuc, SUM(vwcxc_cobros_x_vta_nota_x_RetFuente.dc_ValorPago) AS Valor_ret, 
                         YEAR(vwcxc_cobros_x_vta_nota_x_RetFuente.cr_fechaDocu) AS IdAnioRT, MONTH(vwcxc_cobros_x_vta_nota_x_RetFuente.cr_fechaDocu) AS IdMes
FROM            fa_cliente INNER JOIN
                         tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona INNER JOIN
                         fa_notaCreDeb ON fa_cliente.IdEmpresa = fa_notaCreDeb.IdEmpresa AND fa_cliente.IdCliente = fa_notaCreDeb.IdCliente INNER JOIN
                         vwcxc_cobros_x_vta_nota_x_RetFuente ON fa_notaCreDeb.IdEmpresa = vwcxc_cobros_x_vta_nota_x_RetFuente.IdEmpresa AND 
                         fa_notaCreDeb.IdSucursal = vwcxc_cobros_x_vta_nota_x_RetFuente.IdSucursal AND fa_notaCreDeb.IdBodega = vwcxc_cobros_x_vta_nota_x_RetFuente.IdBodega_Cbte AND 
                         fa_notaCreDeb.IdNota = vwcxc_cobros_x_vta_nota_x_RetFuente.IdCbte_vta_nota
WHERE        (vwcxc_cobros_x_vta_nota_x_RetFuente.dc_TipoDocumento = 'NTDB')
GROUP BY vwcxc_cobros_x_vta_nota_x_RetFuente.IdEmpresa, tb_persona.pe_cedulaRuc, YEAR(vwcxc_cobros_x_vta_nota_x_RetFuente.cr_fechaDocu), MONTH(vwcxc_cobros_x_vta_nota_x_RetFuente.cr_fechaDocu)