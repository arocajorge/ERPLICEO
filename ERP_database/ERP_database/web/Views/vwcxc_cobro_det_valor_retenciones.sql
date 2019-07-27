CREATE VIEW web.vwcxc_cobro_det_valor_retenciones
AS
SELECT RET.IdEmpresa, RET.IdSucursal, RET.IdBodega_Cbte, RET.IdCbte_vta_nota, RET.dc_TipoDocumento, sum(ret.ValorRteFTE) ValorRteFTE, sum(ret.ValorRteIVA) ValorRteIVA, max(PorcentajeRetFTE) PorcentajeRetFTE, MAX(PorcentajeRetIVA) PorcentajeRetIVA, MAX(cr_fecha) cr_fecha, sum(ret.ValorRteFTE) + sum(ret.ValorRteIVA) TotalRTE
FROM(
SELECT cxc_cobro_det.IdEmpresa, cxc_cobro_det.IdSucursal, cxc_cobro_det.IdBodega_Cbte, cxc_cobro_det.IdCbte_vta_nota, cxc_cobro_det.dc_TipoDocumento, 0 ValorRteFTE, SUM(cxc_cobro_det.dc_ValorPago) AS ValorRteIVA, 'N' ESRetenFTE,  cxc_cobro_tipo.ESRetenIVA, 
                  cxc_cobro.cr_fecha, 0 PorcentajeRetFTE, cxc_cobro_tipo.PorcentajeRet PorcentajeRetIVA
FROM     cxc_cobro_det INNER JOIN
                  cxc_cobro_tipo ON cxc_cobro_det.IdCobro_tipo = cxc_cobro_tipo.IdCobro_tipo INNER JOIN
                  cxc_cobro_tipo_motivo ON cxc_cobro_tipo.IdMotivo_tipo_cobro = cxc_cobro_tipo_motivo.IdMotivo_tipo_cobro INNER JOIN
                  cxc_cobro ON cxc_cobro_det.IdEmpresa = cxc_cobro.IdEmpresa AND cxc_cobro_det.IdSucursal = cxc_cobro.IdSucursal AND cxc_cobro_det.IdCobro = cxc_cobro.IdCobro
WHERE  (cxc_cobro_tipo.ESRetenIVA = 'S')
GROUP BY cxc_cobro_det.IdEmpresa, cxc_cobro_det.IdSucursal, cxc_cobro_det.IdBodega_Cbte, cxc_cobro_det.IdCbte_vta_nota, cxc_cobro_det.dc_TipoDocumento, cxc_cobro_tipo.ESRetenIVA, cxc_cobro.cr_fecha, cxc_cobro_tipo.PorcentajeRet
UNION ALL 
SELECT cxc_cobro_det.IdEmpresa, cxc_cobro_det.IdSucursal, cxc_cobro_det.IdBodega_Cbte, cxc_cobro_det.IdCbte_vta_nota, cxc_cobro_det.dc_TipoDocumento, SUM(cxc_cobro_det.dc_ValorPago) AS ValorRteFTE, 0 , cxc_cobro_tipo.ESRetenFTE, 'N' ESRetenIVA, 
                  cxc_cobro.cr_fecha, cxc_cobro_tipo.PorcentajeRet PorcentajeRetFTE, 0
FROM     cxc_cobro_det INNER JOIN
                  cxc_cobro_tipo ON cxc_cobro_det.IdCobro_tipo = cxc_cobro_tipo.IdCobro_tipo INNER JOIN
                  cxc_cobro_tipo_motivo ON cxc_cobro_tipo.IdMotivo_tipo_cobro = cxc_cobro_tipo_motivo.IdMotivo_tipo_cobro INNER JOIN
                  cxc_cobro ON cxc_cobro_det.IdEmpresa = cxc_cobro.IdEmpresa AND cxc_cobro_det.IdSucursal = cxc_cobro.IdSucursal AND cxc_cobro_det.IdCobro = cxc_cobro.IdCobro
WHERE  (cxc_cobro_tipo.ESRetenFTE = 'S')
GROUP BY cxc_cobro_det.IdEmpresa, cxc_cobro_det.IdSucursal, cxc_cobro_det.IdBodega_Cbte, cxc_cobro_det.IdCbte_vta_nota, cxc_cobro_det.dc_TipoDocumento, cxc_cobro_tipo.ESRetenFTE, cxc_cobro.cr_fecha, cxc_cobro_tipo.PorcentajeRet
) AS RET group by RET.IdEmpresa, RET.IdSucursal, RET.IdBodega_Cbte, RET.IdCbte_vta_nota, RET.dc_TipoDocumento