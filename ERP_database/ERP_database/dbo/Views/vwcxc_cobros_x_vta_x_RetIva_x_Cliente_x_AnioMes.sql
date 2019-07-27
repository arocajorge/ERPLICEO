CREATE VIEW [dbo].[vwcxc_cobros_x_vta_x_RetIva_x_Cliente_x_AnioMes]
AS
SELECT A.IdEmpresa,A.pe_cedulaRuc,SUM(A.Valor_ret) Valor_ret, A.IdAnioRT, A.IdMes FROM(
SELECT        dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa, dbo.tb_persona.pe_cedulaRuc, SUM(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.dc_ValorPago) AS Valor_ret, 
                         YEAR(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu) AS IdAnioRT, MONTH(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu) AS IdMes
FROM            dbo.vwcxc_cobros_x_vta_nota_x_RetIVA INNER JOIN
                         dbo.fa_factura ON dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa = dbo.fa_factura.IdEmpresa AND 
                         dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdSucursal = dbo.fa_factura.IdSucursal AND 
                         dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdBodega_Cbte = dbo.fa_factura.IdBodega AND 
                         dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdCbte_vta_nota = dbo.fa_factura.IdCbteVta INNER JOIN
                         dbo.fa_cliente ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona
WHERE        (dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.dc_TipoDocumento = 'FACT')
GROUP BY dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa, dbo.tb_persona.pe_cedulaRuc, YEAR(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu), 
                         MONTH(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu)
UNION

SELECT        dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa, dbo.tb_persona.pe_cedulaRuc, SUM(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.dc_ValorPago) AS Valor_ret, 
                         YEAR(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu) AS IdAnioRT, MONTH(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu) AS IdMes
FROM            dbo.tb_persona INNER JOIN
                         dbo.fa_cliente ON dbo.tb_persona.IdPersona = dbo.fa_cliente.IdPersona INNER JOIN
                         dbo.fa_notaCreDeb ON dbo.fa_cliente.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.fa_cliente.IdCliente = dbo.fa_notaCreDeb.IdCliente INNER JOIN
                         dbo.vwcxc_cobros_x_vta_nota_x_RetIVA ON dbo.fa_notaCreDeb.IdEmpresa = dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa AND 
                         dbo.fa_notaCreDeb.IdSucursal = dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdSucursal AND 
                         dbo.fa_notaCreDeb.IdBodega = dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdBodega_Cbte AND 
                         dbo.fa_notaCreDeb.IdNota = dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdCbte_vta_nota AND 
                         dbo.fa_notaCreDeb.CodDocumentoTipo = dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.dc_TipoDocumento
GROUP BY dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.IdEmpresa, dbo.tb_persona.pe_cedulaRuc, YEAR(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu), 
                         MONTH(dbo.vwcxc_cobros_x_vta_nota_x_RetIVA.cr_fechaDocu)) A
GROUP BY A.IdEmpresa,A.pe_cedulaRuc,A.IdAnioRT, A.IdMes