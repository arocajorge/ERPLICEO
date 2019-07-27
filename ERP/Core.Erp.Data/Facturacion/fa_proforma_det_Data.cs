using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
   public class fa_proforma_det_Data
    {
        public List<fa_proforma_det_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdProforma)
        {
            try
            {
                List<fa_proforma_det_Info> Lista;

                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    Lista = (from q in Context.vwfa_proforma_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdProforma == IdProforma
                             select new fa_proforma_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdProforma = q.IdProforma,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 pd_cantidad = q.pd_cantidad,
                                 pd_precio = q.pd_precio,
                                 pd_por_descuento_uni = q.pd_por_descuento_uni,
                                 pd_descuento_uni = q.pd_descuento_uni,
                                 pd_precio_final = q.pd_precio_final,
                                 pd_subtotal = q.pd_subtotal,
                                 IdCod_Impuesto = q.IdCod_Impuesto,
                                 pd_por_iva = q.pd_por_iva,
                                 pd_iva = q.pd_iva,
                                 pd_total = q.pd_total,
                                 anulado = q.anulado,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 lote_num_lote = q.lote_num_lote,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
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
    }
}
