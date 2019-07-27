using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Facturacion
{
    public class fa_factura_det_Data
    {
        public List<fa_factura_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                List<fa_factura_det_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_factura_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdCbteVta == IdCbteVta
                             select new fa_factura_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,
                                 IdProducto = q.IdProducto,
                                 vt_cantidad = q.vt_cantidad,
                                 vt_DescUnitario = q.vt_DescUnitario,
                                 vt_PrecioFinal = q.vt_PrecioFinal,
                                 vt_Precio = q.vt_Precio,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_detallexItems = q.vt_detallexItems,
                                 vt_iva = q.vt_iva,
                                 vt_PorDescUnitario = q.vt_PorDescUnitario,
                                 vt_por_iva = q.vt_por_iva,
                                 vt_total = q.vt_total,
                                 IdCentroCosto = q.IdCentroCosto,
                                 IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                                 IdEmpresa_pf = q.IdEmpresa_pf,
                                 IdProforma = q.IdProforma,
                                 IdPunto_Cargo = q.IdPunto_Cargo,
                                 IdPunto_cargo_grupo = q.IdPunto_cargo_grupo,
                                 IdSucursal_pf = q.IdSucursal_pf,
                                 Secuencia = q.Secuencia,
                                 Secuencia_pf = q.Secuencia_pf,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 lote_num_lote = q.lote_num_lote,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 CantidadAnterior = q.vt_cantidad,
                                 tp_manejaInven = q.tp_ManejaInven,
                                 se_distribuye = q.se_distribuye
                                
                             }).ToList();
                }
                Lista.ForEach(V =>
                {
                    V.pr_descripcion = V.pr_descripcion + " " + V.nom_presentacion + " - " + V.lote_num_lote + " - " + (V.lote_fecha_vcto != null ? Convert.ToDateTime(V.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
                });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<fa_factura_det_Info> get_list_proformas_x_facturar(int IdEmpresa, int IdSucursal, decimal IdCliente)
        {
            try
            {
                List<fa_factura_det_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_proforma_det_por_facturar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdCliente == IdCliente
                             select new fa_factura_det_Info
                             {
                                 IdProducto = q.IdProducto,
                                 vt_cantidad = q.pd_cantidad,
                                 vt_DescUnitario = q.pd_descuento_uni,
                                 vt_PrecioFinal = q.pd_precio_final,
                                 vt_Precio = q.pd_precio,
                                 vt_Subtotal = q.pd_subtotal,
                                 vt_iva = q.pd_iva,
                                 vt_PorDescUnitario = q.pd_por_descuento_uni,
                                 vt_por_iva = q.pd_por_iva,
                                 vt_total = q.pd_total,
                                 IdCod_Impuesto_Iva = q.IdCod_Impuesto,
                                 IdEmpresa_pf = q.IdEmpresa,
                                 IdProforma = q.IdProforma,
                                 IdSucursal_pf = q.IdSucursal,
                                 Secuencia_pf = q.Secuencia,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 lote_num_lote = q.lote_num_lote,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 tp_manejaInven = q.tp_ManejaInven,
                                 se_distribuye = q.se_distribuye
                             }).ToList();
                }
                Lista.ForEach(V =>
                {
                    V.pr_descripcion = V.pr_descripcion + " " + V.nom_presentacion + " - " + V.lote_num_lote + " - " + (V.lote_fecha_vcto != null ? Convert.ToDateTime(V.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
                    V.secuencial = Convert.ToInt32(V.IdEmpresa_pf).ToString("00") + Convert.ToInt32(V.IdSucursal_pf).ToString("00") + Convert.ToInt32(V.IdProforma).ToString("000000") + Convert.ToInt32(V.Secuencia_pf).ToString("00");
                });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<fa_factura_det_Info> get_list_proforma(int IdEmpresa, int IdSucursal, decimal IdCliente, decimal IdProforma)
        {
            try
            {
                List<fa_factura_det_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_proforma_det_por_facturar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdCliente == IdCliente
                             && q.IdProforma == IdProforma
                             select new fa_factura_det_Info
                             {
                                 IdProducto = q.IdProducto,
                                 vt_cantidad = q.pd_cantidad,
                                 vt_DescUnitario = q.pd_descuento_uni,
                                 vt_PrecioFinal = q.pd_precio_final,
                                 vt_Precio = q.pd_precio,
                                 vt_Subtotal = q.pd_subtotal,
                                 vt_iva = q.pd_iva,
                                 vt_PorDescUnitario = q.pd_por_descuento_uni,
                                 vt_por_iva = q.pd_por_iva,
                                 vt_total = q.pd_total,
                                 IdCod_Impuesto_Iva = q.IdCod_Impuesto,
                                 IdEmpresa_pf = q.IdEmpresa,
                                 IdProforma = q.IdProforma,
                                 IdSucursal_pf = q.IdSucursal,
                                 Secuencia_pf = q.Secuencia,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 lote_num_lote = q.lote_num_lote,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 tp_manejaInven = q.tp_ManejaInven,
                                 se_distribuye = q.se_distribuye
                             }).ToList();
                }
                Lista.ForEach(V =>
                {
                    V.pr_descripcion = V.pr_descripcion + " " + V.nom_presentacion + " - " + V.lote_num_lote + " - " + (V.lote_fecha_vcto != null ? Convert.ToDateTime(V.lote_fecha_vcto).ToString("dd/MM/yyyy") : "");
                    V.secuencial = Convert.ToInt32(V.IdEmpresa_pf).ToString("00") + Convert.ToInt32(V.IdSucursal_pf).ToString("00") + Convert.ToInt32(V.IdProforma).ToString("000000") + Convert.ToInt32(V.Secuencia_pf).ToString("00");
                });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
