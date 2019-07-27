using Core.Erp.Info.Reportes.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Importacion
{
   public class IMP_001_Data
    {
        public List<IMP_001_Info> get_list (int IdEmpresa, int IdOrdenCompra_ext)
        {
            try
            {
                List<IMP_001_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWIMP_001
                             where q.IdEmpresa == IdEmpresa
                             && q.IdOrdenCompra_ext == IdOrdenCompra_ext
                             select new IMP_001_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenCompra_ext = q.IdOrdenCompra_ext,
                                 IdProveedor = q.IdProveedor,
                                 oe_fecha = q.oe_fecha,
                                 oe_fecha_embarque_est = q.oe_fecha_embarque_est,
                                 oe_fecha_llegada_est = q.oe_fecha_llegada_est,
                                 oe_observacion = q.oe_observacion,
                                 NomMoneda = q.NomMoneda,
                                 pr_codigo = q.pr_codigo,
                                 pr_descripcion = q.pr_descripcion,
                                 od_cantidad = q.od_cantidad,
                                 od_costo = q.od_costo,
                                 od_costo_final = q.od_costo_final,
                                 od_descuento = q.od_descuento,
                                 od_por_descuento = q.od_por_descuento,
                                 od_subtotal = q.od_subtotal,
                                 NomFormaPago = q.NomFormaPago,
                                 NomPais = q.NomPais,
                                 NomUnidad = q.NomUnidad,
                                 NomVia = q.NomVia,
                                 nom_presentacion = q.nom_presentacion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Descripcion_Ciudad =q.Descripcion_Ciudad,
                                 od_total_fob=q.od_total_fob
                                 
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
