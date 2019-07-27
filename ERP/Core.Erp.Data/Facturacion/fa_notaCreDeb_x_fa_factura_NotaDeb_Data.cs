using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_notaCreDeb_x_fa_factura_NotaDeb_Data
    {

        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> get_list_cartera(int IdEmpresa, int IdSucursal, decimal IdCliente, bool mostrar_saldo0)
        {
            try
            {
                List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> Lista;

                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    if(!mostrar_saldo0)
                    Lista = (from q in Context.vwcxc_cartera_x_cobrar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdCliente == IdCliente
                             && q.Saldo > 0
                             && q.Estado == "A"
                             select new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                             {
                                 IdEmpresa_fac_nd_doc_mod = q.IdEmpresa,
                                 IdSucursal_fac_nd_doc_mod = q.IdSucursal,
                                 IdBodega_fac_nd_doc_mod = q.IdBodega,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 IdCbteVta_fac_nd_doc_mod = q.IdComprobante,
                                 vt_NumDocumento = q.vt_NunDocumento,
                                 Observacion = q.Referencia,                                 
                                 vt_fecha = q.vt_fecha,
                                 vt_total = q.vt_total,
                                 Saldo = q.Saldo,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_iva = q.vt_iva,
                                 NumDocumento = q.vt_NunDocumento
                             }).ToList();
                    else
                        Lista = (from q in Context.vwcxc_cartera_x_cobrar
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdCliente == IdCliente
                                 && q.Estado == "A"
                                 select new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                                 {
                                     IdEmpresa_fac_nd_doc_mod = q.IdEmpresa,
                                     IdSucursal_fac_nd_doc_mod = q.IdSucursal,
                                     IdBodega_fac_nd_doc_mod = q.IdBodega,
                                     vt_tipoDoc = q.vt_tipoDoc,
                                     IdCbteVta_fac_nd_doc_mod = q.IdComprobante,
                                     vt_NumDocumento = q.vt_NunDocumento,
                                     Observacion = q.Referencia,
                                     vt_fecha = q.vt_fecha,
                                     vt_total = q.vt_total,
                                     Saldo = q.Saldo,
                                     vt_Subtotal = q.vt_Subtotal,
                                     vt_iva = q.vt_iva,
                                     NumDocumento = q.vt_NunDocumento
                                 }).ToList();

                    Lista.ForEach(q => { q.secuencial = q.vt_tipoDoc + "-" + q.IdBodega_fac_nd_doc_mod.ToString() + "-" + q.IdCbteVta_fac_nd_doc_mod.ToString(); q.Valor_Aplicado = Convert.ToDouble(q.Saldo); });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> Lista;

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_notaCreDeb_x_fa_factura_NotaDeb
                             where q.IdEmpresa_nt == IdEmpresa
                             && q.IdSucursal_nt == IdSucursal
                             && q.IdBodega_nt == IdBodega
                             && q.IdNota_nt == IdNota
                             select new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                             {
                                 IdEmpresa_fac_nd_doc_mod = q.IdEmpresa_nt,
                                 IdSucursal_fac_nd_doc_mod = q.IdSucursal_fac_nd_doc_mod,
                                 IdBodega_fac_nd_doc_mod = q.IdBodega_fac_nd_doc_mod,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 IdCbteVta_fac_nd_doc_mod = q.IdCbteVta_fac_nd_doc_mod,
                                 vt_NumDocumento = q.vt_NumFactura,
                                 Observacion = q.vt_Observacion,
                                 vt_fecha = q.vt_fecha,
                                 vt_total = q.vt_total,
                                 Saldo = q.saldo_sin_cobro,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_iva = q.vt_iva,
                                 Saldo_final = q.saldo,
                                 seleccionado = true,
                                 Valor_Aplicado = q.Valor_Aplicado,
                                 NumDocumento = null
                             }).ToList();
                }
                Lista.ForEach(q => { q.secuencial = q.vt_tipoDoc + "-" + q.IdBodega_fac_nd_doc_mod.ToString() + "-" + q.IdCbteVta_fac_nd_doc_mod.ToString(); });
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
