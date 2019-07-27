using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
   public class FAC_008_Data
    {
        public List<FAC_008_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                List<FAC_008_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWFAC_008
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega == IdBodega
                             && q.IdNota == IdNota
                             select new FAC_008_Info
                             {
                                 IdEmpresa =q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNota = q.IdNota,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 pr_descripcion = q.pr_descripcion,
                                 nom_presentacion = q.nom_presentacion,
                                 lote_fecha_vcto = q.lote_fecha_vcto,
                                 lote_num_lote = q.lote_num_lote,
                                 sc_cantidad = q.sc_cantidad,
                                 sc_descUni = q.sc_descUni,
                                 sc_iva = q.sc_iva,
                                 sc_PordescUni = q.sc_PordescUni,
                                 sc_Precio = q.sc_Precio,
                                 sc_precioFinal = q.sc_precioFinal,
                                 sc_subtotal = q.sc_subtotal,
                                 sc_subtotal0 = q.sc_subtotal0,
                                 sc_subtotalIVA = q.sc_subtotalIVA,
                                 Su_Descripcion = q.Su_Descripcion,
                                 CreDeb = q.CreDeb,
                                 DescTotal = q.DescTotal,
                                 Nombres = q.Nombres,
                                 no_fecha = q.no_fecha,
                                 no_fecha_venc = q.no_fecha_venc,
                                 NumNota_Impresa = q.NumNota_Impresa,
                                 vt_por_iva = q.vt_por_iva,
                                 No_Descripcion = q.No_Descripcion,
                                 sc_observacion = q.sc_observacion,
                                  sc_total = q.sc_total

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
